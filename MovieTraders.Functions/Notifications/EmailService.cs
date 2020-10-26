using System.Net;
using System.Net.Mail;

namespace MovieTraders.Functions.Notifications
{
    public class EmailService
    {
        private readonly EmailSettings settings;

        public EmailService(EmailSettings settings)
        {
            this.settings = settings;
        }

        public void Send(EmailMessage emailMessage)
        {
            MailMessage message = new MailMessage
            {
                From = new MailAddress(settings.FromEmail, settings.FromName)
            };

            message.To.Add(new MailAddress(emailMessage.Email));
            message.Subject = emailMessage.Subject;
            message.IsBodyHtml = true;
            message.Body = emailMessage.MessageHtml;

            var credentials = new NetworkCredential
            {
                UserName = settings.FromEmail,
                Password = settings.Password
            };

            using (SmtpClient client = new SmtpClient())
            {
                client.EnableSsl = true;
                client.Credentials = credentials;
                client.Host = settings.Host;
                client.Port = settings.Port;
                client.EnableSsl = true;
                client.Send(message);
            }
        }
    }
}
