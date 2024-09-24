using SU24SE069_PLATFORM_KAROKE_BusinessLayer.Commons;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.ReponseModels;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.ReponseModels.Helpers;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.RequestModels.Account;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.RequestModels.Helpers;
using SU24SE069_PLATFORM_KAROKE_Service.Filters;
using SU24SE069_PLATFORM_KAROKE_Service.RequestModels.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SU24SE069_PLATFORM_KAROKE_BusinessLayer.ReponseModels.Helpers.DynamicModelResponse;

namespace SU24SE069_PLATFORM_KAROKE_BusinessLayer.IServices
{
    public interface IAccountService
    {
        public Task<UserLoginResponse> Login(string username, string password);
        public Task<ResponseResult<AccountViewModel>> SignUp(CreateAccount1RequestModel request, string verifyCode);
        public Task<ResponseResult<AccountViewModel>> GetAccount(Guid accountId);
        public DynamicModelResponse.DynamicModelsResponse<AccountViewModel> GetAccounts(AccountFilter filter,
            PagingRequest paging, AccountOrderFilter orderFilter);
        
        public DynamicModelResponse.DynamicModelsResponse<AccountViewModel> GetAccountsForAdmin(string? role, string? filter,
            PagingRequest paging, AccountOrderFilter orderFilter);
        public Task<ResponseResult<AccountViewModel>> CreateAccount(CreateAccountRequestModel request);
        public Task<ResponseResult<AccountViewModel>> UpdateMemberAccount(Guid id, UpdateAccountByMailRequestModel request);
        public Task<ResponseResult<AccountViewModel>> UpdatePassword(Guid id, string password);
        public Task<ResponseResult<AccountViewModel>> UpdateAccount(Guid id, UpdateAccountRequestModel request);
        public Task<ResponseResult<AccountViewModel>> UpdateStatusOnline(Guid id,bool statusOnline);
        public DynamicModelResponse.DynamicModelsResponse<AccountViewModel> GetAccountFilterByStatusOnline(PagingRequest paging);

        public Task<ResponseResult<AccountViewModel>> DeleteAccount(Guid id); 
        public Task<ResponseResult<AccountViewModel>> ActiveAccount(Guid id);
    
        public bool SendVerificationCode(string receiverMail);
        public Task<ResponseResult<AccountViewModel>> CreateNewMemberAccount(MemberSignUpRequest signUpRequest);
        public Task<(bool, string)> SendVerificationEmail(string accountEmail);
        public Task<ResponseResult<AccountViewModel>> VerifyMemberAccount(MemberAccountVerifyRequest verifyRequest);
    }
}
