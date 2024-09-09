using SU24SE069_PLATFORM_KAROKE_DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Repository.IRepository
{
    public interface IGenreRepository : IBaseRepository<Genre>
    {
        public Task<bool> AddGenre(Genre request);
        public bool CheckGenre(string genreName);
        public Task<bool> DeleteGenre(Genre request);
        public Task<bool> UpdateGenre(Genre request);
    }
}
