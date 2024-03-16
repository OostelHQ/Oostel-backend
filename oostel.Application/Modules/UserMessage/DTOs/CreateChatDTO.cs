using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oostel.Application.Modules.UserMessage.DTOs
{
    public record CreateChatDTO
    {
        public string Message { get; set; }
        public string RecipientId { get; set; }
        public string SenderId { get; set; }
        public string? MediaUrl { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; } = DateTime.Now;
    }
}
