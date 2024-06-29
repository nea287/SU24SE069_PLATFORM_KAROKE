using AutoMapper;
using AutoMapper.QueryableExtensions;
using Castle.Core.Internal;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.Commons;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.Helpers;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.ReponseModels.Helpers;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.RequestModels.Helpers;
using SU24SE069_PLATFORM_KAROKE_DataAccess.Models;
using SU24SE069_PLATFORM_KAROKE_Repository.IRepository;
using SU24SE069_PLATFORM_KAROKE_Service.IServices;
using SU24SE069_PLATFORM_KAROKE_Service.ReponseModels;
using SU24SE069_PLATFORM_KAROKE_Service.RequestModels.PostRate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Service.Services
{
    public class PostRateService : IPostRateService
    {
        private readonly IMapper _mapper;
        private readonly IPostRateRepository _postRateRepository;

        public PostRateService(IMapper mapper, IPostRateRepository postRateRepository)
        {
            _mapper = mapper;
            _postRateRepository = postRateRepository;
        }
        public async Task<ResponseResult<PostRateViewModel>> CreatePostRate(CreatePostRateRequestModel request)
        {
            PostRateViewModel result = new PostRateViewModel();
            try
            {
                var data = _mapper.Map<PostRate>(request);

                if (!data.Reports.IsNullOrEmpty())
                {

                    data.Reports = data.Reports
                        .Select(item => { item.CreateTime = DateTime.Now; return item; })
                        .ToList();
                }

                if (!await _postRateRepository.AddPostRate(data))
                {
                    _postRateRepository.DetachEntity(data);
                    throw new Exception();
                }

                await _postRateRepository.SaveChagesAsync();
                result = _mapper.Map<PostRateViewModel>(data);
            }
            catch (Exception ex)
            {
                return new ResponseResult<PostRateViewModel>()
                {
                    Message = Constraints.CREATE_FAILED,
                    result = false,
                };
            }

            return new ResponseResult<PostRateViewModel>()
            {
                Message = Constraints.CREATE_SUCCESS,
                result = true,
                Value = result
            };
        }

        public async Task<ResponseResult<PostRateViewModel>> Delete(Guid id)
        {
            try
            {
                if (!_postRateRepository.ExistedPostRate(id))
                    return new ResponseResult<PostRateViewModel>()
                    {
                        Message = Constraints.NOT_FOUND,
                        result = false,
                    };

                if (!await _postRateRepository.DeletePostRate(id))
                {
                    throw new Exception();
                }
            }
            catch (Exception ex)
            {
                return new ResponseResult<PostRateViewModel>()
                {
                    Message = Constraints.DELETE_FAILED,
                    result = false,
                };
            }

            return new ResponseResult<PostRateViewModel>()
            {
                Message = Constraints.DELETE_SUCCESS,
                result = true,
            };
        }

        public async Task<DynamicModelResponse.DynamicModelsResponse<PostRateViewModel>> GetPostRates(PostRateViewModel filter, PagingRequest paging, PostRateOrderFilter orderFilter)
        {
            (int, IQueryable<PostRateViewModel>) result;
            try
            {
                lock (_postRateRepository)
                {
                    var data = _postRateRepository.GetAll(
                                                /*includeProperties: String.Join(",",
                                                SupportingFeature.GetNameIncludedProperties<PostRate>())*/)
                        .AsQueryable()
                        .ProjectTo<PostRateViewModel>(_mapper.ConfigurationProvider);
                        //.DynamicFilter(filter);

                    string? colName = Enum.GetName(typeof(PostRateOrderFilter), orderFilter);

                    data = SupportingFeature.Sorting(data.AsEnumerable(), (SortOrder)paging.OrderType, colName).AsQueryable();

                    result = data.PagingIQueryable(paging.page, paging.pageSize,
                            Constraints.LimitPaging, Constraints.DefaultPaging);
                }
            }
            catch (Exception ex)
            {
                return new DynamicModelResponse.DynamicModelsResponse<PostRateViewModel>()
                {
                    Message = Constraints.LOAD_FAILED,
                };
            }

            return new DynamicModelResponse.DynamicModelsResponse<PostRateViewModel>()
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

        public async Task<ResponseResult<PostRateViewModel>> UpdatePostRate(Guid id)
        {
            PostRateViewModel result = new PostRateViewModel();
            try
            {
                lock (_postRateRepository)
                {
                    var data1 = _postRateRepository.GetPostRate(id).Result;

                    if (data1 is null)
                    {
                        return new ResponseResult<PostRateViewModel>()
                        {
                            Message = Constraints.NOT_FOUND,
                            result = false,
                        };
                    }

                    var data = _mapper.Map<PostRate>(data1);

                    if (data == null)
                    {
                        return new ResponseResult<PostRateViewModel>()
                        {
                            Message = Constraints.NOT_FOUND,
                            result = false,
                            Value = _mapper.Map<PostRateViewModel>(data)
                        };
                    }

                    _postRateRepository.DetachEntity(data1);
                    _postRateRepository.MotifyEntity(data);

                    if (!_postRateRepository.UpdatePostRate(data1).Result)
                    {
                        _postRateRepository.DetachEntity(data);
                        throw new Exception();
                    }

                    result = _mapper.Map<PostRateViewModel>(data);
                };


            }
            catch (Exception ex)
            {
                return new ResponseResult<PostRateViewModel>()
                {
                    Message = Constraints.UPDATE_FAILED,
                    result = false,
                    Value = result
                };
            }

            return new ResponseResult<PostRateViewModel>()
            {
                Message = Constraints.UPDATE_SUCCESS,
                result = true,
                Value = result
            };
        }
    }
}
