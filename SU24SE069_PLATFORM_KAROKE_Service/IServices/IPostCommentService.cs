using SU24SE069_PLATFORM_KAROKE_BusinessLayer.Commons;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.ReponseModels.Helpers;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.RequestModels.Helpers;
using SU24SE069_PLATFORM_KAROKE_DataAccess.Models;
using SU24SE069_PLATFORM_KAROKE_Service.ReponseModels;
using SU24SE069_PLATFORM_KAROKE_Service.RequestModels.Post;
using SU24SE069_PLATFORM_KAROKE_Service.RequestModels.PostComment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Service.IServices
{
    public interface IPostCommentService
    {
        public Task<ResponseResult<PostCommentViewModel>> CreatePostComment(CreatePostCommentRequestModel requestModel);
        public Task<ResponseResult<PostCommentViewModel>> UpdatePostComment(UpdatePostComment request, Guid id);
        public Task<DynamicModelResponse.DynamicModelsResponse<PostCommentViewModel>> GetComments(Filters.PostCommentFilter filter, PagingRequest paging, PostCommentFilter orderFilter);
        public Task<ResponseResult<PostCommentViewModel>> DeletePostComment(Guid id);
    }
}
