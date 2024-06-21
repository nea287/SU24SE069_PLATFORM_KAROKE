using SU24SE069_PLATFORM_KAROKE_DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Repository.IRepository
{
    public interface IKaraokeRoomRepository : IBaseRepository<KaraokeRoom>
    {
        public Task<bool> CreateRoom(KaraokeRoom request);
        public Task<bool> UpdateRoom(KaraokeRoom room);
        public bool ExistedRoom(string? roomLog = null, Guid? id = null);
        public Task<KaraokeRoom> GetRoom(Guid id);
    }
}
