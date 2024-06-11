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
        public Task<UserLoginResponse> Login(string username, string password);
        public Task<ResponseResult<AccountViewModel>> GetAccount(Guid accountId);
        public DynamicModelResponse.DynamicModelsResponse<AccountViewModel> GetAccounts(AccountViewModel filter,
            PagingRequest paging, AccountOrderFilter orderFilter);
        public Task<ResponseResult<AccountViewModel>> CreateAccount(CreateAccountRequestModel request);
        public Task<ResponseResult<AccountViewModel>> UpdateAccountByEmail(string email, UpdateAccountByMailRequestModel request);
        public Task<ResponseResult<AccountViewModel>> DeleteAccount(Guid id);



    }
}
