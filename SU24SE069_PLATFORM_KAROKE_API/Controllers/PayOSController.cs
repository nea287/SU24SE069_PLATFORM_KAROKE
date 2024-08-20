using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SU24SE069_PLATFORM_KAROKE_API.AppStarts.OptionSetup;
using SU24SE069_PLATFORM_KAROKE_Service.IServices;
using SU24SE069_PLATFORM_KAROKE_Service.RequestModels.PayOS;

namespace SU24SE069_PLATFORM_KAROKE_API.Controllers
{
    [Route("api/payos")]
    [ApiController]
    [EnableCors("AllowAnyOrigins")]
    public class PayOSController : ControllerBase
    {
        private readonly IPayOSService _payOSService;

        public PayOSController(IPayOSService payOSService)
        {
            _payOSService = payOSService;
        }

        //[IPFilter("203.171.29.218")]
        [HttpPost]
        [Route("webhook")]
        public async Task<IActionResult> OnPayOSWebhookReceive([FromBody] PayOSWebhookRequest webhookRequest)
        {
            await _payOSService.ProcessPayOSWebhookRequest(webhookRequest);
            return Ok(new { success = true });
        }

        [HttpGet]
        [Route("return")]
        public async Task<IActionResult> OnPayOSPaymentReturn([FromQuery] PayOSPaymentQuery query)
        {
            return NoContent();
        }

        [HttpGet]
        [Route("cancel")]
        public async Task<IActionResult> OnPayOSPaymentCancel([FromQuery] PayOSPaymentQuery query)
        {
            await _payOSService.OnCancelPaymentLinkRequest(query);
            return NoContent();
        }
    }
}
