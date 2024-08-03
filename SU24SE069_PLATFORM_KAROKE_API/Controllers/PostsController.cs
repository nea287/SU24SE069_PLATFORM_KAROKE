using Castle.Core.Internal;
using Microsoft.AspNetCore.Mvc;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.Commons;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.RequestModels.Helpers;
using SU24SE069_PLATFORM_KAROKE_Service.IServices;
using SU24SE069_PLATFORM_KAROKE_Service.ReponseModels;
using SU24SE069_PLATFORM_KAROKE_Service.RequestModels.Post;

namespace SU24SE069_PLATFORM_KAROKE_API.Controllers
{
    [Route("api/posts")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostsController(IPostService postService)
        {
            _postService = postService;
        }
        [HttpGet]
        public async Task<IActionResult> GetPosts([FromQuery] PostViewModel filter, [FromQuery] PagingRequest paging, [FromQuery] PostOrderFilter orderFilter = PostOrderFilter.UpdateTime) 
        {
            var rs = await _postService.GetPosts(filter, paging, orderFilter);

            return rs.Results.IsNullOrEmpty() ? NotFound(rs) : Ok(rs);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost([FromBody] CreatePostRequestModel request)
        {
            var rs = await _postService.CreatePost(request);

            return rs.result.HasValue ? (rs.result.Value ? Ok(rs) : BadRequest(rs)) : BadRequest(rs);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdatePost(Guid id, [FromBody] string? caption)
        {
            var rs = await _postService.UpdatePost(id, caption);

            return rs.result.HasValue ? (rs.result.Value ? Ok(rs) : BadRequest(rs)) : BadRequest(rs);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeletePost(Guid id)
        {
            var rs = await _postService.Delete(id);

            return rs.result.HasValue ? (rs.result.Value ? Ok(rs) : BadRequest(rs)) : BadRequest(rs);
        }
    }
}
