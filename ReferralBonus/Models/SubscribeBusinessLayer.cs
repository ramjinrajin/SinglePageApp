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
    public class SubscribeBusinessLayer
    {
        string ConnectionName = "GS_Connect_online";
        public void Subscribe(SubscribeUs _sub_us)
        {


            string Connection_string = ConfigurationManager.ConnectionStrings[ConnectionName].ConnectionString;
            using (SqlConnection conn = new SqlConnection(Connection_string))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("USP_SubscribeInsert", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                if (conn.State == ConnectionState.Open)
                {
                    try
                    {
                        SqlParameter paramName = new SqlParameter();
                        paramName.ParameterName = "@Name";
                        paramName.Value = _sub_us.Name;
                        cmd.Parameters.Add(paramName);

                        SqlParameter paramEmail = new SqlParameter();
                        paramEmail.ParameterName = "@Email";
                        paramEmail.Value = _sub_us.Email;
                        cmd.Parameters.Add(paramEmail);

                        cmd.ExecuteNonQuery();

                        string smtpAddress = "smtpout.secureserver.net";
                        int portNumber = 80;
                        bool enableSSL = true;

                        string emailFrom = _sub_us.Email;
                        string password = "Kalupur12";
                        string emailTo = "support@maximumreferral.com";
                        string subject = "Subscription";
                        string body = _sub_us.Name + " subscribed  in maximumreferral.com for the mail " + _sub_us.Email;

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
                            }
                        }



                    }

                    catch (Exception ex)
                    {

                        throw;
                    }
                }







            }

        }
    }
}