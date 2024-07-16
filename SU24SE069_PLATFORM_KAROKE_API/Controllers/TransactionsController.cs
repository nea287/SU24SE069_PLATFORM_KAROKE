using Castle.Core.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.Ocsp;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.Commons;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.RequestModels.Helpers;
using SU24SE069_PLATFORM_KAROKE_Service.IServices;
using SU24SE069_PLATFORM_KAROKE_Service.ReponseModels;
using SU24SE069_PLATFORM_KAROKE_Service.RequestModels.MoneyTransaction;

namespace SU24SE069_PLATFORM_KAROKE_API.Controllers
{
    [Route("api/transactions")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly IMonetaryTransactionService _service;

        public TransactionsController(IMonetaryTransactionService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> GetTransactions([FromQuery] MonetaryTransactionViewModel filter, [FromQuery] PagingRequest paging, MonetaryTransactionOrderFilter orderFilter = MonetaryTransactionOrderFilter.CreatedDate)
        {
            var rs = await _service.GetTransactions(filter, paging, orderFilter);

            return rs.Results.IsNullOrEmpty() ? NotFound(rs) : Ok(rs);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTransaction([FromBody] MonetaryTransactionRequestModel request)
        {
            var rs = await _service.CreateTransaction(request);

            return rs.result.HasValue? (rs.result.Value? Ok(rs) : BadRequest(rs)) : BadRequest(rs);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpddateTransaction([FromBody] PaymentStatus status, Guid id)
        {
            var rs = await _service.UpdateStatusTransaction(id, status);

            return rs.result.HasValue ? (rs.result.Value ? Ok(rs) : BadRequest(rs)) : BadRequest(rs);
        }

    }
}
