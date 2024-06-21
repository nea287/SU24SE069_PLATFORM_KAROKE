using SU24SE069_PLATFORM_KAROKE_DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Repository.IRepository
{
    public interface IConversationRepository : IBaseRepository<Conversation>
    {
        public Task<bool> CreateConversation(Conversation request);
    }
}
