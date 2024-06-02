using SU24SE069_PLATFORM_KAROKE_BusinessLayer.Commons;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.ReponseModels;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.ReponseModels.Helpers;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.RequestModels.Account;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.RequestModels.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_BusinessLayer.IServices
{
    public interface IAccountService
    {
        public UserLoginResponse Login(string username, string password);
        public ResponseResult<AccountViewModel> GetAccount(Guid accountId);
        public DynamicModelResponse.DynamicModelsResponse<AccountViewModel> GetAccounts(AccountViewModel filter,
            PagingRequest paging, AccountOrderFilter orderFilter);
        public ResponseResult<AccountViewModel> CreateAccount(CreateAccountRequestModel request);
        public ResponseResult<AccountViewModel> UpdateAccountByEmail(string email, UpdateAccountByMailRequestModel request);



    }
}
