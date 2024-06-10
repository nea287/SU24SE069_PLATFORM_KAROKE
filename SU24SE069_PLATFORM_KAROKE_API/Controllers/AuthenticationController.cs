using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.IServices;
using SU24SE069_PLATFORM_KAROKE_Service.RequestModels.Account;

namespace SU24SE069_PLATFORM_KAROKE_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowAnyOrigins")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AuthenticationController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestModel request)
        {
            var rs = await _accountService.Login(request.email, request.password);

            return rs.Result.HasValue? (rs.Result.Value? Ok(rs) : BadRequest(rs)): BadRequest(rs);
        }
    }
}
