using AutoMapper;
using AutoMapper.QueryableExtensions;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.Commons;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.Helpers;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.ReponseModels.Helpers;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.RequestModels.Helpers;
using SU24SE069_PLATFORM_KAROKE_DataAccess.Models;
using SU24SE069_PLATFORM_KAROKE_Repository.IRepository;
using SU24SE069_PLATFORM_KAROKE_Service.IServices;
using SU24SE069_PLATFORM_KAROKE_Service.ReponseModels;
using SU24SE069_PLATFORM_KAROKE_Service.RequestModels.LoginActivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Service.Services
{
    public class LoginActivityService : ILoginActivityService
    {
        private readonly IMapper _mapper;
        private readonly ILoginActivityRepository _repository;

        public LoginActivityService(IMapper mapper, ILoginActivityRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }
        public async Task<ResponseResult<LoginActivityViewModel>> CreateActivity(LoginActivityRequestModel request)
        {
            LoginActivity rs = new LoginActivity();
            try
            {
                rs = _mapper.Map<LoginActivity>(request);

                rs.LoginTime = DateTime.Now;

                if (!await _repository.CreateActivity(rs))
                {
                    _repository.DetachEntity(rs);
                    throw new Exception();
                }
            }
            catch (Exception ex)
            {
                return new ResponseResult<LoginActivityViewModel>()
                {
                    Message = Constraints.CREATE_FAILED,
                    result = false
                };
            }
            finally { lock (_repository) { } }

            return new ResponseResult<LoginActivityViewModel>()
            {
                Message = Constraints.CREATE_SUCCESS,
                result = true,
                Value = _mapper.Map<LoginActivityViewModel>(rs)
            };
        }

        public async Task<DynamicModelResponse.DynamicModelsResponse<LoginActivityViewModel>> GetActivites(LoginActivityViewModel filter, PagingRequest paging, LoginActivityOrderFilter orderFilter)
        {
            (int, IQueryable<LoginActivityViewModel>) result;
            try
            {
                lock (_repository)
                {
                    var data = _repository.GetAll(
                                                includeProperties: String.Join(",",
                                                SupportingFeature.GetNameIncludedProperties<LoginActivity>()))
                        .AsQueryable()
                        .ProjectTo<LoginActivityViewModel>(_mapper.ConfigurationProvider)
                        .DynamicFilter(filter);

                    string? colName = Enum.GetName(typeof(LoginActivityOrderFilter), orderFilter);

                    data = SupportingFeature.Sorting(data.AsEnumerable(), (SortOrder)paging.OrderType, colName).AsQueryable();

                    result = data.PagingIQueryable(paging.page, paging.pageSize,
                            Constraints.LimitPaging, Constraints.DefaultPaging);
                }


            }
            catch (Exception ex)
            {
                return new DynamicModelResponse.DynamicModelsResponse<LoginActivityViewModel>()
                {
                    Message = Constraints.LOAD_FAILED,
                };
            }

            return new DynamicModelResponse.DynamicModelsResponse<LoginActivityViewModel>()
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
    }
}
