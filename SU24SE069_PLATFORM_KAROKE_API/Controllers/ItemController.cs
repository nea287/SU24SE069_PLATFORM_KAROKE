using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.Commons;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.RequestModels.Helpers;
using SU24SE069_PLATFORM_KAROKE_Service.IServices;
using SU24SE069_PLATFORM_KAROKE_Service.ReponseModels;
using SU24SE069_PLATFORM_KAROKE_Service.RequestModels.Item;
using System.Security.Cryptography.X509Certificates;

namespace SU24SE069_PLATFORM_KAROKE_API.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    [EnableCors("AllowAnyOrigins")]
    public class ItemController : ControllerBase
    {
        private readonly IItemService _itemService;

        public ItemController(IItemService itemService)
        {
            _itemService = itemService;
        }
        [HttpGet("GetItem/{id:guid}")]
        public IActionResult GetItem(Guid id)
        {
            var rs = _itemService.GetItem(id);

            return rs.result.HasValue ? (rs.result.Value ? Ok(rs) : NotFound(rs)) : BadRequest(rs);
        }

        [HttpGet("GetItems")]
        public IActionResult GetItems([FromQuery] ItemViewModel filter, [FromQuery] PagingRequest paging, [FromQuery] ItemOrderFilter orderFilter = ItemOrderFilter.CreatedDate)
        {
            var rs = _itemService.GetItems(filter, paging, orderFilter);

            return rs.Results is not null? Ok(rs) : NotFound(rs);
        }

        [HttpDelete("DeleteItem/{id:guid}")]
        public IActionResult DeleteItem(Guid id)
        {
            var rs = _itemService.DeleteItem(id);

            return rs.result.HasValue ? (rs.result.Value ? Ok(rs) : BadRequest(rs)) : BadRequest(rs);   
        }

        [HttpPost("CreateItem")]
        public IActionResult CreateItem([FromBody] CreateItemRequestModel request)
        {
            var rs = _itemService.CreateItem(request);

            return rs.result.HasValue ? (rs.result.Value ? Ok(rs) : BadRequest(rs)) : BadRequest(rs);
        }

        [HttpPut("UpdateItem/{id:guid}")]
        public IActionResult UpdateItem(Guid id, [FromBody] UpdateItemRequestModel request)
        {
            var rs = _itemService.UpdateItem(id, request);

            return rs.result.HasValue ? (rs.result.Value ? Ok(rs) : BadRequest(rs)) : BadRequest(rs);
        }
    }
}
