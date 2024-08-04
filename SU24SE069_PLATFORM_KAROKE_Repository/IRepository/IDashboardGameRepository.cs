using SU24SE069_PLATFORM_KAROKE_DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Repository.IRepository
{
    public interface IDashboardGameRepository : IBaseRepository<InAppTransaction>
    {
        public Task<Dictionary<int, decimal>> GetDashboardByMonth(int? month = null, int? startMonth = null, int? endMonth = null, int? fromYear = null, int? toYear = null);
        public Task<Dictionary<DateTime, decimal>> GetDashboardByDate(DateTime? date = null, DateTime? startDate = null, DateTime? endDate = null);
        public Task<Dictionary<int, decimal>> GetDashboardByYear(int? year = null, int? fromYear = null, int? toYear = null);
    }
}
