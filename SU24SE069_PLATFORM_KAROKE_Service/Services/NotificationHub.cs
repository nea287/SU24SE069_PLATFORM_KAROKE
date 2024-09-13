using Microsoft.AspNetCore.SignalR;
using SU24SE069_PLATFORM_KAROKE_Service.IServices;

namespace SU24SE069_PLATFORM_KAROKE_Service.Services
{
    public class NotificationHub : Hub, INotificationHub
    {
        private const string OnClientConnectedMethodName = "NotificationHubConnected";

        public override async Task OnConnectedAsync()
        {
            string connectionId = Context.ConnectionId;
            string userId = Context.GetHttpContext().Request.Query["userId"];
            if (!string.IsNullOrEmpty(userId)) 
            {
                await Groups.AddToGroupAsync(connectionId, userId);
            }

            await Clients.Client(connectionId).SendAsync(OnClientConnectedMethodName, $"Hello user '{userId}' connected with ID: '{connectionId}'");

            Console.WriteLine($"User '{userId}' connected with ID: '{connectionId}'");
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            return base.OnDisconnectedAsync(exception);
        }

        public async Task SendNotification(string userId, string title, string message)
        {
            await Clients.Groups(userId).SendAsync("ReceiveNotification", title, message);
        }
    }
}
