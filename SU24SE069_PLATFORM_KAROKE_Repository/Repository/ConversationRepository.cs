using SU24SE069_PLATFORM_KAROKE_DataAccess.Models;
using SU24SE069_PLATFORM_KAROKE_Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Repository.Repository
{
    public class ConversationRepository : BaseRepository<Conversation>, IConversationRepository
    {
        public async Task<bool> CreateConversation(Conversation request)
        {
            try
            {
                await InsertAsync(request);
                await SaveChagesAsync();
            }catch(Exception ex)
            {
                return false;
            }
            return true;
        }

        public IQueryable<Conversation> GetConversationOfMember(string include, Guid id)
        {
            IQueryable<Conversation> conversations;
            try
            {
                conversations = GetAll(includeProperties: include, filter: x => x.MemberId1 == id || x.MemberId2 == id).OrderBy(x => x.Messages.OrderBy(a => a.TimeStamp));
            }
            catch (Exception)
            {
                return null;
            }

            return conversations;
        }
    }
}
