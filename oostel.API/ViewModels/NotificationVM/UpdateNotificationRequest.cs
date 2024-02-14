namespace Oostel.API.ViewModels.NotificationVM
{
    public class UpdateNotificationRequest
    {
        public string UserId { get; set; }
        public List<string> NotificationIds { get; set; }
    }
}
