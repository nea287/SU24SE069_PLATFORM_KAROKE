using Castle.Core.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.Commons;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.RequestModels.Helpers;
using SU24SE069_PLATFORM_KAROKE_Service.IServices;
using SU24SE069_PLATFORM_KAROKE_Service.ReponseModels;
using SU24SE069_PLATFORM_KAROKE_Service.RequestModels.Report;

namespace SU24SE069_PLATFORM_KAROKE_API.Controllers
{
    [Route("api/reports")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly IReportService _service;

        public ReportsController(IReportService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetReports([FromQuery] ReportViewModel filter, [FromQuery] PagingRequest paging, [FromQuery] ReportOrderFilter orderFilter = ReportOrderFilter.CreateTime)
        {
            var rs = _service.GetReports(filter, paging, orderFilter);

            return rs.Results.IsNullOrEmpty() ? NotFound(rs) : Ok(rs);
        }
        
        [HttpGet("get-reports")]
        public async Task<IActionResult> GetReportsForAdmin([FromQuery] string? filter, [FromQuery] PagingRequest paging, [FromQuery] ReportOrderFilter orderFilter = ReportOrderFilter.CreateTime)
        {
            var rs = _service.GetReportsForAdmin(filter, paging, orderFilter);

            return rs.Results.IsNullOrEmpty() ? NotFound(rs) : Ok(rs);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateReport([FromBody] UpdateReportForMemberRequestModel request, Guid id)
        {
            var rs = await _service.UpdateReportByMemberAccount(id, request);

            return rs.result.HasValue ? (rs.result.Value ? Ok(rs) : BadRequest(rs)) : BadRequest(rs);
        }
        
        [HttpPut("update-status/{id:guid}")]
        public async Task<IActionResult> UpdateReportStatus([FromBody] ReportStatus request, Guid id)
        {
            var rs = await _service.UpdateStatusReport(id, request);

            return rs.result.HasValue ? (rs.result.Value ? Ok(rs) : BadRequest(rs)) : BadRequest(rs);
        }

        [HttpPost]
        public async Task<IActionResult> CreateReport([FromBody] CreateReportRequestModel requestModel)
        {
            var rs = await _service.AddReport(requestModel);

            return rs.result.HasValue ? (rs.result.Value ? Ok(rs) : BadRequest(rs)) : BadRequest(rs);
        }
    }
}
