using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oostel.Infrastructure.EmailService
{
    public interface IEmailSender
    {
        Task SendEmailAsync(EmailParameter emailParameter);
    }
}
