using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Net.Mail;
using TCNX.Models;
using System.Net;

namespace TCNX.commonFunction
{
    public class MailSend
    {

        public static string SendMail(MailSendModel _mailsend)
        {
            try
            {
                MailMessage msg = new MailMessage();
                msg.To.Add(_mailsend.To);
                MailAddress address = new MailAddress(GlobalVariable.SendEmailid);
                msg.From = address;
                msg.Subject = _mailsend.Subject;
                msg.Body = _mailsend.Body;
                msg.IsBodyHtml = true;
                SmtpClient client = new SmtpClient();
                //client.Host = "relay-hosting.secureserver.net";
                client.Host = GlobalVariable.SendEmailhost;
                NetworkCredential credentials = new NetworkCredential(GlobalVariable.SendEmailid, GlobalVariable.SendEmailpassword);
                client.Credentials = credentials;
                client.Port = 25;
                client.UseDefaultCredentials = true;
                //Send the msg
                client.Send(msg);
                return "Mail Send Successfully.";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public static string SendMailRegistraion(string subject, string To, string Body)
        {
            try
            {
                MailMessage msg = new MailMessage();
                msg.To.Add(To);
                MailAddress address = new MailAddress(GlobalVariable.SendEmailid);
                msg.From = address;
                msg.Subject = subject;
                msg.Body = Body;
                msg.IsBodyHtml = true;
                SmtpClient client = new SmtpClient();
                client.Host = GlobalVariable.SendEmailhost;
                //   client.Host = "relay-hosting.secureserver.net";
                NetworkCredential credentials = new NetworkCredential(GlobalVariable.SendEmailid  , GlobalVariable.SendEmailpassword);
                
                client.Port = 25;
                client.UseDefaultCredentials = false;
                client.Credentials = credentials;
                client.ServicePoint.MaxIdleTime = 1;
                //Send the msg
                client.Send(msg);
                msg.Dispose();
                return "Mail Send Successfully.";
                //Display some feedback to the user to let them know it was sent
                //ViewBag.msg = "Your message was sent!";
            }
            catch (Exception ex)
            {
                return ex.Message;
                //If the message failed at some point, let the user know
                //ViewBag.msg = ex.ToString(); //alt text "Your message failed to send, please try again."
            }
        }



        public static string downline_requestgh(string subject, string To, string Body)
        {
            try
            {

                MailMessage msg = new MailMessage();
                msg.To.Add(To);
                MailAddress address = new MailAddress("support@turbotraining.live");
                msg.From = address;
                msg.Subject = subject;
                msg.Body = Body;
                msg.IsBodyHtml = true;
                SmtpClient client = new SmtpClient();
                //client.Host = "relay-hosting.secureserver.net";
                client.Host = "dedrelay.secureserver.net";
                NetworkCredential credentials = new NetworkCredential("support@turbotraining.live", "Support@123");
                client.Credentials = credentials;
                client.Port = 25;
                client.UseDefaultCredentials = true;
                //Send the msg
                client.Send(msg);
                return "Mail Send Successfully.";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public static string supportMail(string subject, string To, string Body)
        {
            try
            {
                MailMessage msg = new MailMessage();
                msg.To.Add(To);
                MailAddress address = new MailAddress("support@turbotraining.live");
                msg.From = address;
                msg.Subject = subject;
                msg.Body = Body;
                msg.IsBodyHtml = true;
                SmtpClient client = new SmtpClient();
                // client.Host = "relay-hosting.secureserver.net";
                client.Host = "dedrelay.secureserver.net";
                NetworkCredential credentials = new NetworkCredential("support@turbotraining.live", "Support@123");
                client.Credentials = credentials;
                client.Port = 25;
                client.UseDefaultCredentials = true;
                //Send the msg
                client.Send(msg);
                return "Mail Send Successfully.";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

    }
}