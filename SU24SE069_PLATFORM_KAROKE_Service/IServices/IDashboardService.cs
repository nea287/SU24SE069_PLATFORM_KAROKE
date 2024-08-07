using Org.BouncyCastle.Utilities;
using SU24SE069_PLATFORM_KAROKE_Service.ReponseModels;
using SU24SE069_PLATFORM_KAROKE_Service.RequestModels.Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Service.IServices
{
    public interface IDashboardService
    {
        public Task<DashboardResponse<SU24SE069_PLATFORM_KAROKE_BusinessLayer.Commons.Month>> GetDashboardGameByMonth(MonthRequestModel request);
        public Task<DashboardResponse<DateTime>> GetDashboardGamebyDate(DateRequestModel request);
        public Task<DashboardResponse<int>> GetDashboardGameByYear(YearRequestModel request);
        public Task<DashboardResponse<DateTime>> GetDashboardGameByTransaction(DateRequestModel request);
        
        public Task<DashboardResponse<SU24SE069_PLATFORM_KAROKE_BusinessLayer.Commons.Month>> GetDashboardMonetaryByMonth(MonthRequestModel request);
        public Task<DashboardResponse<DateTime>> GetDashboardMoneytarybyDate(DateRequestModel request);
        public Task<DashboardResponse<int>> GetDashboardMonetaryByYear(YearRequestModel request);
        public Task<DashboardResponse<DateTime>> GetDashboardByTransaction(DateRequestModel request);

    }
}
