using Castle.Core.Internal;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.Commons;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.RequestModels.Helpers;
using SU24SE069_PLATFORM_KAROKE_Service.Filters;
using SU24SE069_PLATFORM_KAROKE_Service.IServices;
using SU24SE069_PLATFORM_KAROKE_Service.ReponseModels;
using SU24SE069_PLATFORM_KAROKE_Service.RequestModels.AccountInventoryItem;

namespace SU24SE069_PLATFORM_KAROKE_API.Controllers
{
    [Route("api/inventory-items")]
    [ApiController]
    [EnableCors("AllowAnyOrigins")]

    public class InventoryItemsController : ControllerBase
    {
        private readonly IAccountItemService _inventoryService;

        public InventoryItemsController(IAccountItemService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        [HttpGet]
        public IActionResult GetInventoryItems([FromQuery] AccountItemFilter filter, [FromQuery] PagingRequest paging, [FromQuery] AccountInventoryItemOrderFilter orderFilter = AccountInventoryItemOrderFilter.ActivateDate)
        {
            var rs = _inventoryService.GetAccountInventories(filter, paging, orderFilter);

            return rs.Results.IsNullOrEmpty() ? NotFound(rs) : Ok(rs);
        }

        [HttpPost]
        public async Task<IActionResult> CreateInventoryItem([FromBody] CreateAccountInventoryItemRequestModel request)
        {
            var rs = await _inventoryService.CreateAccountInventory(request);

            return rs.result.HasValue? (rs.result.Value ? Ok(rs) : BadRequest(rs)) : BadRequest(rs);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateInventoryItem(Guid id, [FromBody] CreateAccountInventoryItemRequestModel request)
        {
            var rs = await _inventoryService.UpdateAccountInventoryItem(id, request);

            return rs.result.HasValue ? (rs.result.Value ? Ok(rs) : BadRequest(rs)) : BadRequest(rs);
        }
    }
}
