using Microsoft.EntityFrameworkCore;
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

        public async Task<bool> HasUserPurchaseSong(Guid? userId, Guid? songId)
        {
            if (userId == null || userId == Guid.Empty)
            {
                return false;
            }
            if (songId == null || songId == Guid.Empty)
            {
                return false;
            }
            return await GetDbSet().AnyAsync(s => s.MemberId == userId && s.SongId == songId);
        }
    }
}
