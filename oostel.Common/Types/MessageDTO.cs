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
        public int Id { get; set; }
        public int SenderId { get; set; }
        public string SenderLastname { get; set; }
        public string SenderPhotoUrl { get; set; }
        public int RecipientId { get; set; }
        public string RecipientLastname { get; set; }
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
