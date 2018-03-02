using ReferralBonus.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;

namespace ReferralBonus.WebService
{
    /// <summary>
    /// Summary description for SubscribeMailer
    /// </summary>
    [WebService(Namespace = "http://maximumreferral.com/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]


    public class Subscribers
    {

        public string Name { get; set; }

        public string Email { get; set; }

    }

    public class SubscribeMailer : System.Web.Services.WebService
    {
        string ConnectionName = "GS_Connect_online";
        [WebMethod]
        public int ADD(int i, int j)
        {
            return i + j;
        }


        [WebMethod]
        public void Mailer(int FeedId)
        {
            ErrorExceptionHandler _handler = new ErrorExceptionHandler();
            _handler.Webservice_initialize("Web service initialized for subscribers");
            try
            {
                var path = Path.Combine(Server.MapPath("~/WebService"), "MailContent.html");

                string html = File.ReadAllText(path);
                string Email_Subscribe = "";
                Feed _feed = new Feed();

                string connectionString = ConfigurationManager.ConnectionStrings[ConnectionName].ConnectionString;

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("select TOP 1 * from GS_FEED WHERE Rowstatus='A'  order by 1 desc", con);
                    cmd.CommandType = CommandType.Text;

                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {

                        _feed.ID = (int)rdr["FeedID"];
                        _feed.Title = rdr["Title"].ToString();
                        _feed.Description = rdr["Description"].ToString();
                        _feed.image = rdr["Images"].ToString();
                        _feed.link = rdr["link"].ToString();
                        _feed.CategoryID = (int)rdr["Category_Id"];
                        _feed.Rowstatus = Convert.ToChar(rdr["Rowstatus"]);


                    }
                }

                html = html.Replace("Title", _feed.Title);
                html = html.Replace("Description", _feed.Description);
                html = html.Replace("link", _feed.link);
                html = html.Replace("PicName", _feed.image);



                List<Subscribers> _Subscribers = GetSubscribers.ToList();

                for (int i = 0; i < _Subscribers.Count; i++)
                {
                    Email_Subscribe = _Subscribers[i].Email;
                    Sentmail(html, Email_Subscribe, _feed.Title);
                }

                JavaScriptSerializer js = new JavaScriptSerializer();
                Context.Response.Write(js.Serialize(_Subscribers));

            }

            catch (Exception ex)
            {

                string Module_name = "MailerWebservice";
                string Action = "Get_All_Subscribers";
                _handler.ReportError(Module_name, ex.Message, Action);

            }



         



        }

        private void Sentmail(string html, string Email_Subscribe,string Subject)
        {

            try
            {

                string smtpAddress = "smtpout.secureserver.net";
                int portNumber = 80;
                //bool enableSSL = true;

                string emailFrom = "info@maximumreferral.com";
                string password = "Kalupur12";
                string emailTo = Email_Subscribe;
                string subject = Subject;
                string body = html;

                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress(emailFrom, "Maximum Referral");
                    mail.To.Add(emailTo);
                    mail.Subject = subject;
                    mail.Body = body;
                    mail.IsBodyHtml = true;
                    // Can set to false, if you are sending pure text.

                    //mail.Attachments.Add(new Attachment("C:\\SomeFile.txt"));
                    //mail.Attachments.Add(new Attachment("C:\\SomeZip.zip"));

                    using (SmtpClient smtp = new SmtpClient(smtpAddress, portNumber))
                    {
                      
                        smtp.Credentials = new NetworkCredential("support@maximumreferral.com", password);
                        smtp.Send(mail);

                    }
                }



            }


            catch (Exception ex)
            {

                string Module_name = "MailerWebservice";
                string Action = "Email_sent";
                ErrorExceptionHandler _handler = new ErrorExceptionHandler();
                _handler.ReportError(Module_name, ex.Message, Action);

            }

        }

        public IEnumerable<Subscribers> GetSubscribers
        {


            get
            {




                string connectionString = ConfigurationManager.ConnectionStrings[ConnectionName].ConnectionString;

                List<Subscribers> Subscribers_ = new List<Subscribers>();


                try
                {
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand("select * from Gs_Subscribe order by 1 desc", con);
                        cmd.CommandType = CommandType.Text;

                        SqlDataReader rdr = cmd.ExecuteReader();
                        while (rdr.Read())
                        {
                            Subscribers _Subscribers = new Subscribers();

                            _Subscribers.Name = rdr["Name"].ToString();
                            _Subscribers.Email = rdr["Email"].ToString();

                            Subscribers_.Add(_Subscribers);
                        }
                    }
                }

                catch (Exception ex)
                {

                    string Module_name = "Webservice";
                    string Action = "Mailer";
                    ErrorExceptionHandler _handler = new ErrorExceptionHandler();
                    _handler.ReportError(Module_name, ex.Message, Action);

                }



                return Subscribers_;
            }





        }




    }
}
