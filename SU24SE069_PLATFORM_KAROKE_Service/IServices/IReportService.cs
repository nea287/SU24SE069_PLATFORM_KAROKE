using SU24SE069_PLATFORM_KAROKE_BusinessLayer.Commons;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.ReponseModels.Helpers;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.RequestModels.Helpers;
using SU24SE069_PLATFORM_KAROKE_Service.ReponseModels;
using SU24SE069_PLATFORM_KAROKE_Service.RequestModels.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Service.IServices
{
    public interface IReportService
    {
        public Task<ResponseResult<ReportViewModel>> GetReportById(Guid id);
        public DynamicModelResponse.DynamicModelsResponse<ReportViewModel> GetReports(ReportViewModel filter, PagingRequest paging, ReportOrderFilter orderFilter = ReportOrderFilter.CreateTime);
        public DynamicModelResponse.DynamicModelsResponse<ReportViewModel> GetReportsForAdmin(string? filter, PagingRequest paging, ReportOrderFilter orderFilter = ReportOrderFilter.CreateTime);
        public Task<ResponseResult<ReportViewModel>> AddReport(CreateReportRequestModel request);
        public Task<ResponseResult<ReportViewModel>> UpdateReportByMemberAccount(Guid reportId, UpdateReportForMemberRequestModel request);
        public Task<ResponseResult<ReportViewModel>> UpdateStatusReport(Guid id, ReportStatus status);

    }
}
