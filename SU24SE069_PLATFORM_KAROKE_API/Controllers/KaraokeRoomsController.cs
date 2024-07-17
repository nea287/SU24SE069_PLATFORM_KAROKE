using Castle.Core.Internal;
using Microsoft.AspNetCore.Mvc;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.Commons;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.RequestModels.Helpers;
using SU24SE069_PLATFORM_KAROKE_Service.IServices;
using SU24SE069_PLATFORM_KAROKE_Service.ReponseModels;
using SU24SE069_PLATFORM_KAROKE_Service.RequestModels.KaraokeRoom;

namespace SU24SE069_PLATFORM_KAROKE_API.Controllers
{
    [Route("api/karaoke-rooms")]
    [ApiController]
    public class KaraokeRoomsController : ControllerBase
    {
        private readonly IKaraokeRoomService _service;

        public KaraokeRoomsController(IKaraokeRoomService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> GetRooms([FromQuery] KaraokeRoomViewModel filter, [FromQuery] PagingRequest paging, [FromQuery] KaraokeRoomOrderFilter orderFilter = KaraokeRoomOrderFilter.CreateTime)
        {
            var rs = await _service.GetRooms(filter, paging, orderFilter);

            return rs.Results.IsNullOrEmpty()? NotFound(rs) : Ok(rs);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRoom([FromBody] KaraokeRoomRequestModel request)
        {
            var rs = await _service.CreateRoom(request);

            return rs.result.HasValue ? (rs.result.Value ? Ok(rs) : BadRequest(rs)) : BadRequest(rs);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateRoom([FromBody] KaraokeRoomRequestModel request, Guid id)
        {
            var rs = await _service.UpdateRoom(request, id);

            return rs.result.HasValue ? (rs.result.Value ? Ok(rs) : BadRequest(rs)) : BadRequest(rs);
        }
    }
}
