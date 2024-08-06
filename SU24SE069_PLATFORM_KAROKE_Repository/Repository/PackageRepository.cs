using SU24SE069_PLATFORM_KAROKE_DataAccess.Models;
using SU24SE069_PLATFORM_KAROKE_Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Repository.Repository
{
    public class PackageRepository : BaseRepository<Package>, IPackageRepository
    {
        public async Task<decimal?> AmountTotalPackage(Guid id)
        {
            decimal? rs;
            try
            {
                rs = FirstOrDefaultAsync(x => x.PackageId == id).Result.MoneyAmount;
            }
            catch (Exception)
            {
                return -1;
            }
            return rs;
        }

        public async Task<bool> CreatePackage(Package package)
        {
            try
            {
                await InsertAsync(package);
                await SaveChagesAsync();
            }catch(Exception ex)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> UpdatePackage(Package package)
        {
            try
            {
                await Update(package);
                await SaveChagesAsync();

            }catch(Exception ex)
            {
                return false;
            }

            return true;
        }
    }
}
