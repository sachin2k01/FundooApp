using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.Models
{
    public class Sent
    {
        public string SendMessage(ForgotPasswordModel forgot)
        {
            try
            {
                string frm_mail = "thenamesachin@gmail.com";
                string frm_pass = "wxgr llne grof nxqb";
                MailMessage message = new MailMessage();
                message.From = new MailAddress(frm_mail,forgot.eMail);//forgot.eMail(Reciever Email)
                message.Subject = forgot.Subject;
                message.Body = forgot.Message;
                message.BodyEncoding = Encoding.UTF8;
                message.IsBodyHtml = false;

                SmtpClient smtpclient = new SmtpClient("smtp.gmail.com", 587);
                    //Port = 587,              
                smtpclient.EnableSsl = true;
                smtpclient.UseDefaultCredentials = false;
                smtpclient.Credentials = new NetworkCredential(frm_mail, frm_pass);
                smtpclient.Send(message);
                

                return "Password reset send to email"+forgot.eMail;

            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
