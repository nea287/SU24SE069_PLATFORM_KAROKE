using SU24SE069_PLATFORM_KAROKE_DataAccess.Models;
using SU24SE069_PLATFORM_KAROKE_Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Repository.Repository
{
    public class SingerRepository : BaseRepository<Singer>, ISingerRepository
    {
        public async Task<bool> AddSinger(Singer singer)
        {
            try
            {
                await InsertAsync(singer);
                await SaveChagesAsync();
            }catch(Exception ex)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> DeleteSinger(Singer singer)
        {
            try
            {
                Delete(singer);
                await SaveChagesAsync();
            }catch(Exception ex)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> UpdateSinger(Singer request)
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
    }
}
