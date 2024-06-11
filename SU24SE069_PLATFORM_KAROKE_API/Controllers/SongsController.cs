using Castle.Core.Internal;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.Ocsp;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.Commons;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.ReponseModels.Helpers;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.RequestModels.Helpers;
using SU24SE069_PLATFORM_KAROKE_Service.IServices;
using SU24SE069_PLATFORM_KAROKE_Service.ReponseModels;
using SU24SE069_PLATFORM_KAROKE_Service.RequestModels.Song;

namespace SU24SE069_PLATFORM_KAROKE_API.Controllers
{
    [Route("api/songs")]
    [ApiController]
    [EnableCors("AllowAnyOrigins")]
    public class SongsController : ControllerBase
    {
        private readonly ISongService _songService;

        public SongsController(ISongService songService)
        {
            _songService = songService;
        }

        [HttpGet("{songId:guid}")]
        public async Task<IActionResult> GetSong(Guid songId)
        {
            ResponseResult<SongViewModel> result = await _songService.GetSong(songId);

            return result.result.HasValue ? 
                (result.result.Value ? Ok(result) : NotFound(result)) : BadRequest(result);
        }

        [HttpGet]
        public IActionResult GetSongs([FromQuery]SongViewModel filter,
            [FromQuery]PagingRequest paging,[FromQuery] SongOrderFilter orderFilter = SongOrderFilter.UpdatedDate)
        {
            DynamicModelResponse.DynamicModelsResponse<SongViewModel> rs = _songService.GetSongs(filter, paging, orderFilter);

            return rs.Results.IsNullOrEmpty() ? NotFound(rs) : Ok(rs);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSong([FromBody] CreateSongRequestModel request)
        {
            var rs = await _songService.CreateSong(request);
            
            return rs.result.HasValue? (rs.result.Value ? Ok(rs) : BadRequest(rs)) : NotFound(rs);
        }

        [HttpPut("{songId:guid}")]
        public async Task<IActionResult> UpdateSong(Guid songId, [FromBody] UpdateSongRequestModel request)
        {
            var rs = await _songService.UpdateSong(songId, request);

            return rs.result.HasValue ? (rs.result.Value ? Ok(rs) : BadRequest(rs)) : NotFound(rs);
        }

        [HttpDelete("{songId:guid}")]
        public async Task<IActionResult> DeleteSong(Guid songId)
        {
            var rs =await _songService.DeleteSong(songId);

            return rs.result.HasValue ? (rs.result.Value ? Ok(rs) : BadRequest(rs)) : NotFound(rs);
        }
    }
}
