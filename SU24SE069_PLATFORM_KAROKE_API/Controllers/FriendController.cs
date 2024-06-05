using Castle.Core.Internal;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.Commons;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.RequestModels.Helpers;
using SU24SE069_PLATFORM_KAROKE_Service.IServices;
using SU24SE069_PLATFORM_KAROKE_Service.ReponseModels;
using SU24SE069_PLATFORM_KAROKE_Service.ReponseModels.Friend;

namespace SU24SE069_PLATFORM_KAROKE_API.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    [EnableCors("AllowAnyOrigins")]
    public class FriendController : ControllerBase
    {
        private readonly IFriendService _friendService;

        public FriendController(IFriendService friendService)
        {
            _friendService = friendService;
        }
        [HttpGet("GetFriends")]
        public IActionResult GetFriends([FromQuery] FriendViewModel filter, [FromQuery] PagingRequest paging, [FromQuery] FriendOrderFilter orderFilter = FriendOrderFilter.Status)
        {
            var rs = _friendService.GetFriends(filter, paging, orderFilter);

            return rs.Results.IsNullOrEmpty() ? NotFound(rs) : Ok(rs);
        }

        [HttpDelete("DeleteFriend/{friendId:guid}")]
        public IActionResult DeleteFriend(Guid friendId)
        {
            var rs = _friendService.DeleteFriend(friendId);

            return rs.result.HasValue ? (rs.result.Value ? Ok(rs) : NotFound(rs)) : BadRequest(rs);
        }

        [HttpPost("CreateFriend")]
        public IActionResult CreateFriend([FromBody] FriendRequestModel request)
        {
            var rs = _friendService.CreateFriend(request);

            return rs.result.HasValue ? (rs.result.Value ? Ok(rs) : BadRequest(rs)) : BadRequest(rs);
        }
    }
}
