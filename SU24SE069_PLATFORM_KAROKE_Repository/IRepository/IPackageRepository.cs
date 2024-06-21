using SU24SE069_PLATFORM_KAROKE_DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Repository.IRepository
{
    public interface IPackageRepository : IBaseRepository<Package>
    {
        public Task<bool> CreatePackage(Package package);
        public Task<bool> UpdatePackage(Package package);
        //public Task<bool> DeletePackage(Package package);
     
    }
}
