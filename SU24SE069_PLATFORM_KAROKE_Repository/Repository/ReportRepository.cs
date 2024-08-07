using SU24SE069_PLATFORM_KAROKE_DataAccess.Models;
using SU24SE069_PLATFORM_KAROKE_Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Repository.Repository
{
    public class ReportRepository : BaseRepository<Report>, IReportRepository
    {
        public async Task<bool> AddReport(Report report)
        {
            try
            {
                await InsertAsync(report);
                await SaveChagesAsync();
            }catch (Exception)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> DeleteReport(Report report)
        {
            throw new Exception();
        }

        public async Task<bool> UpdateReport(Report report)
        {
            try
            {
                await UpdateGuid(report, report.ReportId);
                await SaveChagesAsync();
            }catch(Exception)
            {
                return false;
            }
            return true;
        }
    }
}
