﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.ReponseModels.Helpers;
using SU24SE069_PLATFORM_KAROKE_Service.IServices;
using SU24SE069_PLATFORM_KAROKE_Service.ReponseModels;
using SU24SE069_PLATFORM_KAROKE_Service.RequestModels.InAppTransaction;
using SU24SE069_PLATFORM_KAROKE_Service.RequestModels.Item;
using SU24SE069_PLATFORM_KAROKE_Service.RequestModels.MoneyTransaction;
using SU24SE069_PLATFORM_KAROKE_Service.RequestModels.PurchasedSong;

namespace SU24SE069_PLATFORM_KAROKE_API.Controllers
{
    [Route("api/shops")]
    [ApiController]
    public class ShopsController : ControllerBase
    {
        private readonly IInAppTransactionService _inappTransaction;
        private readonly IMonetaryTransactionService _monetaryService;

        public ShopsController(IInAppTransactionService inappTransaction, IMonetaryTransactionService monetaryService)
        {
            _inappTransaction = inappTransaction;
            _monetaryService = monetaryService;
        }
        [HttpPost("purchase-song")]
        public async Task<IActionResult> PurchaseSong([FromBody] PurchasedSongRequestModel request)
        {
            var rs = await _inappTransaction.PurchaseSong(request);

            return rs.result.HasValue? (rs.result.Value ? Ok(rs) : BadRequest(rs)) : BadRequest(rs);
        }

        [HttpPost("purchase-item")]
        public async Task<IActionResult> PurchaseItem([FromBody]PurchaseItemRequestModel request)
        {
            var rs = await _inappTransaction.PurchaseItem(request);

            return rs.result.HasValue ? (rs.result.Value ? Ok(rs) : BadRequest(rs)) : BadRequest(rs);
        }
        
        [HttpPost("purchase-package")]
        public async Task<IActionResult> PurchasePackage([FromBody]MonetaryTransactionRequestModel request)
        {
            var rs = await _monetaryService.BuyPackageTransaction(request);

            return rs.result.HasValue ? (rs.result.Value ? Ok(rs) : BadRequest(rs)) : BadRequest(rs);
        }
    }
}
