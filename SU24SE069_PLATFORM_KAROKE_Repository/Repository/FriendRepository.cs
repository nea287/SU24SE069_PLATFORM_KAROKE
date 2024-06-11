using SU24SE069_PLATFORM_KAROKE_DataAccess.Models;
using SU24SE069_PLATFORM_KAROKE_Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Repository.Repository
{
    public class FriendRepository : BaseRepository<Friend>, IFriendRepository
    {
        public async Task<bool> CreateFriend(Friend request)
        {
            try
            {
                await InsertAsync(request);
                SaveChages();

            }catch(Exception ex)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> DeleteFriend(Guid id)
        {
            try
            {
                Friend friend = await FirstOrDefaultAsync(x => x.ReceiverId == id);
                Delete(friend);
                SaveChages();
            }catch(Exception ex)
            {
                return false;
            }
            return true; 
        }
    }
}
