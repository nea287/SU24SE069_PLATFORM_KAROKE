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
