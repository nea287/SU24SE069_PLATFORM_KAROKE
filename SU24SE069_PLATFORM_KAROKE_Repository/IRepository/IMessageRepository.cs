using SU24SE069_PLATFORM_KAROKE_DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Repository.IRepository
{
    public interface IMessageRepository : IBaseRepository<Message>
    {
        public Task<bool> CreateMessage(Message request);
       // public Task<bool> UpdateMessage(Message request);
    }
}
