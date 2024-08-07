using SU24SE069_PLATFORM_KAROKE_DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Repository.IRepository
{
    public interface IReportRepository : IBaseRepository<Report>
    {
        public Task<bool> AddReport(Report report);
        public Task<bool> DeleteReport(Report report);
        public Task<bool> UpdateReport(Report report);
    }
}
