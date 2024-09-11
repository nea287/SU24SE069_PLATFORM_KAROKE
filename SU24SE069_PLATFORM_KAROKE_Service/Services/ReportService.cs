using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.Ocsp;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.Commons;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.Helpers;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.ReponseModels.Helpers;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.RequestModels.Helpers;
using SU24SE069_PLATFORM_KAROKE_DataAccess.Models;
using SU24SE069_PLATFORM_KAROKE_Repository.IRepository;
using SU24SE069_PLATFORM_KAROKE_Service.IServices;
using SU24SE069_PLATFORM_KAROKE_Service.ReponseModels;
using SU24SE069_PLATFORM_KAROKE_Service.RequestModels.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Service.Services
{
    public class ReportService : IReportService
    {
        private readonly IMapper _mapper;
        private readonly IReportRepository _repository;

        public ReportService(IMapper mapper, IReportRepository reportRepository)
        {
            _mapper = mapper;
            _repository = reportRepository;
        }
        public async Task<ResponseResult<ReportViewModel>> AddReport(CreateReportRequestModel request)
        {
            Report rs = new Report();
            try
            {
                rs = _mapper.Map<Report>(request);

                rs.Status = (int)ReportStatus.PROCCESSING;
                rs.CreateTime = DateTime.Now;

                if (!await _repository.AddReport(rs))
                {
                    _repository.DetachEntity(rs);
                    throw new Exception();
                }
            }
            catch (Exception)
            {
                return new ResponseResult<ReportViewModel>()
                {
                    Message = Constraints.CREATE_FAILED,
                    result = false
                };
            }
            finally { lock (_repository) { } }

            return new ResponseResult<ReportViewModel>()
            {
                Message = Constraints.CREATE_SUCCESS,
                result = true,
                Value = _mapper.Map<ReportViewModel>(rs)
            };
        }

        public async Task<ResponseResult<ReportViewModel>> GetReportById(Guid id)
        {
            Report rs = new Report();
            try
            {
                rs = await _repository.GetByIdGuid(id);

                if (rs is null)
                {
                    return new ResponseResult<ReportViewModel>()
                    {
                        Message = Constraints.NOT_FOUND,
                        result = false,
                    };
                }


            }
            catch (Exception)
            {
                return new ResponseResult<ReportViewModel>()
                {
                    Message = Constraints.NOT_FOUND,
                    result = false,

                };
            }

            return new ResponseResult<ReportViewModel>()
            {
                Message = Constraints.INFORMATION,
                result = true,
                Value = _mapper.Map<ReportViewModel>(rs)
            };
        }

        public DynamicModelResponse.DynamicModelsResponse<ReportViewModel> GetReports(ReportViewModel filter, PagingRequest paging, ReportOrderFilter orderFilter = ReportOrderFilter.CreateTime)
        {
            (int, IQueryable<ReportViewModel>) result;
            try
            {
                var data = _repository.GetAll(includeProperties: string.Join(",", SupportingFeature.GetNameIncludedProperties<Report>()))
                                      .ProjectTo<ReportViewModel>(_mapper.ConfigurationProvider).DynamicFilter(filter);
                                      //.PagingIQueryable(paging.page, paging.pageSize, Constraints.DefaultPaging, Constraints.DefaultPage);

                string? collName = Enum.GetName(typeof(ReportOrderFilter), orderFilter);

                data = SupportingFeature.Sorting(data.AsEnumerable(), (SortOrder)paging.OrderType, collName).AsQueryable();

                result = data.PagingIQueryable(paging.page, paging.pageSize, Constraints.LimitPaging, Constraints.DefaultPaging);

                if(result.Item2 == null)
                {
                    return new DynamicModelResponse.DynamicModelsResponse<ReportViewModel>()
                    {
                        Message = Constraints.NOT_FOUND,

                    };
                }
            }
            catch (Exception)
            {
                return new DynamicModelResponse.DynamicModelsResponse<ReportViewModel>()
                {
                    Message = Constraints.LOAD_FAILED,

                };
            }

            return new DynamicModelResponse.DynamicModelsResponse<ReportViewModel>()
            {
                Message = Constraints.INFORMATION,
                Metadata = new DynamicModelResponse.PagingMetadata()
                {
                    Page = paging.page,
                    Size = paging.pageSize,
                    Total = result.Item1
                },
                Results = result.Item2.ToList()

            };
        }

        public DynamicModelResponse.DynamicModelsResponse<ReportViewModel> GetReportsForAdmin(string? filter, PagingRequest paging, ReportOrderFilter orderFilter = ReportOrderFilter.CreateTime)
        {
            (int, IQueryable<ReportViewModel>) result;
            try
            {
                var data = _repository.GetAll(includeProperties: string.Join(",", SupportingFeature.GetNameIncludedProperties<Report>()))
                                      .ProjectTo<ReportViewModel>(_mapper.ConfigurationProvider).DynamicFilterForAdmin(filter);
                //.PagingIQueryable(paging.page, paging.pageSize, Constraints.DefaultPaging, Constraints.DefaultPage);

                string? collName = Enum.GetName(typeof(ReportOrderFilter), orderFilter);

                data = SupportingFeature.Sorting(data.AsEnumerable(), (SortOrder)paging.OrderType, collName).AsQueryable();

                result = data.PagingIQueryable(paging.page, paging.pageSize, Constraints.LimitPaging, Constraints.DefaultPaging);

                if (result.Item2 == null)
                {
                    return new DynamicModelResponse.DynamicModelsResponse<ReportViewModel>()
                    {
                        Message = Constraints.NOT_FOUND,

                    };
                }
            }
            catch (Exception)
            {
                return new DynamicModelResponse.DynamicModelsResponse<ReportViewModel>()
                {
                    Message = Constraints.LOAD_FAILED,

                };
            }

            return new DynamicModelResponse.DynamicModelsResponse<ReportViewModel>()
            {
                Message = Constraints.INFORMATION,
                Metadata = new DynamicModelResponse.PagingMetadata()
                {
                    Page = paging.page,
                    Size = paging.pageSize,
                    Total = result.Item1
                },
                Results = result.Item2.ToList()

            };
        }

        public async Task<ResponseResult<ReportViewModel>> UpdateReportByMemberAccount(Guid reportId, UpdateReportForMemberRequestModel request)
        {
            Report rs = new Report();
            try
            {
                rs = await _repository.GetByIdGuid(reportId);
                if (rs is null)
                {
                    return new ResponseResult<ReportViewModel>()
                    {
                        Message = Constraints.NOT_FOUND,
                        result = false,
                    };
                }

                //rs = _mapper.Map<Report>(request);
                //rs.ReportId = reportId;

                //_repository.DetachEntity(data);
                rs.ReportCategory = (int)request.ReportCategory;
                rs.Reason = request.Reason;
                rs.ReportType = (int)request.ReportType;

                _repository.MotifyEntity(rs);

                if (!await _repository.UpdateReport(rs))
                {
                    _repository.DetachEntity(rs);
                    throw new Exception();
                }

            }
            catch (Exception)
            {
                return new ResponseResult<ReportViewModel>()
                {
                    Message = Constraints.UPDATE_FAILED,
                    result = false,
                };
            }
            finally { lock (_repository) { } }

            return new ResponseResult<ReportViewModel>()
            {
                Message = Constraints.UPDATE_SUCCESS,
                result = true,
                Value = _mapper.Map<ReportViewModel>(rs)
            };
        }

        public async Task<ResponseResult<ReportViewModel>> UpdateStatusReport(Guid id, ReportStatus status)
        {
            Report rs = new Report();
            try
            {
                rs = await _repository.GetByIdGuid(id);
                if (rs is null)
                {
                    return new ResponseResult<ReportViewModel>()
                    {
                        Message = Constraints.NOT_FOUND,
                        result = false,
                    };
                }

                //rs = _mapper.Map<Report>(request);
                rs.ReportId = id;

                //_repository.DetachEntity(data);
                rs.Status = (int)status;

                _repository.MotifyEntity(rs);

                if (!await _repository.UpdateReport(rs))
                {
                    _repository.DetachEntity(rs);
                    throw new Exception();
                }

            }
            catch (Exception)
            {
                return new ResponseResult<ReportViewModel>()
                {
                    Message = Constraints.UPDATE_FAILED,
                    result = false,
                };
            }
            finally { lock (_repository) { } }

            return new ResponseResult<ReportViewModel>()
            {
                Message = Constraints.UPDATE_SUCCESS,
                result = true,
                Value = _mapper.Map<ReportViewModel>(rs)
            };
        }
    }
}
