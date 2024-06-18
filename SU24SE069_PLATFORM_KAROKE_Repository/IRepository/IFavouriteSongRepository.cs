using SU24SE069_PLATFORM_KAROKE_DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Repository.IRepository
{
    public interface IFavouriteSongRepository : IBaseRepository<FavouriteSong>
    {
        public Task<bool> CreateFavouriteSong(FavouriteSong request);
        public bool ExistedFavouriteSong(Guid songId, Guid memberId);
        public Task<bool> UpdateFavouriteSong(FavouriteSong request);
        //public Task<Account> GetAccount(Guid id);
        //public Task<Account> GetAccountByMail(string email);
        public Task<bool> DeleteFavouriteSong(FavouriteSong request);
    }
}
