using Castle.Core.Internal;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.Commons;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.RequestModels.Helpers;
using SU24SE069_PLATFORM_KAROKE_Service.Filters;
using SU24SE069_PLATFORM_KAROKE_Service.IServices;
using SU24SE069_PLATFORM_KAROKE_Service.ReponseModels;
using SU24SE069_PLATFORM_KAROKE_Service.RequestModels.Item;
using System.Security.Cryptography.X509Certificates;

namespace SU24SE069_PLATFORM_KAROKE_API.Controllers
{
    [Route("api/items")]
    [ApiController]
    [EnableCors("AllowAnyOrigins")]
    public class ItemsController : ControllerBase
    {
        private readonly IItemService _itemService;

        public ItemsController(IItemService itemService)
        {
            _itemService = itemService;
        }
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetItem(Guid id)
        {
            var rs = await _itemService.GetItem(id);

            return rs.result.HasValue ? (rs.result.Value ? Ok(rs) : NotFound(rs)) : BadRequest(rs);
        }

        [HttpGet]
        public IActionResult GetItems([FromQuery] ItemFilter filter, [FromQuery] PagingRequest paging, [FromQuery] ItemOrderFilter orderFilter = ItemOrderFilter.CreatedDate)
        {
            var rs = _itemService.GetItems(filter, paging, orderFilter);

            return rs.Results.IsNullOrEmpty()? NotFound(rs) : Ok(rs);
        }
        [HttpGet]
        public IActionResult GetItemsForAdmin([FromQuery] string filter, [FromQuery] PagingRequest paging, [FromQuery] ItemOrderFilter orderFilter = ItemOrderFilter.CreatedDate)
        {
            var rs = _itemService.GetItemsForAdmin(filter, paging, orderFilter);

            return rs.Results.IsNullOrEmpty()? NotFound(rs) : Ok(rs);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteItem(Guid id)
        {
            var rs = await _itemService.DeleteItem(id);

            return rs.result.HasValue ? (rs.result.Value ? Ok(rs) : BadRequest(rs)) : BadRequest(rs);   
        }

        [HttpPost]
        public async Task<IActionResult> CreateItem([FromBody] CreateItemRequestModel request)
        {
            var rs = await _itemService.CreateItem(request);

            return rs.result.HasValue ? (rs.result.Value ? Ok(rs) : BadRequest(rs)) : BadRequest(rs);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateItem(Guid id, [FromBody] UpdateItemRequestModel request)
        {
            var rs = await _itemService.UpdateItem(id, request);

            return rs.result.HasValue ? (rs.result.Value ? Ok(rs) : BadRequest(rs)) : BadRequest(rs);
        }
    }
}
