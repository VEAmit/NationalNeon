using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace NationalNeon.Web.Helpers
{
    public static class EmailHelper
    {
        public async static void SendEmail(string tomail,string body ,string subject )
        {
            MailMessage mail = new MailMessage();
            mail.To.Add(tomail);
            mail.From = new MailAddress("softwaretester.ve@gmail.com");
            mail.IsBodyHtml = true;
            mail.Body = body;
            mail.Subject = subject;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential("softwaretester.ve@gmail.com", "9412137628");
            smtp.EnableSsl = true;
            await smtp.SendMailAsync(mail);
           
        }

    
    }
}
