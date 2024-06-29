using SU24SE069_PLATFORM_KAROKE_DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Repository.IRepository
{
    public interface IPostRateRepository : IBaseRepository<PostRate>
    {
        public Task<bool> AddPostRate(PostRate postRate);
        public Task<bool> DeletePostRate(Guid id);
        public Task<bool> UpdatePostRate(PostRate postRate);
        public bool ExistedPostRate(Guid id);
        public Task<PostRate> GetPostRate(Guid id);
    }
}
