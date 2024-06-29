using SU24SE069_PLATFORM_KAROKE_DataAccess.Models;
using SU24SE069_PLATFORM_KAROKE_Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Repository.Repository
{
    public class PostRateRepository : BaseRepository<PostRate>, IPostRateRepository
    {
        public async Task<bool> AddPostRate(PostRate postRate)
        {
            try
            {
                await InsertAsync(postRate);
                await SaveChagesAsync();
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> DeletePostRate(Guid id)
        {
            try
            {
                await HardDeleteGuid(id);
                SaveChages();
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public bool ExistedPostRate(Guid id)
            => Any(x => x.RateId == id);

        public async Task<PostRate> GetPostRate(Guid id)
        {
            PostRate? rs = new PostRate();
            try
            {
                rs = await FirstOrDefaultAsync(x => x.RateId == id);

            }
            catch (Exception ex)
            {
                return rs = null;
            }
            return rs;
        }

        public async Task<bool> UpdatePostRate(PostRate postRate)
        {
            try
            {
                await UpdateGuid(postRate, postRate.RateId);
                SaveChages();
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
    }
}
