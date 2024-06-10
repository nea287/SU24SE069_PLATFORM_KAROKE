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
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowAnyOrigins")]
    public class FriendsController : ControllerBase
    {
        private readonly IFriendService _friendService;

        public FriendsController(IFriendService friendService)
        {
            _friendService = friendService;
        }
        [HttpGet]
        public IActionResult GetFriends([FromQuery] FriendViewModel filter, [FromQuery] PagingRequest paging, [FromQuery] FriendOrderFilter orderFilter = FriendOrderFilter.Status)
        {
            var rs = _friendService.GetFriends(filter, paging, orderFilter);

            return rs.Results.IsNullOrEmpty() ? NotFound(rs) : Ok(rs);
        }

        [HttpDelete("{friendId:guid}")]
        public async Task<IActionResult> DeleteFriend(Guid friendId)
        {
            var rs = await _friendService.DeleteFriend(friendId);

            return rs.result.HasValue ? (rs.result.Value ? Ok(rs) : NotFound(rs)) : BadRequest(rs);
        }

        [HttpPost]
        public async Task<IActionResult> CreateFriend([FromBody] FriendRequestModel request)
        {
            var rs = await _friendService.CreateFriend(request);

            return rs.result.HasValue ? (rs.result.Value ? Ok(rs) : BadRequest(rs)) : BadRequest(rs);
        }
    }
}
