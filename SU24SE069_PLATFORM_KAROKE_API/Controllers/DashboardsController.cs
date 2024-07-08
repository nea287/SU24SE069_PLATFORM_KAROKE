using Castle.Core.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SU24SE069_PLATFORM_KAROKE_Service.IServices;
using SU24SE069_PLATFORM_KAROKE_Service.RequestModels.Dashboard;

namespace SU24SE069_PLATFORM_KAROKE_API.Controllers
{
    [Route("api/dashboards")]
    [ApiController]
    public class DashboardsController : ControllerBase
    {
        private readonly IDashboardService _service;

        public DashboardsController(IDashboardService service)
        {
            _service = service;
        }

        [HttpGet("get-date-transactions")]
        public async Task<IActionResult> GetDashboardDateTransactions([FromQuery] DateRequestModel request)
        {
            var rs = await _service.GetDashboardMoneytarybyDate(request);

            return rs.Values.IsNullOrEmpty()? BadRequest(rs) : Ok(rs);
        }

        [HttpGet("get-month-transactions")]
        public async Task<IActionResult> GetDashboardMonthTransactions([FromQuery] MonthRequestModel request)
        {
            var rs = await _service.GetDashboardMonetaryByMonth(request);

            return rs.Values.IsNullOrEmpty() ? BadRequest(rs) : Ok(rs);
        }
        
        [HttpGet("get-year-transactions")]
        public async Task<IActionResult> GetDashboardYearTransactions([FromQuery] YearRequestModel request)
        {
            var rs = await _service.GetDashboardMonetaryByYear(request);

            return rs.Values.IsNullOrEmpty() ? BadRequest(rs) : Ok(rs);
        }
        
        [HttpGet("get-date-game-transactions")]
        public async Task<IActionResult> GetDashboardDateGametransactions([FromQuery] DateRequestModel request)
        {
            var rs = await _service.GetDashboardGamebyDate(request);

            return rs.Values.IsNullOrEmpty() ? BadRequest(rs) : Ok(rs);
        }
        
        [HttpGet("get-month-game-transactions")]
        public async Task<IActionResult> GetDashboardMonthGametransactions([FromQuery] MonthRequestModel request)
        {
            var rs = await _service.GetDashboardGameByMonth(request);

            return rs.Values.IsNullOrEmpty() ? BadRequest(rs) : Ok(rs);
        }
        
        [HttpGet("get-year-game-transactions")]
        public async Task<IActionResult> GetDashboardYearGametransactions([FromQuery] YearRequestModel request)
        {
            var rs = await _service.GetDashboardGameByYear(request);

            return rs.Values.IsNullOrEmpty() ? BadRequest(rs) : Ok(rs);
        }

    }
}
