using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oostel.Application.Modules.UserMessage.DTOs
{
    public class CreateMessageDTO
    {
        public string Sender { get; set; }
        public string Recipient { get; set; }
        public string RecipientLastname { get; set; }
        public string SenderLastname { get; set; }
        public string Content { get; set; }
    }
}
