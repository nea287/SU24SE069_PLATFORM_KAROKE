namespace SU24SE069_PLATFORM_KAROKE_Service.IServices
{
    public interface INotificationHub
    {
        Task SendNotification(string userId, string title, string message);
    }
}
