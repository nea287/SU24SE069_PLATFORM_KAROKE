using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SU24SE069_PLATFORM_KAROKE_Service.IServices;
using SU24SE069_PLATFORM_KAROKE_Service.RequestModels.InAppTransaction;
using SU24SE069_PLATFORM_KAROKE_Service.RequestModels.PurchasedSong;

namespace SU24SE069_PLATFORM_KAROKE_API.Controllers
{
    [Route("api/shops")]
    [ApiController]
    public class ShopsController : ControllerBase
    {
        private readonly IInAppTransactionService _inappTransaction;

        public ShopsController(IInAppTransactionService inappTransaction)
        {
            _inappTransaction = inappTransaction;
        }
        [HttpPost("purchase-song")]
        public async Task<IActionResult> PurchaseSong([FromBody] PurchasedSongRequestModel request)
        {
            var rs = await _inappTransaction.PurchaseSong(request);

            return rs.result.HasValue? (rs.result.Value ? Ok(rs) : BadRequest(rs)) : BadRequest(rs);
        }
    }
}
