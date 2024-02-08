using Oostel.Common.Types;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oostel.Domain.Notification
{
    public class Notifications : BaseEntity<string>
    {
        public string UserId { get; set; }
        public string NotificationType { get; set; }
        public string Content { get; set; }
        public string UserProfilePicUrl { get; set; }
        public bool IsRead { get; set; }
        public string NotificationTypeValueId { get; set; }

        public Notifications()
        {
            LastModifiedDate = DateTime.UtcNow;
            CreatedDate = DateTime.UtcNow;
        }

        public Notifications(string id, string userId, string notificationType, string userProfilePicUrl, string content, DateTime createdDate, bool isRead, string notificationTypeValueId)
        {
            Id = id;
            UserId = userId;
            NotificationType = notificationType;
            Content = content;
            UserProfilePicUrl = userProfilePicUrl;
            CreatedDate = createdDate;
            IsRead = isRead;
            NotificationTypeValueId = notificationTypeValueId;
        }

        public Notifications(string id)
        {
            Id = id;
        }
    }
}
