using Castle.Core.Internal;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.Commons;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.RequestModels.Helpers;
using SU24SE069_PLATFORM_KAROKE_Service.IServices;
using SU24SE069_PLATFORM_KAROKE_Service.ReponseModels;
using SU24SE069_PLATFORM_KAROKE_Service.RequestModels.AccountInventoryItem;

namespace SU24SE069_PLATFORM_KAROKE_API.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    [EnableCors("AllowAnyOrigins")]

    public class InventoryItemController : ControllerBase
    {
        private readonly IAccountInventoryItemService _inventoryService;

        public InventoryItemController(IAccountInventoryItemService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        [HttpGet("GetInventoryItems")]
        public IActionResult GetInventoryItems([FromQuery] AccountInventoryItemViewModel filter, [FromQuery] PagingRequest paging, [FromQuery] AccountInventoryItemOrderFilter orderFilter = AccountInventoryItemOrderFilter.ActivateDate)
        {
            var rs = _inventoryService.GetAccountInventories(filter, paging, orderFilter);

            return rs.Results.IsNullOrEmpty() ? NotFound(rs) : Ok(rs);
        }

        [HttpPost("CreateInventoryItem")]
        public IActionResult CreateInventoryItem([FromBody] CreateAccountInventoryItemRequestModel request)
        {
            var rs = _inventoryService.CreateAccountInventory(request);

            return rs.result.HasValue? (rs.result.Value ? Ok(rs) : BadRequest(rs)) : BadRequest(rs);
        }

        [HttpPut("UpdateInventoryItem/{id:guid}")]
        public IActionResult UpdateInventoryItem(Guid id, [FromBody] CreateAccountInventoryItemRequestModel request)
        {
            var rs = _inventoryService.UpdateAccountInventoryItem(id, request);

            return rs.result.HasValue ? (rs.result.Value ? Ok(rs) : BadRequest(rs)) : BadRequest(rs);
        }
    }
}
