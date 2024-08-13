using Castle.Core.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.Ocsp;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.Commons;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.RequestModels.Helpers;
using SU24SE069_PLATFORM_KAROKE_Service.IServices;
using SU24SE069_PLATFORM_KAROKE_Service.ReponseModels;
using SU24SE069_PLATFORM_KAROKE_Service.RequestModels.Singer;

namespace SU24SE069_PLATFORM_KAROKE_API.Controllers
{
    [Route("api/singers")]
    [ApiController]
    public class SingersController : ControllerBase
    {
        private readonly ISingerService _service;

        public SingersController(ISingerService service)
        {
            _service = service;
        }
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetSinger(Guid id)
        {
            var rs = await _service.GetSinger(id);

            return rs.result.HasValue ? (rs.result.Value ? Ok(rs) : NotFound(rs)) : BadRequest(rs);
        }

        [HttpGet]
        public async Task<IActionResult> GetSingers([FromQuery] SingerViewModel filter, [FromQuery] PagingRequest paging, [FromQuery] SingerOrderFilter orderFilter = SingerOrderFilter.SingerId)
        
        {
            var rs = await _service.GetSingers(filter, paging, orderFilter);

            return rs.Results.IsNullOrEmpty() ? NotFound(rs) : Ok(rs);
        }
        
        [HttpGet("get-singers")]
        public async Task<IActionResult> GetSingersForAdmin([FromQuery] string filter, [FromQuery] PagingRequest paging, [FromQuery] SingerOrderFilter orderFilter = SingerOrderFilter.SingerId)
        
        {
            var rs = await _service.GetSingersForAdmin(filter, paging, orderFilter);

            return rs.Results.IsNullOrEmpty() ? NotFound(rs) : Ok(rs);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSinger([FromBody] SingerRequestModel request)
        {
            var rs = await _service.CreateSinger(request);

            return rs.result.HasValue ? (rs.result.Value ? Ok(rs) : BadRequest(rs)) : BadRequest(rs);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateSinger([FromBody] SingerRequestModel request, Guid id)
        {
            var rs = await _service.UpdateSinger(id, request);

            return rs.result.HasValue ? (rs.result.Value ? Ok(rs) : BadRequest(rs)) : BadRequest(rs);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteSinger(Guid id)
        {
            var rs = await _service.DeleteSinger(id);

            return rs.result.HasValue ? (rs.result.Value ? Ok(rs) : BadRequest(rs)) : BadRequest(rs);
        }
    }
}
