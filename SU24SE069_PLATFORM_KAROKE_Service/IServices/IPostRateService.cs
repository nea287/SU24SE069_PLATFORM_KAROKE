using SU24SE069_PLATFORM_KAROKE_BusinessLayer.Commons;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.ReponseModels.Helpers;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.RequestModels.Helpers;
using SU24SE069_PLATFORM_KAROKE_Service.ReponseModels;
using SU24SE069_PLATFORM_KAROKE_Service.RequestModels.PostRate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Service.IServices
{
    public interface IPostRateService
    {
        public Task<ResponseResult<PostRateViewModel>> CreatePostRate(CreatePostRateRequestModel request);
        public Task<ResponseResult<PostRateViewModel>> Delete(Guid id);
        public Task<DynamicModelResponse.DynamicModelsResponse<PostRateViewModel>> GetPostRates(PostRateViewModel filter, PagingRequest paging, PostRateOrderFilter orderFilter);
        public Task<ResponseResult<PostRateViewModel>> UpdatePostRate(Guid id);
    }
}
