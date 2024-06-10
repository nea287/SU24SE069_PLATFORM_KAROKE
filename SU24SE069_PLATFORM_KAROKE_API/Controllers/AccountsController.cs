using Castle.Core.Internal;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.Commons;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.IServices;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.ReponseModels;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.ReponseModels.Helpers;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.RequestModels.Account;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.RequestModels.Helpers;
using System.Net.NetworkInformation;

namespace SU24SE069_PLATFORM_KAROKE_API.Controllers
{
    [Route("api/accounts")]
    [ApiController]
    [EnableCors("AllowAnyOrigins")]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _service;

        public AccountsController(IAccountService service)
        {
            _service = service;
        }
        //[Authorize(Policy = "RequireStaffRole")]
        [HttpPost]
        public async Task<IActionResult> CreateAccount([FromBody] CreateAccountRequestModel request)
        {
            ResponseResult<AccountViewModel> result = await _service.CreateAccount(request);
            if(result.Value is null)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
            

        [HttpGet("{accountId:guid}")]
        public async Task<IActionResult> GetAccount(Guid accountId) 
        {
            var rs = await _service.GetAccount(accountId);

            return rs.Value is null? NotFound(rs) : Ok(rs);
        }

    [HttpGet]
        public IActionResult GetAccounts([FromQuery] AccountViewModel filter,
            [FromQuery] PagingRequest paging, [FromQuery] AccountOrderFilter orderFilter = AccountOrderFilter.CreatedTime)
        {
            var rs = _service.GetAccounts(filter, paging, orderFilter);

            return rs.Results.IsNullOrEmpty() ? NotFound(rs) : Ok(rs);
        }

        [HttpPut("{email}")]
        public async Task<IActionResult> UpdateAccount(string email, [FromBody] UpdateAccountByMailRequestModel request)
        {
            var rs = await _service.UpdateAccountByEmail(email, request);

            return rs.result.HasValue? (rs.result.Value? Ok(rs) : BadRequest(rs)) : NotFound(rs);

        }

    }
}

