using Castle.Core.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.Commons;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.RequestModels.Helpers;
using SU24SE069_PLATFORM_KAROKE_Service.IServices;
using SU24SE069_PLATFORM_KAROKE_Service.ReponseModels;
using SU24SE069_PLATFORM_KAROKE_Service.RequestModels.LoginActivity;

namespace SU24SE069_PLATFORM_KAROKE_API.Controllers
{
    [Route("api/login-activities")]
    [ApiController]
    public class LoginActivitiesController : ControllerBase
    {
        private readonly ILoginActivityService _service;

        public LoginActivitiesController(ILoginActivityService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetActivities([FromQuery] LoginActivityViewModel filter, [FromQuery] PagingRequest paging, [FromQuery] LoginActivityOrderFilter orderFilter)
        {
            var rs = await _service.GetActivites(filter, paging, orderFilter);

            return rs.Results.IsNullOrEmpty() ? NotFound(rs) : Ok(rs);  
        }

        [HttpPost]
        public async Task<IActionResult> CreateActivity([FromBody] LoginActivityRequestModel request)
        {
            var rs = await _service.CreateActivity(request);

            return rs.result.HasValue ? (rs.result.Value ? Ok(rs) : BadRequest(rs)) : BadRequest(rs);
        }
    }
}
