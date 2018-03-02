using ReferralBonus.Controllers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace ReferralBonus.Models
{
    public class Contact_usBusinessLayer
    {
        string ConnectionName = "GS_Connect_online";
        public bool ContactUS(Contact_us _cont_us, bool _mail_status)
        {
          

            string Connection_string = ConfigurationManager.ConnectionStrings[ConnectionName].ConnectionString;
            using (SqlConnection conn = new SqlConnection(Connection_string))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("USP_ContactUSInsert", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                if (conn.State == ConnectionState.Open)
                {
                    try
                    {
                        SqlParameter paramName= new SqlParameter();
                        paramName.ParameterName = "@Name";
                        paramName.Value = _cont_us.Name;
                        cmd.Parameters.Add(paramName);

                        SqlParameter paramEmail = new SqlParameter();
                        paramEmail.ParameterName = "@Email";
                        paramEmail.Value = _cont_us.Email;
                        cmd.Parameters.Add(paramEmail);

                        SqlParameter paramSubject = new SqlParameter();
                        paramSubject.ParameterName = "@Subject";
                        paramSubject.Value = _cont_us.Subject;
                        cmd.Parameters.Add(paramSubject);

                        SqlParameter paramMessage = new SqlParameter();
                        paramMessage.ParameterName = "@Message";
                        paramMessage.Value = _cont_us.Message;
                        cmd.Parameters.Add(paramMessage);


                        cmd.ExecuteNonQuery();


                        string smtpAddress = "smtpout.secureserver.net";
                        int portNumber = 80;
                        //bool enableSSL = true;

                        string emailFrom = _cont_us.Email;
                        string password = "Kalupur12";
                        string emailTo = "support@maximumreferral.com";
                        string subject = _cont_us.Subject;
                        string body = _cont_us.Message;

                        using (MailMessage mail = new MailMessage())
                        {
                            mail.From = new MailAddress(emailFrom);
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
                                //smtp.EnableSsl = enableSSL;
                                smtp.Send(mail);
                                _mail_status = true;
                            }
                        }

                        

                    }

                    catch (Exception ex)
                    {
                       
                        _mail_status = false;
                        throw;
                    }
                }

            }
            return _mail_status;

        }
    }
}