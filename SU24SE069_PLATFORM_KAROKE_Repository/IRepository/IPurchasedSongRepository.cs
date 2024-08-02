using SU24SE069_PLATFORM_KAROKE_DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Repository.IRepository
{
    public interface IPurchasedSongRepository : IBaseRepository<PurchasedSong>
    {
        public Task<bool> CreatePurchasedSong(PurchasedSong purchasedSong);
        public bool CheckPurchasedSong(Guid memberId, Guid songId);
        Task<bool> HasUserPurchaseSong(Guid? userId, Guid? songId);
    }
}
