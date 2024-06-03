using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.Commons;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.IServices;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.ReponseModels;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.RequestModels.Account;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.RequestModels.Helpers;

namespace SU24SE069_PLATFORM_KAROKE_API.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    [EnableCors("AllowAnyOrigins")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _service;

        public AccountController(IAccountService service)
        {
            _service = service;
        }
        //[Authorize(Policy = "RequireStaffRole")]
        [HttpPost("CreateAccount")]
        public IActionResult CreateAccount([FromBody] CreateAccountRequestModel request)
            => Ok(_service.CreateAccount(request));

        [HttpGet("GetAccount/{accountId:guid}")]
        public IActionResult GetAccount(Guid accountId) 
            => Ok(_service.GetAccount(accountId));

        [HttpGet("GetAccounts")]
        public IActionResult GetAccounts([FromQuery]AccountViewModel filter,
            [FromQuery] PagingRequest paging,[FromQuery] AccountOrderFilter orderFilter)
            => Ok(_service.GetAccounts(filter, paging, orderFilter));

        [HttpPut("UpdateAccount/{email}")]
        public IActionResult UpdateAccount(string email, [FromBody] UpdateAccountByMailRequestModel request)
            => Ok(_service.UpdateAccountByEmail(email, request));

    }
}

