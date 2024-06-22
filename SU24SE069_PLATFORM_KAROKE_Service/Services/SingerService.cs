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
using SU24SE069_PLATFORM_KAROKE_Service.RequestModels.Singer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Service.Services
{
    public class SingerService : ISingerService
    {
        private readonly IMapper _mapper;
        private readonly ISingerRepository _repository;

        public SingerService(IMapper mapper, ISingerRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }
        public async Task<ResponseResult<SingerViewModel>> CreateSinger(SingerRequestModel request)
        {
            Singer rs = new Singer();
            try
            {
                rs = _mapper.Map<Singer>(request);

                if (!await _repository.AddSinger(rs))
                {
                    _repository.DetachEntity(rs);
                    throw new Exception();
                }
            }
            catch (Exception ex)
            {
                return new ResponseResult<SingerViewModel>()
                {
                    Message = Constraints.CREATE_FAILED,
                    result = false
                };
            }
            finally { lock (_repository) { } }

            return new ResponseResult<SingerViewModel>()
            {
                Message = Constraints.CREATE_SUCCESS,
                result = true,
                Value = _mapper.Map<SingerViewModel>(rs)
            };
        }

        public async Task<ResponseResult<SingerViewModel>> DeleteSinger(Guid id)
        {
            try
            {
                var data = await _repository.GetByIdGuid(id);
                if(data is null)
                {
                    return new ResponseResult<SingerViewModel>()
                    {
                        Message = Constraints.NOT_FOUND,
                        result = false,
                    };
                }

                if(!await _repository.DeleteSinger(data))
                {
                    _repository.DetachEntity(data);
                    throw new Exception();
                    
                }
            }catch(Exception ex)
            {
                return new ResponseResult<SingerViewModel>()
                {
                    Message = Constraints.DELETE_FAILED,
                    result = false,
                };
            }

            return new ResponseResult<SingerViewModel>()
            {
                Message = Constraints.DELETE_SUCCESS,
                result = true
            };
        }

        public async Task<ResponseResult<SingerViewModel>> GetSinger(Guid id)
        {
            Singer rs = new Singer();
            try
            {
                rs = await _repository.GetByIdGuid(id); 

                if(rs is null)
                {
                    return new ResponseResult<SingerViewModel>()
                    {
                        Message = Constraints.NOT_FOUND,
                        result = false,
                    };
                }

                
            }catch(Exception ex)
            {
                return new ResponseResult<SingerViewModel>()
                {
                    Message = Constraints.NOT_FOUND,
                    result = false,

                };
            }

            return new ResponseResult<SingerViewModel>()
            {
                Message = Constraints.INFORMATION,
                result = true,
                Value = _mapper.Map<SingerViewModel>(rs)
            };
        }

        public async Task<DynamicModelResponse.DynamicModelsResponse<SingerViewModel>> GetSingers(SingerViewModel filter, PagingRequest paging, SingerOrderFilter orderFilter)
        {
            (int, IQueryable<SingerViewModel>) result;
            try
            {
                lock (_repository)
                {
                    var data = _repository.GetAll(
                                                includeProperties: String.Join(",",
                                                SupportingFeature.GetNameIncludedProperties<Singer>()))
                        .AsQueryable()
                        .ProjectTo<SingerViewModel>(_mapper.ConfigurationProvider)
                        .DynamicFilter(filter);

                    string? colName = Enum.GetName(typeof(SingerOrderFilter), orderFilter);

                    data = SupportingFeature.Sorting(data.AsEnumerable(), (SortOrder)paging.OrderType, colName).AsQueryable();

                    result = data.PagingIQueryable(paging.page, paging.pageSize,
                            Constraints.LimitPaging, Constraints.DefaultPaging);
                }


            }
            catch (Exception ex)
            {
                return new DynamicModelResponse.DynamicModelsResponse<SingerViewModel>()
                {
                    Message = Constraints.LOAD_FAILED,
                };
            }

            return new DynamicModelResponse.DynamicModelsResponse<SingerViewModel>()
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

        public async Task<ResponseResult<SingerViewModel>> UpdateSinger(Guid id, SingerRequestModel request)
        {
            Singer rs = new Singer();
            try
            {
                var data = await _repository.GetByIdGuid(id);
                if (data is null)
                {
                    return new ResponseResult<SingerViewModel>()
                    {
                        Message = Constraints.NOT_FOUND,
                        result = false,
                    };
                }

                rs = _mapper.Map<Singer>(request);
                rs.SingerId = id;

                _repository.DetachEntity(data);
                _repository.MotifyEntity(rs);

                if (!await _repository.UpdateSinger(rs))
                {
                    _repository.DetachEntity(rs);
                    throw new Exception();
                }

            }
            catch (Exception ex)
            {
                return new ResponseResult<SingerViewModel>()
                {
                    Message = Constraints.UPDATE_FAILED,
                    result = false,
                };
            }
            finally { lock (_repository) { } }

            return new ResponseResult<SingerViewModel>()
            {
                Message = Constraints.UPDATE_SUCCESS,
                result = true,
                Value = _mapper.Map<SingerViewModel>(rs)
            };
        }
    }
}
