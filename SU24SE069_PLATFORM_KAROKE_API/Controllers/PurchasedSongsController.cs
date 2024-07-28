using Castle.Core.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.Commons;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.RequestModels.Helpers;
using SU24SE069_PLATFORM_KAROKE_Service.IServices;
using SU24SE069_PLATFORM_KAROKE_Service.ReponseModels;

namespace SU24SE069_PLATFORM_KAROKE_API.Controllers
{
    [Route("api/purchased-songs")]
    [ApiController]
    public class PurchasedSongsController : ControllerBase
    {
        private readonly IPurchasedSongService _service;

        public PurchasedSongsController(IPurchasedSongService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetSongs([FromQuery] PurchasedSongViewModel filter, [FromQuery] PagingRequest paging, [FromQuery] PurchasedSongOrderFilter orderFilter = PurchasedSongOrderFilter.PurchasedSongId)
        {
            var rs = await _service.GetPurchasedSongs(filter, paging, orderFilter);

            return rs.Results.IsNullOrEmpty() ? NotFound(rs) : Ok(rs);
        }

        [HttpGet]
        [Route("filter")]
        public async Task<IActionResult> GetPurchasedSongFavoriteFilter([FromQuery] PurchasedSongViewModel filter, [FromQuery] PagingRequest paging, [FromQuery] PurchasedSongOrderFilter orderFilter = PurchasedSongOrderFilter.PurchaseDate)
        {
            var result = await _service.GetPurchasedSongFavoriteFilter(filter, paging, orderFilter);
            return result.Results.IsNullOrEmpty() ? NotFound(result) : Ok(result);
        }
    }
}
