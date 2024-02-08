using Oostel.Common.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oostel.Application.Modules.Notification.DTOs
{
    public class GetNotificationRequestDTO
    {
            public string UserId { get; set; }
            public NotificationType? NotificationType { get; set; }
            public int notificationDurationInDays { get; set; }

    }
}
