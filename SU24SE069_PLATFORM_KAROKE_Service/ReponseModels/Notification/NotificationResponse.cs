using SU24SE069_PLATFORM_KAROKE_BusinessLayer.Commons;

namespace SU24SE069_PLATFORM_KAROKE_Service.ReponseModels.Notification
{
    public class NotificationResponse
    {
        public int NotificationId { get; set; } = 0;
        public string Description { get; set; } = string.Empty;
        public NotificationType NotificationType { get; set; } = NotificationType.MESSAGE_COMMING;
        public NotificationStatus Status { get; set; } = NotificationStatus.UNREAD;
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public Guid AccountId { get; set; }
    }
}
