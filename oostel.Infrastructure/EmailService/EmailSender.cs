using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Oostel.Infrastructure.EmailService
{
    public class EmailSender //: IEmailSender
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




    }
}
