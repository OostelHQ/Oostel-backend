using MimeKit;
using System;
using MailKit.Net.Smtp;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
