using Hangfire_Service.Models;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace EmailServices
{
    public class EmailService : IEmailService
    {
        public MailSettingsCredentials _mailMessageSettings { get; }

        public EmailService(IOptions<MailSettingsCredentials> mailMessageSettings, ILogger<EmailService> logger)
        {
            _mailMessageSettings = mailMessageSettings.Value;
        }
        public void SendEmail(string toEmail, string toName, string Subject, string Message)
        {
            using (var message = new MailMessage())
            {
                string from = _mailMessageSettings.EmailFrom;

                message.To.Add(new MailAddress(toEmail, toName));
                message.From = new MailAddress(from, _mailMessageSettings.DisplayName);
                //message.CC.Add(new MailAddress("cc@email.com", "CC Name"));
                //message.Bcc.Add(new MailAddress("bcc@email.com", "BCC Name"));
                message.Subject = Subject;
                message.Body = Message;
                message.IsBodyHtml = true;


                using (var client = new System.Net.Mail.SmtpClient(_mailMessageSettings.SmtpHost))
                {
                    client.Port = _mailMessageSettings.SmtpPort;
                    client.UseDefaultCredentials = true;
                    client.Credentials = new NetworkCredential(_mailMessageSettings.SmtpUser, _mailMessageSettings.SmtpPass);
                    client.EnableSsl = true;
                    System.Net.ServicePointManager.ServerCertificateValidationCallback = (object s, System.Security.Cryptography.X509Certificates.X509Certificate certificate, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors) => true;
                    try
                    {
                        client.Send(message);
                    }
                    catch (Exception ex)
                    {
                        //do nothing
                    }
                }
            }
        }
    }
}
