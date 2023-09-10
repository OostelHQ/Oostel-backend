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
            MailjetClient client = new MailjetClient("a5c40e5c36c8db1f9fd9e82cafb12b41", "a27259756955ea0d61d4cf12005391e6")//_config["Mailjet : APIKey"], _config["Mailjet : SecretKey"])
            {

            };
            MailjetRequest request = new MailjetRequest
            {
                Resource = Send.Resource,
            }


            .Property(Send.FromEmail, "ajeigbekehindeowooluwa@gmail.com")
            .Property(Send.FromName, "Oostel Management")
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
