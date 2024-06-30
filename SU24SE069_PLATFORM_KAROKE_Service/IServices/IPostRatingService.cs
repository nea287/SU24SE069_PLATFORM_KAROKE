using SU24SE069_PLATFORM_KAROKE_BusinessLayer.Commons;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.ReponseModels.Helpers;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.RequestModels.Helpers;
using SU24SE069_PLATFORM_KAROKE_Repository.Repository;
using SU24SE069_PLATFORM_KAROKE_Service.ReponseModels;
using SU24SE069_PLATFORM_KAROKE_Service.RequestModels.PostRating;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Service.IServices
{
    public interface IPostRatingService
    {
        public Task<ResponseResult<PostRatingViewModel>> UpdateRating(UpdatePostRatingRequestModel request, Guid memberId, Guid postId);
        public Task<ResponseResult<PostRatingViewModel>> CreateRating(PostRatingRequestModel request);
        public Task<ResponseResult<PostRatingViewModel>> DeleteRating(Guid memberId, Guid postId);
        public Task<DynamicModelResponse.DynamicModelsResponse<PostRatingViewModel>> GetRatings(PostRatingViewModel filter, PagingRequest paging, RatingOrderFilter orderFliter);
    }
}
