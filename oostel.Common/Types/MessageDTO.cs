using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Oostel.Common.Types
{
    public class MessageDTO
    {
        public string Id { get; set; }
        public string SenderId { get; set; }
        public string SenderEmail { get; set; }
        public string SenderPhotoUrl { get; set; }
        public string RecipientId { get; set; }
        public string RecipientEmail { get; set; }
        public string RecipientPhotoUrl { get; set; }
        public string Content { get; set; }
        public DateTime? DateRead { get; set; }
        public DateTime MessageSent { get; set; }

        [JsonIgnore]
        public bool SenderDeleted { get; set; }

        [JsonIgnore]
        public bool RecipientDeleted { get; set; }
    }
}
