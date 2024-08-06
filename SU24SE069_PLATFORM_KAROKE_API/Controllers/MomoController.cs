using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.ReponseModels.Helpers;
using SU24SE069_PLATFORM_KAROKE_DataAccess.Models;
using SU24SE069_PLATFORM_KAROKE_Service.IServices;
using SU24SE069_PLATFORM_KAROKE_Service.ReponseModels;
using SU24SE069_PLATFORM_KAROKE_Service.RequestModels.MoMo;
using SU24SE069_PLATFORM_KAROKE_Service.RequestModels.MoneyTransaction;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_API.Controllers
{
    [Route("api/momo")]
    [ApiController]
    [EnableCors("AllowAnyOrigins")]
    public class MomoController : ControllerBase
    {
        private readonly IMomoService _momoService;
        private readonly IMonetaryTransactionService _monetaryTransactionService;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IMapper _mapper; // Assuming you inject AutoMapper here

        public MomoController(IMomoService momoService, IHttpClientFactory httpClientFactory, IMapper mapper, IMonetaryTransactionService monetaryTransactionService)
        {
            _momoService = momoService;
            _httpClientFactory = httpClientFactory;
            _mapper = mapper;
            _monetaryTransactionService = monetaryTransactionService;
        }

        [HttpPost]
        [Route("CreatePaymentUrl")]
        public async Task<IActionResult> CreatePaymentUrl([FromBody] MonetaryTransactionRequestModel transactionRequest)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await _monetaryTransactionService.CreateTransaction(transactionRequest);
            //var response = await client.PostAsJsonAsync("https://localhost:7017/api/transactions", transactionRequest);

            if (!response.result == true)
            {
                return BadRequest("Failed to create transaction");
            }

            var responseContent = JsonConvert.SerializeObject(response);

            try
            {
                var transactionResponse = JsonConvert.DeserializeObject<ResponseResult<MonetaryTransactionViewModel>>(responseContent);
                if (transactionResponse == null || transactionResponse.Value == null)
                {
                    return BadRequest("Failed to deserialize transaction response");
                }

                var monetaryTransaction = _mapper.Map<MonetaryTransactionViewModel>(transactionResponse.Value);

                var paymentResponse = await _momoService.CreatePaymentAsync(monetaryTransaction);

                return Ok(paymentResponse);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error deserializing transaction response: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("PaymentCallback")]
        public IActionResult PaymentCallBack()
        {
            var response = _momoService.PaymentExecuteAsync(HttpContext.Request.Query);
            return Ok(response);
        }

        [HttpPost]
        [Route("NotifyCallback")]
        public IActionResult NotifyCallBack([FromBody]MoMoIpnRequest moMoIpnRequest)
        {
            var response = _momoService.PaymentNotifyAsync(moMoIpnRequest);
            return NoContent();
        }

    }
}
