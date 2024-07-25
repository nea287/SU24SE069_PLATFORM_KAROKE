using Castle.Core.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.Ocsp;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.Commons;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.RequestModels.Helpers;
using SU24SE069_PLATFORM_KAROKE_Service.IServices;
using SU24SE069_PLATFORM_KAROKE_Service.ReponseModels;
using SU24SE069_PLATFORM_KAROKE_Service.RequestModels.FavouriteSong;

namespace SU24SE069_PLATFORM_KAROKE_API.Controllers
{
    [Route("api/favourite-songs")]
    [ApiController]
    public class FavouriteSongsController : ControllerBase
    {
        private readonly IFavouriteSongSerivce _service;

        public FavouriteSongsController(IFavouriteSongSerivce service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetFavouriteSongs([FromQuery] FavouriteSongViewModel filter, [FromQuery] PagingRequest paging, [FromQuery] FavouriteSongOrderFilter orderFilter = FavouriteSongOrderFilter.SongId)
        {
            var rs = _service.GetFavouriteSongs(filter, paging, orderFilter).Result;

            return rs.Results.IsNullOrEmpty() ? NotFound(rs) : Ok(rs);
        }

        [HttpPost]
        public async Task<IActionResult> CreateFavouriteSong([FromBody] CreateFavouriteSongRequestModel request)
        {
            var rs = await _service.CreateFavouteSong(request);

            return rs.result.HasValue ? (rs.result.Value ? Ok(rs) : BadRequest(rs)) : BadRequest(rs);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteFavouriteSong([FromQuery] CreateFavouriteSongRequestModel request)
        {
            var rs = await _service.DeleteFavouteSong(request);
            return rs.result.HasValue ? (rs.result.Value ? Ok(rs) : BadRequest(rs)) : NotFound(rs);
        }

        [HttpGet]
        [Route("filter")]
        public async Task<IActionResult> GetFavoriteSongsPurchasedFilter([FromQuery] FavouriteSongViewModel filter, [FromQuery] PagingRequest paging, [FromQuery] FavouriteSongOrderFilter orderFilter = FavouriteSongOrderFilter.SongId)
        {
            var result = await _service.GetFavoriteSongsPurchasedFilter(filter, paging, orderFilter);
            return result.Results.IsNullOrEmpty() ? NotFound(result) : Ok(result);
        }
    }
}
