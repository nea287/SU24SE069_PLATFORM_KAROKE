using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_DAO.IDAO
{
    public interface IChatHubDAO
    {
        public Task SendPublicMessage(string senderName, string message);
        public Task SendPrivateMessage(string receiverId, string message);
        public Task Login(string userId);
    }
}
