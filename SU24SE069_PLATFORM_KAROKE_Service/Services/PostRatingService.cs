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
using SU24SE069_PLATFORM_KAROKE_Service.RequestModels.PostRating;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Service.Services
{
    public class PostRatingService : IPostRatingService
    {
        private readonly IMapper _mapper;
        private readonly IPostRatingRepository _repository;

        public PostRatingService(IMapper mapper, IPostRatingRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<ResponseResult<PostRatingViewModel>> CreateRating(PostRatingRequestModel request)
        {
            try
            {
                if(_repository.ExistedRating(request.MemberId, request.PostId))
                {
                    return new ResponseResult<PostRatingViewModel>()
                    {
                        Message = Constraints.INFORMATION_EXISTED,
                        result = false,
                    };
                }

                if(!await _repository.AddRating(_mapper.Map<PostRating>(request)))
                {
                    throw new Exception();
                }

            }catch(Exception)
            {
                return new ResponseResult<PostRatingViewModel>()
                {
                    Message = Constraints.CREATE_FAILED,
                    result = false,
                };
            }

            return new ResponseResult<PostRatingViewModel>()
            {
                Message = Constraints.CREATE_SUCCESS,
                result = true,
                Value = _mapper.Map<PostRatingViewModel>(request)
            };
        }

        public async Task<ResponseResult<PostRatingViewModel>> DeleteRating(Guid memberId, Guid postId)
        {
            try
            {
                if(!_repository.ExistedRating(memberId, postId))
                {
                    return new ResponseResult<PostRatingViewModel>()
                    {
                        Message = Constraints.NOT_FOUND,
                        result = false,
                    };
                }

                if(!await _repository.DeleteRating(memberId, postId))
                {
                    throw new Exception();
                }
            }catch(Exception)
            {
                return new ResponseResult<PostRatingViewModel>()
                {
                    Message = Constraints.DELETE_FAILED,
                    result = false
                };
            }

            return new ResponseResult<PostRatingViewModel>()
            {
                Message = Constraints.DELETE_SUCCESS,
                result = true
            };
        }

        public async Task<DynamicModelResponse.DynamicModelsResponse<PostRatingViewModel>> GetRatings(PostRatingViewModel filter, PagingRequest paging, RatingOrderFilter orderFliter)
        {
            (int, IQueryable<PostRatingViewModel>) result;
            try
            {
                var dat = _repository.GetAll(includeProperties: String.Join(",", SupportingFeature.GetNameIncludedProperties<PostRating>()))
                                     .ProjectTo<PostRatingViewModel>(_mapper.ConfigurationProvider)
                                     .DynamicFilter(filter);

                string? colName = Enum.GetName(typeof(RatingOrderFilter), orderFliter);

                result = SupportingFeature.Sorting(dat.AsEnumerable(), paging.OrderType, colName??"")
                                          .AsQueryable()
                                          .PagingIQueryable(paging.page, paging.pageSize, Constraints.DefaultPaging, Constraints.DefaultPage);
            }catch(Exception)
            {
                return new DynamicModelResponse.DynamicModelsResponse<PostRatingViewModel>()
                {
                    Message = Constraints.LOAD_FAILED,

                };
            }

            return new DynamicModelResponse.DynamicModelsResponse<PostRatingViewModel>()
            {
                Message = Constraints.INFORMATION,
                Metadata = new DynamicModelResponse.PagingMetadata()
                {
                    Page = paging.page,
                    Size = paging.pageSize,
                    Total = result.Item1
                },
                Results = result.Item2.ToList(),
            };
        }

        public async Task<ResponseResult<PostRatingViewModel>> UpdateRating(UpdatePostRatingRequestModel request, Guid memberId, Guid postId)
        {
            PostRating data = new PostRating();
            try
            {
                if(!_repository.ExistedRating(memberId, postId))
                {
                    return new ResponseResult<PostRatingViewModel>()
                    {
                        Message = Constraints.UPDATE_FAILED,
                        result = false,
                    };

                }

                data = _mapper.Map<PostRating>(request);
                data.MemberId = memberId;
                data.PostId = postId;

                if(!await _repository.UpdateRating(data))
                {
                    throw new Exception();
                }

            }catch(Exception)
            {
                return new ResponseResult<PostRatingViewModel>()
                {
                    Message = Constraints.UPDATE_FAILED,
                    result = false,
                };
            }

            return new ResponseResult<PostRatingViewModel>()
            {
                Message = Constraints.UPDATE_SUCCESS,
                result = true,
                Value = _mapper.Map<PostRatingViewModel>(data)
            };
        }
    }
}
