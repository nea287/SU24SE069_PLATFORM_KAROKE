using Microsoft.AspNetCore.SignalR;
using SU24SE069_PLATFORM_KAROKE_DAO.DAO;
using SU24SE069_PLATFORM_KAROKE_Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Repository.Repository
{
    public class ChatHubRepository : IChatHubRepository
    {
        private readonly IHubContext<ChatHubDAO> _hubContext;

        public ChatHubRepository(IHubContext<ChatHubDAO> hubContext)
        {
            _hubContext = hubContext;
        }
        public async Task SendPrivateMessage(string receiverId, string senderName, string message)
        {
            await _hubContext.Clients.Client(receiverId).SendAsync("ReceivePrivateMessage", senderName, message);
        }

        public async Task SendPublicMessage(string senderName, string message)
        {
            await _hubContext.Clients.All.SendAsync("ReceiveMessage", senderName, message);
        }
    }
}
