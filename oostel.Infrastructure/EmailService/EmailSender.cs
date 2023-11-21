using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

using RestSharp;
using RestSharp.Authenticators;

namespace Oostel.Infrastructure.EmailService
{
    public class EmailSender : IEmailSender
    {

        private readonly IConfiguration _config;
        // private readonly EmailConfiguration _mailjetSettings;
        public EmailSender(IConfiguration configuration)
        {
            _config = configuration;
        }

        /*public EmailSender(IOptions<EmailConfiguration> mailjetSettings)
        {
            _mailjetSettings = mailjetSettings.Value;
        }*/

       /* public async Task SendEmailAsync(string email, string subject, string body)
        {
            MailjetClient client = new MailjetClient("61ad72cb57d19e529e18f9340ea6730b", "ded532f83c336c0cf6a6273cfbaa38d4");//"61ad72cb57d19e529e18f9340ea6730b", "ded532f83c336c0cf6a6273cfbaa38d4")//_config["Mailjet : APIKey"], _config["Mailjet : SecretKey"])
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
        }*/

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


        public bool SendEmailAsync(string email, string subject, string body)
        {
            var client = new RestClientOptions(baseUrl: "https://api.mailgun.net/v3");

            RestRequest request = new RestRequest(resource: "", Method.Post);

            client.Authenticator =
                new HttpBasicAuthenticator(username:"", password:_config.GetSection(key: "API_KEY").Value);

            request.AddParameter(name: "domain", value: "sandbox2a974a37475a4fb38760dfbc1898ac89.mailgun.org");
            request.Resource = "{domain}/messages";
            request.AddParameter(name: "from", value: "Excited User <postmaster@sandbox2a974a37475a4fb38760dfbc1898ac89.mailgun.org>");
            request.AddParameter(name: "to", value: email);
            request.AddParameter(name: "subject", value: subject);
            request.AddParameter(name: "text", value: body);
            request.Method = Method.Post;

            var response = client.Execute(client);

        }

        public async Task SendEmailAsync(string email, string subject, string body)
        {
            var client = new RestClient("https://api.mailgun.net/v3");

            var request = new RestRequest("{domain}/messages", Method.Post)
                .AddUrlSegment("domain", "sandbox2a974a37475a4fb38760dfbc1898ac89.mailgun.org")
                .AddParameter("from", "Excited User <postmaster@sandbox2a974a37475a4fb38760dfbc1898ac89.mailgun.org>")
                .AddParameter("to", email)
                .AddParameter("subject", subject)
                .AddParameter("text", body);

            // Manually set the Authorization header
            var apiKey = _config.GetSection("API_KEY").Value;
            request.AddHeader("Authorization", "Basic " + Convert.ToBase64String(System.Text.Encoding.Default.GetBytes($"api:{apiKey}")));

            var response = await client.ExecuteAsync(request);

            if (response.IsSuccessful)
            {
                Console.WriteLine($"Message sent! Response: {response.Content}");
            }
            else
            {
                Console.WriteLine($"An error occurred: {response.ErrorMessage}");
            }
        }







    }
}
