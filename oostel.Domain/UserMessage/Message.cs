using Oostel.Common.Types;
using Oostel.Domain.UserAuthentication.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oostel.Domain.UserMessage
{
    public class Message : BaseEntity<string>
    {
        public int SenderId { get; set; }
        public string SenderLastName { get; set; }
        public ApplicationUser Sender { get; set; }
        public int RecipientId { get; set; }
        public string RecipientLastName { get; set; }
        public ApplicationUser Recipient { get; set; }
        public string Content { get; set; }
        public DateTime? DateRead { get; set; }
        public DateTime MessengeSent { get; set; } = DateTime.Now;
        public bool SenderDeleted { get; set; }
        public bool RecipientDeleted { get; set; }
    }
}
