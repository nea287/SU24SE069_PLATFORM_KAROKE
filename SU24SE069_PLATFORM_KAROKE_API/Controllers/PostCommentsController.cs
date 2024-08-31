using Castle.Core.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.Commons;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.RequestModels.Helpers;
using SU24SE069_PLATFORM_KAROKE_Service.IServices;
using SU24SE069_PLATFORM_KAROKE_Service.ReponseModels;
using SU24SE069_PLATFORM_KAROKE_Service.RequestModels.Post;
using SU24SE069_PLATFORM_KAROKE_Service.RequestModels.PostComment;

namespace SU24SE069_PLATFORM_KAROKE_API.Controllers
{
    [Route("api/post-comments")]
    [ApiController]
    public class PostCommentsController : ControllerBase
    {
        private readonly IPostCommentService _service;

        public PostCommentsController(IPostCommentService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetComments([FromQuery] SU24SE069_PLATFORM_KAROKE_Service.Filters.PostCommentFilter filter, [FromQuery] PagingRequest paging, [FromQuery] PostCommentFilter orderFilter = PostCommentFilter.UploadTime)
        {
            var rs = await _service.GetComments(filter, paging, orderFilter);

            return rs.Results.IsNullOrEmpty() ? NotFound(rs) : Ok(rs);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteComment(Guid id)
        {
            var rs = await _service.DeletePostComment(id);

            return rs.result.HasValue ? (rs.result.Value ? Ok(rs) : BadRequest(rs)) : BadRequest(rs);
        }

        [HttpPost]
        public async Task<IActionResult> CreateComment(CreatePostCommentRequestModel request)
        {
            var rs = await _service.CreatePostComment(request);

            return rs.result.HasValue ? (rs.result.Value ? Ok(rs) : BadRequest(rs)) : BadRequest(rs);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateComment(UpdatePostComment request, Guid id)
        {
            var rs = await _service.UpdatePostComment(request, id);

            return rs.result.HasValue ? (rs.result.Value ? Ok(rs) : BadRequest(rs)) : BadRequest(rs);
        }
        
        [HttpPut("change-status/{id:guid}")]
        public async Task<IActionResult> UpdateStatusComment(PostCommentStatus request, Guid id)
        {
            var rs = await _service.ChangeStatusComment(id, request);

            return rs.result.HasValue ? (rs.result.Value ? Ok(rs) : BadRequest(rs)) : BadRequest(rs);
        }
    }
}
