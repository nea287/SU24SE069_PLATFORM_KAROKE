using SU24SE069_PLATFORM_KAROKE_DataAccess.Models;
using SU24SE069_PLATFORM_KAROKE_Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Repository.Repository
{
    public class LoginActivityRepository : BaseRepository<LoginActivity>, ILoginActivityRepository
    {
        public async Task<bool> CreateActivity(LoginActivity request)
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

        public async Task<bool> UpdateActivity(LoginActivity request)
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
