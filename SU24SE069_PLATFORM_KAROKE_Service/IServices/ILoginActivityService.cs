using SU24SE069_PLATFORM_KAROKE_BusinessLayer.Commons;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.ReponseModels.Helpers;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.RequestModels.Helpers;
using SU24SE069_PLATFORM_KAROKE_Service.ReponseModels;
using SU24SE069_PLATFORM_KAROKE_Service.RequestModels.LoginActivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Service.IServices
{
    public interface ILoginActivityService
    {
        public Task<DynamicModelResponse.DynamicModelsResponse<LoginActivityViewModel>> GetActivites(LoginActivityViewModel filter, PagingRequest paging, LoginActivityOrderFilter orderFilter);
        public Task<ResponseResult<LoginActivityViewModel>> CreateActivity(LoginActivityRequestModel request);
    }
}
