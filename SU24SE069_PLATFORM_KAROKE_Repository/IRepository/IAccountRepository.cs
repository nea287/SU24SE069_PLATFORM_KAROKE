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
        public Task<Account> Login(string email);
        public Task<bool> CreateAccount(Account request);
        public bool ExistedAccount(string? email = null, string? username = null);
        public Task<bool> UpdateAccount(Account request);
        public Task<Account> GetAccount(Guid id);
        public Task<Account> GetAccountByMail(string email);
        public bool IsAccountEmailExisted(string email);
        public bool IsAccountUsernameExisted(string username);
    }
}
