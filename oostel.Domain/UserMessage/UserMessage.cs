using Oostel.Common.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oostel.Domain.UserMessage
{
    public class UserMessage : BaseEntity<string>
    {
        public string Message { get; set; }
        public string ReceiverId { get; set; }
        public string SenderId { get; set; }
        public string? MediaUrl { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.Now;

        public UserMessage()
        {
            Id = Guid.NewGuid().ToString();
        }

        public UserMessage(string id, string message, string receiverId, string senderId, string? mediaUrl)
        {
            Id = id;
            Message = message;
            ReceiverId = receiverId;
            SenderId = senderId;
            MediaUrl = mediaUrl;
        }
    }
}
