using SU24SE069_PLATFORM_KAROKE_BusinessLayer.Commons;

namespace SU24SE069_PLATFORM_KAROKE_Service.RequestModels.Notification
{
    public class NotificationStatusUpdateRequest
    {
        public NotificationStatus NewStatus { get; set; } = NotificationStatus.READ;
    }
}
