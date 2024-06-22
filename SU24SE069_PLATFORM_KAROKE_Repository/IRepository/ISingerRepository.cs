using SU24SE069_PLATFORM_KAROKE_DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Repository.IRepository
{
    public interface ISingerRepository : IBaseRepository<Singer>
    {
        public Task<bool> AddSinger(Singer singer);
        public Task<bool> DeleteSinger(Singer singer);
        public Task<bool> UpdateSinger(Singer singer);
    }
}
