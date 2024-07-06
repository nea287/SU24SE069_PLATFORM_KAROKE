using Castle.Core.Internal;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.Commons;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.RequestModels.Helpers;
using SU24SE069_PLATFORM_KAROKE_Service.IServices;
using SU24SE069_PLATFORM_KAROKE_Service.ReponseModels;
using SU24SE069_PLATFORM_KAROKE_Service.RequestModels.Artist;

namespace SU24SE069_PLATFORM_KAROKE_API.Controllers
{
    [Route("api/artists")]
    [ApiController]
    [EnableCors("AllowAnyOrigins")]
    public class ArtistsController : ControllerBase
    {
        private readonly IArtistService _service;

        public ArtistsController(IArtistService service)
        {
            _service = service;
        }
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetArtist(Guid id)
        {
            var rs = await _service.GetArtist(id);

            return rs.result.HasValue ? (rs.result.Value ? Ok(rs) : NotFound(rs)) : BadRequest(rs);
        }

        [HttpGet]
        public async Task<IActionResult> GetArtists([FromQuery] ArtistViewModel filter, [FromQuery] PagingRequest paging, [FromQuery] ArtistOrderFilter orderFilter = ArtistOrderFilter.ArtistId)
        {
            var rs = await _service.GetArtists(filter, paging, orderFilter);

            return rs.Results.IsNullOrEmpty() ? NotFound(rs) : Ok(rs);
        }

        [HttpPost]
        public async Task<IActionResult> CreateArtist([FromBody] ArtistRequestModel request)
        {
            var rs = await _service.CreateArtist(request);

            return rs.result.HasValue ? (rs.result.Value ? Ok(rs) : BadRequest(rs)) : BadRequest(rs);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateArtist([FromBody] ArtistRequestModel request, Guid id)
        {
            var rs = await _service.UpdateArtist(id, request);

            return rs.result.HasValue ? (rs.result.Value ? Ok(rs) : BadRequest(rs)) : BadRequest(rs);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteArtist(Guid id)
        {
            var rs = await _service.DeleteArtist(id);

            return rs.result.HasValue ? (rs.result.Value ? Ok(rs) : BadRequest(rs)) : BadRequest(rs);
        }
    }
}
