using SU24SE069_PLATFORM_KAROKE_DataAccess.Models;
using SU24SE069_PLATFORM_KAROKE_Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Repository.Repository
{
    public class AccountRepository : BaseRepository<Account>, IAccountRepository
    {
        public Account Login(string username, string password)
        {
            Account result = new Account();
            try
            {
                result = this.FistOrDefault(x => x.UserName.ToLower().Equals(username.ToLower()) 
                                    && x.Password.Equals(password));
            }catch(Exception ex)
            {
                throw new Exception(ex?.Message);
            }

            return result;
        }
    }
}
