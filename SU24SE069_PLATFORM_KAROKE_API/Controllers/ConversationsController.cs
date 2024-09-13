using Castle.Core.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.Commons;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.ReponseModels.Helpers;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.RequestModels.Helpers;
using SU24SE069_PLATFORM_KAROKE_Service.Filters;
using SU24SE069_PLATFORM_KAROKE_Service.IServices;
using SU24SE069_PLATFORM_KAROKE_Service.ReponseModels;
using SU24SE069_PLATFORM_KAROKE_Service.RequestModels.Conversation;

namespace SU24SE069_PLATFORM_KAROKE_API.Controllers
{
    [Route("api/conversations")]
    [ApiController]
    public class ConversationsController : ControllerBase
    {
        private readonly IConversationService _service;

        public ConversationsController(IConversationService service)
        {
            _service = service;     
        }

        [HttpGet]
        public async Task<IActionResult> GetConversations([FromQuery] ConversationViewModel filter, [FromQuery] PagingRequest paging, [FromQuery] ConversationOrderFilter orderFilter = ConversationOrderFilter.ConversationId)
        {
            var rs = await _service.GetConversations(filter, paging, orderFilter);

            return rs.Results.IsNullOrEmpty() ? NotFound(rs) : Ok(rs);
        }

        [HttpGet("get-conversation-of-member/{memberId:guid}")]
        public async Task<IActionResult> GetConversationOfMember(Guid memberId, [FromQuery]ConversationFilter filter,[FromQuery] PagingRequest paging)
        {
            var rs = _service.GetConversationOfMember(memberId, filter, paging);

            return rs.Results.IsNullOrEmpty() ? NotFound(rs) : Ok(rs);
        }

        [HttpPost]
        public async Task<IActionResult> CreateConversation([FromBody] ConversationRequestModel request)
        {
            var rs = await _service.CreateConversation(request);

            return rs.result.HasValue ? (rs.result.Value ? Ok(rs) : BadRequest(rs)) : BadRequest(rs);
        }

        [HttpPost("send-private-message")]
        public async Task<IActionResult> SendPrivateMessage([FromBody] ChatConversationRequestModel request)
        {
            var rs = await _service.SendPrivateMessage(request);

            return rs ? Ok(rs) : BadRequest(false);
        }
    }
}
