using SU24SE069_PLATFORM_KAROKE_DataAccess.Models;
using SU24SE069_PLATFORM_KAROKE_Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Repository.Repository
{
    public class ArtistRepository : BaseRepository<Artist>, IArtistRepository
    {
        public async Task<bool> AddArtist(Artist singer)
        {
            try
            {
                await InsertAsync(singer);
                await SaveChagesAsync();
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> DeleteArtist(Artist singer)
        {
            try
            {
                await Update(singer);
                await SaveChagesAsync();
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> UpdateArtist(Artist request)
        {
            try
            {
                await Update(request);
                await SaveChagesAsync();
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
    }
}
