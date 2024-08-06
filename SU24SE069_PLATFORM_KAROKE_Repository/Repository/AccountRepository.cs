using SU24SE069_PLATFORM_KAROKE_DataAccess.Models;
using SU24SE069_PLATFORM_KAROKE_Repository.IRepository;

namespace SU24SE069_PLATFORM_KAROKE_Repository.Repository
{
    public class AccountRepository : BaseRepository<Account>, IAccountRepository
    {
        #region Read
        public IQueryable<Account> GetAccountFilterByStatusOnline(bool status)
        {
            IQueryable<Account> accounts;
            try
            {
                accounts = GetAll(x => x.IsOnline == status);
            }
            catch (Exception)
            {
                return null;
            }

            return accounts;    
        }

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
                result = await FirstOrDefaultAsync(x => x.Email.ToLower().Equals(email.ToLower()));
            }catch(Exception)
            {
                throw new Exception();
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

            }catch(Exception)
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

        public bool ExistedAccountById(Guid id)
        {
            return Any(x => x.AccountId == id);
        }

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
