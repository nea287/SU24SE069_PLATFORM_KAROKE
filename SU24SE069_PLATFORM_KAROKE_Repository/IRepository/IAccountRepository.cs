using SU24SE069_PLATFORM_KAROKE_DAO.IDAO;
using SU24SE069_PLATFORM_KAROKE_DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Repository.IRepository
{
    public interface IAccountRepository : IBaseDAO<Account>
    {
        public Account Login(string username, string password);
        public bool CreateAccount(Account request);
        public bool ExistedAccount(string? email = null, string? username = null);
        public bool UpdateAccountByMail(string email, Account request);
        public Account GetAccount(Guid id);
        public Account GetAccountByMail(string email);
    }
}
