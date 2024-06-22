using Castle.Core.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.Commons;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.RequestModels.Helpers;
using SU24SE069_PLATFORM_KAROKE_Service.IServices;
using SU24SE069_PLATFORM_KAROKE_Service.ReponseModels;
using SU24SE069_PLATFORM_KAROKE_Service.RequestModels.AccountInventoryItem;
using SU24SE069_PLATFORM_KAROKE_Service.RequestModels.InAppTransaction;

namespace SU24SE069_PLATFORM_KAROKE_API.Controllers
{
    [Route("api/in-app-transactions")]
    [ApiController]
    public class InAppTransactionsController : ControllerBase
    {
        private readonly IInAppTransactionService _service;

        public InAppTransactionsController(IInAppTransactionService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetInAppTransactions([FromQuery] InAppTransactionViewModel filter, [FromQuery] PagingRequest paging, [FromQuery] InAppTransactionOrderFilter orderFilter = InAppTransactionOrderFilter.CreatedDate)
        {
            var rs = await _service.GetTransactions(filter, paging, orderFilter);

            return rs.Results.IsNullOrEmpty() ? NotFound(rs) : Ok(rs);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetTransaction(Guid id)
        {
            var rs = await _service.GetTransaction(id);

            return rs.result.HasValue ? (rs.result.Value ? Ok(rs) : NotFound(rs)) : BadRequest(rs);
        }
        

        [HttpPost]
        public async Task<IActionResult> CreateInAppTransaction([FromBody] CrreateInAppTransactionRequestModel request)
        {
            var rs = await _service.CreateInAppTransaction(request);

            return rs.result.HasValue ? (rs.result.Value ? Ok(rs) : BadRequest(rs)) : BadRequest(rs);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateInventoryItem(Guid id, [FromBody] UpdateInAppTransactionRequestModel request)
        {
            var rs = await _service.UpdateInAppTransaction(request, id);

            return rs.result.HasValue ? (rs.result.Value ? Ok(rs) : BadRequest(rs)) : BadRequest(rs);
        }
    }
}
