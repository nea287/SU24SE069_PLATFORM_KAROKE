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
        #region Read
        public async Task<Account> GetAccount(Guid id)
        {
            Account result = new Account();
            try
            {
                result = FistOrDefault(x => x.AccountId == id);

            }catch(Exception ex)
            {
                return result = null;
            }

            return result;
        }

        public async Task<Account> GetAccountByMail(string email)
        {
            Account result = new Account();
            try
            {
                result = await FirstOrDefaultAsync(x => x.Email.ToLower().Equals(email));

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return result;
        }
        #endregion
        #region Create
        public async Task<bool> CreateAccount(Account request)
        {
            try
            {

                await InsertAsync(request);
                SaveChages();

            }catch(Exception ex)
            {
                return false;
            }
            return true;
        }
        #endregion

        #region Login
        public async Task<Account> Login(string email)
        {
            Account result = new Account();
            try
            {
                result = FirstOrDefaultAsync(x => x.Email.ToLower().Equals(email.ToLower())).Result;
            }catch(Exception ex)
            {
                throw new Exception(ex?.Message);
            }

            return result;
        }
        #endregion

        #region Update 
        public async Task<bool> UpdateAccount(Account request)
        {
            try
            {

                await UpdateGuid(request, request.AccountId);
                SaveChages();

            }catch(Exception ex)
            {
                return false;  
            }

            return true;
        }
        #endregion


        #region Validate
        public bool ExistedAccount(string? email = null, string? username = null)
            => this.Any(x =>
                        x.Email.ToLower().Equals(email??"null".ToLower())
                        || x.UserName.ToLower().Equals(username??"null".ToLower(), StringComparison.Ordinal));

        public bool IsAccountEmailExisted(string email)
        {
            return Any(a => a.Email.Trim().ToLower() == email.Trim().ToLower());
        }

        public bool IsAccountUsernameExisted(string username)
        {
            return Any(a => a.UserName.Trim().ToLower() == username.Trim().ToLower());
        }

        #endregion
    }
}
