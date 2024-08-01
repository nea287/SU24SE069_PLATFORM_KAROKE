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
    public class FavouriteSongRepository : BaseRepository<FavouriteSong>, IFavouriteSongRepository
    {
        public async Task<bool> CreateFavouriteSong(FavouriteSong request)
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

        public async Task<bool> DeleteFavouriteSong(FavouriteSong request)
        {
            try
            {
                var existingEntity = FindEntity(request.MemberId, request.SongId);
                if (existingEntity != null)
                {
                    DetachEntity(existingEntity);
                }

                Delete(request) ;
                await SaveChagesAsync();
            }catch(Exception ex)
            {
                return false;
            }

            return true;
        }

        public bool ExistedFavouriteSong(Guid songId, Guid memberId) => Any(x => x.SongId == songId && x.MemberId == memberId);
        public async Task<bool> UpdateFavouriteSong(FavouriteSong request)
        {
            try
            {
                await Update(request);
                await SaveChagesAsync();

            }catch(Exception ex)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> HasUserFavoriteSong(Guid? userId, Guid? songId)
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
