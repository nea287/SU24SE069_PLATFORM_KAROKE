using Castle.Core.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.Commons;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.RequestModels.Helpers;
using SU24SE069_PLATFORM_KAROKE_Service.IServices;
using SU24SE069_PLATFORM_KAROKE_Service.ReponseModels;
using SU24SE069_PLATFORM_KAROKE_Service.RequestModels.Message;

namespace SU24SE069_PLATFORM_KAROKE_API.Controllers
{
    [Route("api/messages")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly IMessageService _service;

        public MessagesController(IMessageService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetMessages([FromQuery] MessageViewModel filter, [FromQuery] PagingRequest paging, [FromQuery] MessageOrderFilter orderFilter = MessageOrderFilter.MessageId)
        {
            var rs = await _service.GetMessages(filter, paging, orderFilter);

            return rs.Results.IsNullOrEmpty() ? NotFound(rs) : Ok(rs);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMessage([FromBody] MessageRequestModel request)
        {
            var rs = await _service.CreateMessage(request);

            return rs.result.HasValue? (rs.result.Value? Ok(rs) : BadRequest(rs)) : BadRequest(rs);
        }
    }
}
