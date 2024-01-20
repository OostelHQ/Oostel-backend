using Mailjet.Client;
using Mailjet.Client.Resources;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;

namespace Oostel.Infrastructure.EmailService
{
    public class EmailSender : IEmailSender
    {

       // private readonly IConfiguration _config;
        private readonly EmailConfiguration _mailjetSettings;
        public EmailSender(IOptions<EmailConfiguration> mailjetSettings)
        {
            _mailjetSettings = mailjetSettings.Value;
        }

        public async Task SendEmailAsync(string email, string subject, string body)
        {
            MailjetClient client = new MailjetClient("3d5b5f5063962cd1f707ee860dfd56ea", "03c549b4d3a65617a022555a6bd222a7");
            {

            };
            MailjetRequest request = new MailjetRequest
            {
                Resource = Send.Resource,
            }


            .Property(Send.FromEmail, "fynda.care@gmail.com")
            .Property(Send.FromName, "Fynda APP")
            .Property(Send.Subject, subject)
            .Property(Send.HtmlPart, body)
            .Property(Send.Recipients, new JArray {
                new JObject {
                 {"Email",email}
                 }
                });

            MailjetResponse response = await client.PostAsync(request);
        }

       /* public async Task SendEmailAsync(EmailParameter emailParameter)
        {
            var message = new MimeMessage();
            message.From.Add(MailboxAddress.Parse(_mailjetSettings.UserName));
            message.To.Add(MailboxAddress.Parse(emailParameter.To));
            message.Subject = emailParameter.Subject;

            var builder = new BodyBuilder { HtmlBody = emailParameter.Content };
            message.Body = builder.ToMessageBody();

            using var client = new SmtpClient();
            await client.ConnectAsync(_mailjetSettings.SmtpServer, _mailjetSettings.Port, SecureSocketOptions.StartTls);
            await client.AuthenticateAsync(_mailjetSettings.UserName, _mailjetSettings.Password);
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }*/




    }
}
