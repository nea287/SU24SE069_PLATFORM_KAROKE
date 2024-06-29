using Castle.Core.Internal;
using Microsoft.AspNetCore.Mvc;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.Commons;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.RequestModels.Helpers;
using SU24SE069_PLATFORM_KAROKE_Service.IServices;
using SU24SE069_PLATFORM_KAROKE_Service.ReponseModels;
using SU24SE069_PLATFORM_KAROKE_Service.RequestModels.PostRate;
using SU24SE069_PLATFORM_KAROKE_Service.Services;

namespace SU24SE069_PLATFORM_KAROKE_API.Controllers
{
    [Route("api/postRates")]
    [ApiController]
    public class PostRatesController : ControllerBase
    {
        private readonly IPostRateService _postRateService;

        public PostRatesController(IPostRateService postRateService)
        {
            _postRateService = postRateService;
        }
        [HttpGet]
        public async Task<IActionResult> GetPostRates([FromQuery] PostRateViewModel filter, [FromQuery] PagingRequest paging, [FromQuery] PostRateOrderFilter orderFilter = PostRateOrderFilter.RateId) // filter = category ti' heh
        {
            var rs = await _postRateService.GetPostRates(filter, paging, orderFilter);

            return rs.Results.IsNullOrEmpty() ? NotFound(rs) : Ok(rs);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePostRate([FromBody] CreatePostRateRequestModel request)
        {
            var rs = await _postRateService.CreatePostRate(request);

            return rs.result.HasValue ? (rs.result.Value ? Ok(rs) : BadRequest(rs)) : BadRequest(rs);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdatePostRate(Guid id)
        {
            var rs = await _postRateService.UpdatePostRate(id);

            return rs.result.HasValue ? (rs.result.Value ? Ok(rs) : BadRequest(rs)) : BadRequest(rs);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeletePostRate(Guid id)
        {
            var rs = await _postRateService.Delete(id);

            return rs.result.HasValue ? (rs.result.Value ? Ok(rs) : BadRequest(rs)) : BadRequest(rs);
        }
    }
}
