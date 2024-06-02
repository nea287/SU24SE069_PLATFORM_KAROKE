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
    [EnableCors]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _service;

        public AccountController(IAccountService service)
        {
            _service = service;
        }

        [HttpGet("Login/{username}/{password}")]
        public void Login(string username, string password)
            => _service.Login(username, password);

        [Authorize(Policy = "RequireStaffRole")]
        [HttpPost("CreateAccount")]
        public IActionResult CreateAccount([FromBody] CreateAccountRequestModel request)
            => Ok(_service.CreateAccount(request));

        [HttpGet("GetAccount")]
        public IActionResult GetAccount([FromHeader] string? username = null, [FromHeader]string? email = null, 
            [FromHeader]Guid? accountId = null) 
            => Ok(_service.GetAccount(username, email, accountId));

        [HttpGet("GetAccounts")]
        public IActionResult GetAccounts([FromQuery]AccountViewModel filter,
            [FromQuery] PagingRequest paging,[FromQuery] AccountOrderFilter orderFilter)
            => Ok(_service.GetAccounts(filter, paging, orderFilter));

    }
}

