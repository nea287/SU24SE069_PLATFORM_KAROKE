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
        public bool CreateFriend(Friend request)
        {
            try
            {
                Insert(request);
                SaveChages();

            }catch(Exception ex)
            {
                return false;
            }
            return true;
        }

        public bool DeleteFriend(Guid id)
        {
            try
            {
                Friend friend = GetFirstOrDefault(x => x.ReceiverId == id);
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
