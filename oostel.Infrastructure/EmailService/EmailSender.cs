using Mailjet.Client;
using Mailjet.Client.Resources;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;

namespace Oostel.Infrastructure.EmailService
{
    public class EmailSender : IEmailSender
    {

        private readonly IConfiguration _config;

        public EmailSender(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendEmailAsync(string email, string subject, string body)
        {
            MailjetClient client = new MailjetClient("61ad72cb57d19e529e18f9340ea6730b", "ded532f83c336c0cf6a6273cfbaa38d4")//_config["Mailjet : APIKey"], _config["Mailjet : SecretKey"])
            {

            };
            MailjetRequest request = new MailjetRequest
            {
                Resource = Send.Resource,
            }


            .Property(Send.FromEmail, "ajeigbekehinde160@gmail.com")
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

    }
}
