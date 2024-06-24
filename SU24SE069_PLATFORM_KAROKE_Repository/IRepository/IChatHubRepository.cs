using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Repository.IRepository
{
    public interface IChatHubRepository
    {
        public Task SendPublicMessage(string senderName,string message);
        public Task SendPrivateMessage(string receiverId, string senderName, string message);
    }
}
