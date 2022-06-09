using Microsoft.Extensions.Options;
using Prolunteer.Common.Configurations;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Prolunteer.Notifications.Email
{
    public class MailService
    {
        private readonly SMTPConfig SMTPConfig;

        public MailService(IOptions<SMTPConfig> smtpConfig)
        {
            this.SMTPConfig = smtpConfig.Value;
        }

        public void SendMessages(List<Email> messages)
        {
            using (var client = new SmtpClient(SMTPConfig.Host, SMTPConfig.Port))
            {
                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(SMTPConfig.Email, SMTPConfig.Password);

                foreach(var message in messages)
                {
                    var emailMessage = new MailMessage();

                    emailMessage.To.Add(String.Join(',', message.Recipients));
                    emailMessage.From = new MailAddress(SMTPConfig.Email, SMTPConfig.DisplayName);
                    emailMessage.Subject = message.Subject;
                    emailMessage.Body = message.Body;

                    client.Send(emailMessage);
                }

                client.Dispose();
            }
        }

        public void SendMessage(Email message)
        {
            var emailMessage = new MailMessage();

            emailMessage.To.Add(String.Join(',', message.Recipients));
            emailMessage.From = new MailAddress(SMTPConfig.Email, SMTPConfig.DisplayName);
            emailMessage.Subject = message.Subject;
            emailMessage.Body = message.Body;

            using(var client = new SmtpClient(SMTPConfig.Host, SMTPConfig.Port))
            {
                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(SMTPConfig.Email, SMTPConfig.Password);
                client.Send(emailMessage);
                client.Dispose();
            }
        }
    }
}