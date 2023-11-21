namespace Oostel.Infrastructure.EmailService
{
    public class EmailParameter
    {
        public string To { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }

        public EmailParameter(string to, string subject, string content)
        {
            To = to;
            Subject = subject;
            Content = content;
        }
        
    }
}
