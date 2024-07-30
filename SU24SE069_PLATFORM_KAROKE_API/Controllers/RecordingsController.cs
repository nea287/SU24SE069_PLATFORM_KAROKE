using Castle.Core.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.Commons;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.RequestModels.Helpers;
using SU24SE069_PLATFORM_KAROKE_Repository.IRepository;
using SU24SE069_PLATFORM_KAROKE_Service.IServices;
using SU24SE069_PLATFORM_KAROKE_Service.ReponseModels;
using SU24SE069_PLATFORM_KAROKE_Service.RequestModels.Recording;

namespace SU24SE069_PLATFORM_KAROKE_API.Controllers
{
    [Route("api/recordings")]
    [ApiController]
    public class RecordingsController : ControllerBase
    {
        private readonly IRecordingService _recordingService;

        public RecordingsController(IRecordingService recordingService)
        {
            _recordingService = recordingService;
        }
        [HttpGet]
        public async Task<IActionResult> GetRecordings([FromQuery] RecordingViewModel filter, [FromQuery] PagingRequest paging, [FromQuery] RecordingOrderFilter orderFilter = RecordingOrderFilter.UpdatedDate)
        {
            var rs =await _recordingService.GetRecordings(filter, paging, orderFilter);

            return rs.Results.IsNullOrEmpty() ? NotFound(rs) : Ok(rs);  
        }

        [HttpPost]
        public async Task<IActionResult> CreateRecording([FromBody] CreateRecordingRequestModel request)
        {
            var rs = await _recordingService.CreateRecording(request);

            return rs.result.HasValue ? (rs.result.Value ? Ok(rs) : BadRequest(rs)) : BadRequest(rs);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateRecording(Guid id, [FromBody] UpdateRecording1RequestModel request)
        {
            var rs = await _recordingService.UpdateRecording(id, request);

            return rs.result.HasValue ? (rs.result.Value ? Ok(rs) : BadRequest(rs)) : BadRequest(rs);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteRecording(Guid id)
        {
            var rs = await _recordingService.Delete(id);

            return rs.result.HasValue ? (rs.result.Value ? Ok(rs) : BadRequest(rs)) : BadRequest(rs);
        }

    }
}
