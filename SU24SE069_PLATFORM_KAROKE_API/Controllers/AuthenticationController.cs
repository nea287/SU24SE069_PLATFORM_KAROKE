using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.IServices;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.ReponseModels.Helpers;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.ReponseModels;
using SU24SE069_PLATFORM_KAROKE_Service.RequestModels.Account;
using Org.BouncyCastle.Asn1.Ocsp;
using Microsoft.AspNetCore.Authorization;

namespace SU24SE069_PLATFORM_KAROKE_API.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    [EnableCors("AllowAnyOrigins")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AuthenticationController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestModel request)
        {
            var rs = await _accountService.Login(request.email, request.password);

            return rs.Result.HasValue ? (rs.Result.Value ? Ok(rs) : BadRequest(rs)) : BadRequest(rs);
        }


        [HttpPost("SendVerificationCode/{email}")]
        public IActionResult SendVerificationCode(string email)
        {
            var rs = _accountService.SendVerificationCode(email);

            return rs ? Ok(rs) : BadRequest(rs);
        }

        [HttpPost("SignUp/{verificationCode}")]
        public async Task<IActionResult> SignUp([FromBody] CreateAccount1RequestModel request, string verificationCode)
        {
            var rs = await _accountService.SignUp(request, verificationCode);

            return rs.result.HasValue ? (rs.result.Value ? Ok(rs) : BadRequest(rs)) : BadRequest(rs);
        }

        [HttpPost]
        [Route("sign-up/member")]
        [AllowAnonymous]
        public async Task<ActionResult<ResponseResult<AccountViewModel>>> MemberSignUp([FromBody] MemberSignUpRequest signUpRequest)
        {
            var result = await _accountService.CreateNewMemberAccount(signUpRequest);
            if (result.result == null || !result.result.Value)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpGet]
        [Route("verify/{email}")]
        [AllowAnonymous]
        public async Task<ActionResult> SendVerificationByEmail([FromRoute] string email)
        {
            (bool result, string message) = await _accountService.SendVerificationEmail(email);
            if (!result)
            {
                return BadRequest(new ResponseResult<bool>()
                {
                    result = result,
                    Message = message,
                    Value = result
                });
            }
            return Ok(new ResponseResult<bool>()
            {
                result = result,
                Message = message,
                Value = result
            });
        }

        [HttpPost]
        [Route("verify")]
        [AllowAnonymous]
        public async Task<ActionResult> VerifyMemberAccount([FromBody] MemberAccountVerifyRequest verifyRequest)
        {
            var result = await _accountService.VerifyMemberAccount(verifyRequest);
            if (result.result == null || !result.result.Value)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
