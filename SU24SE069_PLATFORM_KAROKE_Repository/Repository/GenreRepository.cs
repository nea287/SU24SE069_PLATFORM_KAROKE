using SU24SE069_PLATFORM_KAROKE_DataAccess.Models;
using SU24SE069_PLATFORM_KAROKE_Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Repository.Repository
{
    public class GenreRepository : BaseRepository<Genre>, IGenreRepository
    {
        public async Task<bool> AddGenre(Genre singer)
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

        public async Task<bool> DeleteGenre(Genre singer)
        {
            try
            {
                Delete(singer);
                await SaveChagesAsync();
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> UpdateGenre(Genre request)
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
