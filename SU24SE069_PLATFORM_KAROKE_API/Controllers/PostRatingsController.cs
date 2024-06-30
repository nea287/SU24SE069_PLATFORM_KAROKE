using Castle.Core.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.Commons;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.RequestModels.Helpers;
using SU24SE069_PLATFORM_KAROKE_Service.IServices;
using SU24SE069_PLATFORM_KAROKE_Service.ReponseModels;
using SU24SE069_PLATFORM_KAROKE_Service.RequestModels.PostRating;

namespace SU24SE069_PLATFORM_KAROKE_API.Controllers
{
    [Route("api/post-ratings")]
    [ApiController]
    public class PostRatingsController : ControllerBase
    {
        private readonly IPostRatingService _service;

        public PostRatingsController(IPostRatingService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetPostRatings([FromQuery] PostRatingViewModel filter, [FromQuery] PagingRequest paging, [FromQuery] RatingOrderFilter orderFilter = RatingOrderFilter.PostId)
        {
            var rs = await _service.GetRatings(filter, paging, orderFilter);

            return rs.Results.IsNullOrEmpty() ? NotFound(rs) : BadRequest(rs);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePostRatings([FromBody] PostRatingRequestModel request)
        {
            var rs = await _service.CreateRating(request);

            return rs.result.HasValue ? (rs.result.Value ? Ok(rs) : BadRequest(rs)) : BadRequest(rs);
        }

        [HttpPut("{memberId:guid}/{postId:guid}")]
        public async Task<IActionResult> UpdateRating([FromBody] UpdatePostRatingRequestModel request, Guid memberId, Guid postId)
        {
            var rs = await _service.UpdateRating(request, memberId, postId);

            return rs.result.HasValue ? (rs.result.Value ? Ok(rs) : BadRequest(rs)) : BadRequest(rs);
        }

        [HttpDelete("{memberId:guid}/{postId:guid}")]
        public async Task<IActionResult> DeleteRating(Guid memberId, Guid postId)
        {
            var rs = await _service.DeleteRating(memberId, postId);

            return rs.result.HasValue ? (rs.result.Value ? Ok(rs) : BadRequest(rs)) : BadRequest(rs);
        }
    }
}
