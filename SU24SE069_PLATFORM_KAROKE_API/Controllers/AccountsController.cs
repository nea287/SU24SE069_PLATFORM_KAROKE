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
using SU24SE069_PLATFORM_KAROKE_Service.Filters;
using SU24SE069_PLATFORM_KAROKE_Service.RequestModels.Account;
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
        //[Authorize(Policy = Constraints.ADMIN_STAFF_ROLE)]
        [HttpPost]
        public async Task<IActionResult> CreateAccount([FromBody] CreateAccountRequestModel request)
        {
            ResponseResult<AccountViewModel> result = await _service.CreateAccount(request);
            if (result.Value is null)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }


        [HttpGet("{accountId:guid}")]
        public async Task<IActionResult> GetAccount(Guid accountId)
        {
            var rs = await _service.GetAccount(accountId);

            return rs.Value is null ? NotFound(rs) : Ok(rs);
        }
        
        [HttpGet("get-online-account")]
        public async Task<IActionResult> GetOnlineAccount([FromQuery] PagingRequest paging)
        {
            var rs = _service.GetAccountFilterByStatusOnline( paging);

            return rs.Results.IsNullOrEmpty() ? NotFound(rs) : Ok(rs);
        }

        //[Authorize(Policy = Constraints.ADMIN_STAFF_ROLE)]
        [HttpGet]
        public IActionResult GetAccounts([FromQuery] AccountFilter filter,
                [FromQuery] PagingRequest paging, [FromQuery] AccountOrderFilter orderFilter = AccountOrderFilter.CreatedTime)
        {
            var rs = _service.GetAccounts(filter, paging, orderFilter);

            return rs.Results.IsNullOrEmpty() ? NotFound(rs) : Ok(rs);
        }
        
        [HttpGet("get-accounts")]
        public IActionResult GetAccountsForAdmin([FromQuery] string? filter,
                [FromQuery] PagingRequest paging, [FromQuery] AccountOrderFilter orderFilter = AccountOrderFilter.CreatedTime)
        {
            var rs = _service.GetAccountsForAdmin(filter, paging, orderFilter);

            return rs.Results.IsNullOrEmpty() ? NotFound(rs) : Ok(rs);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateAccount(Guid id, [FromBody] UpdateAccountByMailRequestModel request)
        {
            var rs = await _service.UpdateMemberAccount(id, request);

            return rs.result.HasValue ? (rs.result.Value ? Ok(rs) : BadRequest(rs)) : NotFound(rs);

        }

        //[Authorize(Policy = Constraints.ADMIN_STAFF_ROLE)]
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteAccount(Guid id)
        {
            var rs = await _service.DeleteAccount(id);
            return rs.result.HasValue ? (rs.result.Value ? Ok(rs) : BadRequest(rs)) : NotFound(rs);

        }

        [HttpPut("active-account/{id:guid}")]
        public async Task<IActionResult> ActiveAccount(Guid id)
        {
            var rs = await _service.ActiveAccount(id);
            return rs.result.HasValue ? (rs.result.Value ? Ok(rs) : BadRequest(rs)) : NotFound(rs);

        }
        [HttpPut("change-password/{id:guid}")]
        public async Task<IActionResult> ChangePassword(Guid id,[FromBody] string password)
        {
            var rs = await _service.UpdatePassword(id, password);
            return rs.result.HasValue ? (rs.result.Value ? Ok(rs) : BadRequest(rs)) : NotFound(rs);
        }
        
        [HttpPut("update-account/{id:guid}")]
        public async Task<IActionResult> UpdateAccountById(Guid id,[FromBody] UpdateAccountRequestModel request)
        {
            var rs = await _service.UpdateAccount(id, request);
            return rs.result.HasValue ? (rs.result.Value ? Ok(rs) : BadRequest(rs)) : NotFound(rs);
        }

        
        
    }
}

