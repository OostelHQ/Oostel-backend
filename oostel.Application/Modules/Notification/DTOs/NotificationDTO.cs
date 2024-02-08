using Oostel.Common.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oostel.Application.Modules.Notification.DTOs
{
    public class NotificationDTO
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public NotificationType NotificationType { get; set; }
        public string Content { get; set; }
        public string UserProfilePicUrl { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public bool IsRead { get; set; }
        public string NotificationTypeValueId { get; set; }
    }
}
