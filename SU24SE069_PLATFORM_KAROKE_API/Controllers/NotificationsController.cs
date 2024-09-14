using Castle.Core.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.Commons;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.RequestModels.Helpers;
using SU24SE069_PLATFORM_KAROKE_Service.Filters;
using SU24SE069_PLATFORM_KAROKE_Service.IServices;
using SU24SE069_PLATFORM_KAROKE_Service.ReponseModels;
using SU24SE069_PLATFORM_KAROKE_Service.RequestModels.Notification;

namespace SU24SE069_PLATFORM_KAROKE_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private readonly INotificationService _service;

        public NotificationsController(INotificationService service)
        {
            _service = service;
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetNotificationById(int id)
        {
            var rs = await _service.GetNotification(id);

            return rs.result.HasValue ? (rs.result.Value ? Ok(rs) : NotFound(rs)) : BadRequest(rs);
        }

        [HttpGet("{accountId:guid}")]
        public async Task<IActionResult> GetNotificationsByAccountId(Guid accountId, [FromQuery] NotificationFiilter filter, [FromQuery] PagingRequest paging, [FromQuery] NoticationFilter orderFilter = NoticationFilter.CreateDate)
        {
            var rs = await _service.GetNotificationByAccountId(accountId, filter, paging, orderFilter);

            return rs.Results.IsNullOrEmpty() ? NotFound(rs) : Ok(rs);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateNotificationStatus(int id, [FromBody] NotificationStatus status)
        {
            var rs = await _service.UpdateStatus(id, status);

            return rs.result.HasValue ? (rs.result.Value ? Ok(rs) : NotFound(rs)) : BadRequest(rs);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteNotification(int id)
        {
            var rs = await _service.DeleteNotification(id);

            return rs.result.HasValue ? (rs.result.Value ? Ok(rs) : NotFound(rs)) : BadRequest(rs);
        }

        [HttpPost]
        public async Task<IActionResult> CreateNotification([FromBody] CreateNotificationRequestModel request)
        {
            var rs = await _service.CreateNotification(request);

            return rs.result.HasValue ? (rs.result.Value ? Ok(rs) : BadRequest(rs)) : BadRequest(rs);
        }

        [HttpGet]
        [Route("unread/{userId:guid}")]
        public async Task<IActionResult> GetUserUnreadNotifications(Guid userId)
        {
            var result = await _service.GetUserUnreadNotifications(userId);
            return result.Value.IsNullOrEmpty() ? NotFound(result) : Ok(result);
        }

        [HttpPost]
        [Route("unread/{userId:guid}/read-all")]
        public async Task<IActionResult> UpdateUnreadNotificationsToRead(Guid userId)
        {
            var result = await _service.UpdateUnreadNotificationsToRead(userId);
            return result.Value == false ? BadRequest(result) : Ok(result);
        }

        [HttpPost]
        [Route("{id:int}/update")]
        public async Task<IActionResult> UpdateNotificationStatus([FromRoute]int id, [FromBody] NotificationStatusUpdateRequest updateRequest)
        {
            var result = await _service.UpdateNotificationStatus(id, updateRequest);
            return !(bool)result.result ? BadRequest(result) : Ok(result);
        }
    }
}
