using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.ReponseModels.Helpers;
using SU24SE069_PLATFORM_KAROKE_DataAccess.Models;
using SU24SE069_PLATFORM_KAROKE_Service.IServices;
using SU24SE069_PLATFORM_KAROKE_Service.ReponseModels;
using SU24SE069_PLATFORM_KAROKE_Service.ReponseModels.Momo;
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
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IMapper _mapper;

        public MomoController(IMomoService momoService, IHttpClientFactory httpClientFactory, IMapper mapper)
        {
            _momoService = momoService;
            _httpClientFactory = httpClientFactory;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("create-payment/up-package-purchase")]
        public async Task<IActionResult> CreatePaymentUrl([FromBody] MonetaryTransactionRequestModel transactionRequest)
        {
            var result = await _momoService.CreatePaymentAsync(transactionRequest);
            if(result == null)
            {
                return BadRequest(new ResponseResult<MoMoCreatePaymentResponse>()
                {
                    Message = $"Tạo yêu cầu thanh toán bằng MoMo thất bại. Vui lòng thử lại!",
                    Value = null,
                    result = false,
                });
            }
            return Ok(new ResponseResult<MoMoCreatePaymentResponse>()
            {
                Message = $"Tạo yêu cầu thanh toán bằng MoMo thành công!",
                Value = result,
                result = true,
            });
        }

        [HttpPost]
        [Route("ipn")]
        public async Task<IActionResult> ProcessMoMoIpnRequest([FromBody] MoMoIpnRequest ipnRequest)
        {
            await _momoService.ProcessMoMoIpnRequest(ipnRequest);
            return NoContent();
        }

    }
}
