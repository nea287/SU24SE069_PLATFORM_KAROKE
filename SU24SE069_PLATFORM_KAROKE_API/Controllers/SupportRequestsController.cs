using Castle.Core.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.Commons;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.RequestModels.Helpers;
using SU24SE069_PLATFORM_KAROKE_Service.IServices;
using SU24SE069_PLATFORM_KAROKE_Service.ReponseModels;
using SU24SE069_PLATFORM_KAROKE_Service.RequestModels.SupportRequest;

namespace SU24SE069_PLATFORM_KAROKE_API.Controllers
{
    [Route("api/support-requests")]
    [ApiController]
    public class SupportRequestsController : ControllerBase
    {
        private readonly ISupportRequestService _service;

        public SupportRequestsController(ISupportRequestService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> GetRequests([FromQuery] SupportRequestViewModel filter, [FromQuery] PagingRequest paging, [FromQuery] SupportRequestOrderFilter orderFilter = SupportRequestOrderFilter.CreateTime)
        {
            var rs = await _service.GetRequests(filter, paging, orderFilter);

            return rs.Results.IsNullOrEmpty()? NotFound(rs) : Ok(rs);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRequest([FromBody] SupportRequestRequestModel request)
        {
            var rs = await _service.CreateRequest(request);

            return rs.result.HasValue ? (rs.result.Value ? Ok(rs) : BadRequest(rs)) : BadRequest(rs);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateRequest(Guid id, [FromBody] SupportRequestStatus status)
        {
            var rs = await _service.UpdateRequest(id, status);

            return rs.result.HasValue ? (rs.result.Value ? Ok(rs) : BadRequest(rs)) : BadRequest(rs);

        }
    }
}
