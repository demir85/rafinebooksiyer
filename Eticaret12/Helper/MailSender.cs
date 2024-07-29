using Eticaret12.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net.Mail;


namespace Eticaret12.Helper
{
public class MailSender : IEmailSender
    {

        private ApplicationDbContext _entities;

        public MailSender(ApplicationDbContext entities)
        {
            _entities = entities;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            try
            {
                var senderMail = "rafinebooksiyer@gmail.com";

                var senderMailPassword = "xciwlnsgdlovbcio";  //uygulama oluşturup aldıgımız şifre

                var senderDisplayName = "rafine booksiyer";

                MailMessage mail = new MailMessage();

                mail.To.Add(email);

                mail.From = new MailAddress(senderMail, senderDisplayName, System.Text.Encoding.UTF8);

                mail.Subject = subject;

                mail.Body = htmlMessage;

                mail.Priority = MailPriority.High;

                SmtpClient smtpClient = new SmtpClient();

                smtpClient.Timeout = 10000;

                smtpClient.Credentials = new System.Net.NetworkCredential(senderMail, senderMailPassword);

                smtpClient.Port = 587;

                smtpClient.Host = "smtp.gmail.com";

                smtpClient.EnableSsl = true;

                await smtpClient.SendMailAsync(mail);

            }



            catch (Exception ex)

            {

                //Loglanabilir

            }

        }

}


}
 

