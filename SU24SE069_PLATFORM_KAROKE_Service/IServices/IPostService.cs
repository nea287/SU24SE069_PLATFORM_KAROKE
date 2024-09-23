using SU24SE069_PLATFORM_KAROKE_BusinessLayer.Commons;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.Helpers;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.ReponseModels.Helpers;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.RequestModels.Helpers;
using SU24SE069_PLATFORM_KAROKE_DataAccess.Models;
using SU24SE069_PLATFORM_KAROKE_Service.Filters;
using SU24SE069_PLATFORM_KAROKE_Service.ReponseModels;
using SU24SE069_PLATFORM_KAROKE_Service.RequestModels.Post;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Service.IServices
{
    public interface IPostService
    {
        public Task<ResponseResult<PostViewModel>> CreatePost(CreatePostRequestModel request);
        public Task<ResponseResult<PostViewModel>> Delete(Guid id);
        public Task<DynamicModelResponse.DynamicModelsResponse<PostViewModel>> GetPosts(PostFilter filter, PagingRequest paging, PostOrderFilter orderFilter);
        public Task<DynamicModelResponse.DynamicModelsResponse<PostAdminViewModel>> GetPostAdmins(PostFilter filter, PagingRequest paging, PostOrderFilter orderFilter);
        public Task<ResponseResult<PostViewModel>> UpdatePost(Guid id, string? caption);
        public Task<ResponseResult<PostViewModel>> UploadScore(Guid id);
        public Task<ResponseResult<PostViewModel>> ChangeStatus(Guid id, PostStatus status);
    }
}
