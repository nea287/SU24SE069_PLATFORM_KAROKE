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
        public Account GetAccount(string? email = null, Guid? id = null, string? username = null)
        {
            Account result = new Account();
            try
            {
                result = this.FistOrDefault(x => x.AccountId == id.Value 
                    || x.Email.Equals(email.ToLower()) || username.Equals(username.ToLower()));

            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return result;
        }

        #endregion
        #region Create
        public bool CreateAccount(Account request)
        {
            try
            {

                this.Insert(request);
                this.SaveChages();

            }catch(Exception ex)
            {
                return false;
            }
            return true;
        }
        #endregion

        #region Login
        public Account Login(string username, string password)
        {
            Account result = new Account();
            try
            {
                result = this.FistOrDefault(x => x.UserName.ToLower().Equals(username.ToLower()) 
                                    && x.Password.Equals(password) && x.IsVerified == true);
            }catch(Exception ex)
            {
                throw new Exception(ex?.Message);
            }

            return result;
        }
        #endregion

        #region Update 
        public bool UpdateAccountByMail(string email, Account request)
        {
            try
            {
                Account data = this.FistOrDefault(x => x.Email.ToLower().Equals(email.ToLower()));

                request.UserName = data.UserName;
                request.Email = data.Email;
                request.Role = data.Role;
                request.IsOnline = true;
                request.Role = data.Role;
                request.AccountId = data.AccountId;

                _ = this.UpdateGuid(data, data.AccountId);
                this.SaveChages();

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
                        x.Email.ToLower().Equals(email.ToLower())
                        || x.UserName.ToLower().Equals(username.ToLower(), StringComparison.Ordinal));
        #endregion
    }
}
