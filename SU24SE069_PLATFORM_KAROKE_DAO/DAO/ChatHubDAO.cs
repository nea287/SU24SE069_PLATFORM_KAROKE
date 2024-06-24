using Microsoft.AspNetCore.SignalR;
using SU24SE069_PLATFORM_KAROKE_DAO.IDAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_DAO.DAO
{
    public class ChatHubDAO : Hub, IChatHubDAO
    {
        public async Task SendPublicMessage(string senderName, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", senderName, message);
        }

        public async Task SendPrivateMessage(string receiverId, string message)
        {
            string userId = Context.ConnectionId;
            await Clients.Client(receiverId).SendAsync("ReceivePrivateMessge", userId, message);
        }

        public async Task Login(string userId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, userId);
            await Clients.Caller.SendAsync("LoginConfirmed", userId);
        }

        public override async Task OnConnectedAsync()
        {
            string connectionId = Context.ConnectionId;
            await Clients.Caller.SendAsync("OnConnected", connectionId);
        }
    }
}
