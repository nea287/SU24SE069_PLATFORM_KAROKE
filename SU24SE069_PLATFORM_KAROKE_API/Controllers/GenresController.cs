using Castle.Core.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.Commons;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.RequestModels.Helpers;
using SU24SE069_PLATFORM_KAROKE_Service.IServices;
using SU24SE069_PLATFORM_KAROKE_Service.ReponseModels;
using SU24SE069_PLATFORM_KAROKE_Service.RequestModels.Genre;

namespace SU24SE069_PLATFORM_KAROKE_API.Controllers
{
    [Route("api/genres")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly IGenreService _service;

        public GenresController(IGenreService service)
        {
            _service = service;
        }
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetGenre(Guid id)
        {
            var rs = await _service.GetGenre(id);

            return rs.result.HasValue ? (rs.result.Value ? Ok(rs) : NotFound(rs)) : BadRequest(rs);
        }

        [HttpGet]
        public async Task<IActionResult> GetGenres([FromQuery] GenreViewModel filter, [FromQuery] PagingRequest paging, [FromQuery] GenreOrderFilter orderFilter = GenreOrderFilter.GenreId)
        {
            var rs = await _service.GetGenres(filter, paging, orderFilter);

            return rs.Results.IsNullOrEmpty() ? NotFound(rs) : Ok(rs);
        }
        
        [HttpGet("get-genres")]
        public async Task<IActionResult> GetGenresForAdmin([FromQuery] string filter, [FromQuery] PagingRequest paging, [FromQuery] GenreOrderFilter orderFilter = GenreOrderFilter.GenreId)
        {
            var rs = await _service.GetGenresForAdmin(filter, paging, orderFilter);

            return rs.Results.IsNullOrEmpty() ? NotFound(rs) : Ok(rs);
        }

        [HttpPost]
        public async Task<IActionResult> CreateGenre([FromBody] GenreRequestModel request)
        {
            var rs = await _service.CreateGenre(request);

            return rs.result.HasValue ? (rs.result.Value ? Ok(rs) : BadRequest(rs)) : BadRequest(rs);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateGenre([FromBody] GenreRequestModel request, Guid id)
        {
            var rs = await _service.UpdateGenre(id, request);

            return rs.result.HasValue ? (rs.result.Value ? Ok(rs) : BadRequest(rs)) : BadRequest(rs);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteGenre(Guid id)
        {
            var rs = await _service.DeleteGenre(id);

            return rs.result.HasValue ? (rs.result.Value ? Ok(rs) : BadRequest(rs)) : BadRequest(rs);
        }
    }
}
