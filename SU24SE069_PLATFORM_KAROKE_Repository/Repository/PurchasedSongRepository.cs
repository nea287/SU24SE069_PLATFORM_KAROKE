using SU24SE069_PLATFORM_KAROKE_DataAccess.Models;
using SU24SE069_PLATFORM_KAROKE_Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Repository.Repository
{
    public class PurchasedSongRepository : BaseRepository<PurchasedSong>, IPurchasedSongRepository
    {
        public bool CheckPurchasedSong(Guid memberId, Guid songId)
        {
            return Any(x => x.MemberId == memberId && x.SongId == songId);
        }

        public async Task<bool> CreatePurchasedSong(PurchasedSong purchasedSong)
        {
            try
            {
                await InsertAsync(purchasedSong);
                await SaveChagesAsync();
            }catch(Exception ex)
            {
                return false;
            }
            return true;
        }
    }
}
