using Oostel.Common.Constants;

namespace Oostel.API.ViewModels.NotificationVM
{
    public class GetNotificationsRequest
    {
        public string UserId { get; set; }
        public NotificationType NotificationType { get; set; }
        public int NotificationDurationInDays { get; set; }
    }
}
