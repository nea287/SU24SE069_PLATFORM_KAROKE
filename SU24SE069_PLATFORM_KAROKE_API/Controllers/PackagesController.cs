﻿using Castle.Core.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.Ocsp;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.Commons;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.RequestModels.Helpers;
using SU24SE069_PLATFORM_KAROKE_Repository.Repository;
using SU24SE069_PLATFORM_KAROKE_Service.IServices;
using SU24SE069_PLATFORM_KAROKE_Service.ReponseModels;
using SU24SE069_PLATFORM_KAROKE_Service.RequestModels.MoneyTransaction;
using SU24SE069_PLATFORM_KAROKE_Service.RequestModels.Package;

namespace SU24SE069_PLATFORM_KAROKE_API.Controllers
{
    [Route("api/packages")]
    [ApiController]
    public class PackagesController : ControllerBase
    {
        private readonly IPackageService _service;

        public PackagesController(IPackageService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetPackages([FromQuery] PackageViewModel filter, [FromQuery] PagingRequest paging, PackageOrderFilter orderFilter = PackageOrderFilter.CreatedDate)
        {
            var rs = await _service.GetPackages(filter, paging, orderFilter);

            return rs.Results.IsNullOrEmpty() ? NotFound(rs) : Ok(rs);
        }


        [HttpGet("get-packages")]
        public async Task<IActionResult> GetPackagesForAdmin([FromQuery] string? filter, [FromQuery] PagingRequest paging, PackageOrderFilter orderFilter = PackageOrderFilter.CreatedDate)
        {
            var rs = await _service.GetPackagesForAdmin(filter, paging, orderFilter);

            return rs.Results.IsNullOrEmpty() ? NotFound(rs) : Ok(rs);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePackge([FromBody] PackageRequestModel request)
        {
            var rs = await _service.CreatePackage(request);

            return rs.result.HasValue ? (rs.result.Value ? Ok(rs) : BadRequest(rs)) : BadRequest(rs);
        }

        [HttpPut("update-status/{id:guid}")]
        public async Task<IActionResult> UpdateStatus(Guid id, [FromBody] PackageStatus status)
        {
            var rs = await _service.ChangeStatus(id, status);

            return rs.result.HasValue ? (rs.result.Value ? Ok(rs) : BadRequest(rs)) : BadRequest(rs);
        }
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdatePackage([FromBody] PackageRequestModel request, Guid id)
        {
            var rs = await _service.UpdatePackage(request, id);

            return rs.result.HasValue ? (rs.result.Value ? Ok(rs) : BadRequest(rs)) : BadRequest(rs);

        }

        [HttpPut("enable-package/{id:guid}")]
        public async Task<IActionResult> EnablePackage(Guid id)
        {
            var rs = await _service.EnablePackage(id);

            return rs.result.HasValue ? (rs.result.Value ? Ok(rs) : BadRequest(rs)) : BadRequest(rs);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> RemovePackage(Guid id)
        {
            var rs = await _service.DeletePackage(id);

            return rs.result.HasValue ? (rs.result.Value ? Ok(rs) : BadRequest(rs)) : BadRequest(rs);
        }

        [HttpPost]
        [Route("purchase/payos")]
        public async Task<IActionResult> PurchasePackageWithPayOS([FromBody] MonetaryTransactionRequestModel transactionRequest)
        {
            var result = await _service.CreatePayOSPackagePurchasePayment(transactionRequest);
            if (result.result == null || !result.result.Value)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpDelete]
        [Route("cancel/payos/{id:guid}")]
        public async Task<IActionResult> CancelPayOSPackagePurchaseRequest(Guid id)
        {
            var result = await _service.CancelPayOSPackagePurchaseRequest(id);
            if (result.result == null || !result.result.Value)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpGet]
        [Route("request/pending/{memberId:guid}")]
        public async Task<IActionResult> GetMemberPendingPackagePurchaseRequest(Guid memberId)
        {
            var result = await _service.GetMemberLatestPendingPurchaseRequest(memberId);
            if (result.result == null || !result.result.Value)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
