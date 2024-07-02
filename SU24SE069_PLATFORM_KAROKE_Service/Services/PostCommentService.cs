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
using SU24SE069_PLATFORM_KAROKE_Service.RequestModels.Post;
using SU24SE069_PLATFORM_KAROKE_Service.RequestModels.PostComment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Service.Services
{
    public class PostCommentService : IPostCommentService
    {
        private readonly IMapper _mapper;
        private readonly IPostCommentRepository _repository;

        public PostCommentService(IMapper mapper, IPostCommentRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }
        public async Task<ResponseResult<PostCommentViewModel>> CreatePostComment(CreatePostCommentRequestModel requestModel)
        {
            PostComment cmt = new PostComment();    
            try
            {
                cmt = _mapper.Map<PostComment>(requestModel);
                cmt.UploadTime = DateTime.Now;
                cmt.Status = (int)PostCommentStatus.ACTIVE;


                if (!await _repository.CreateComment(cmt))
                {
                    throw new Exception();
                }
            }catch(Exception)
            {
                return new ResponseResult<PostCommentViewModel>()
                {
                    Message = Constraints.CREATE_FAILED,
                    result = false,
                 
                };
            }

            return new ResponseResult<PostCommentViewModel>()
            {
                Message = Constraints.CREATE_SUCCESS,
                result = true,
                Value = _mapper.Map<PostCommentViewModel>(cmt)
            };
        }

        public async Task<ResponseResult<PostCommentViewModel>> DeletePostComment(Guid id)
        {
            try
            {
                var data = await _repository.GetByIdGuid(id);

                if (data is null)
                {
                    _repository.DetachEntity(data);
                    return new ResponseResult<PostCommentViewModel>()
                    {
                        Message = Constraints.NOT_FOUND,
                        result = false,
                    };
                }

                data.Status = (int)PostCommentStatus.DEACTIVE;

                if (!await _repository.UpdateComment(data))
                {
                    _repository.DetachEntity(data);
                    throw new Exception();
                }

            }catch(Exception)
            {
                return new ResponseResult<PostCommentViewModel>()
                {
                    Message = Constraints.DELETE_FAILED,
                    result = false
                };
            }

            return new ResponseResult<PostCommentViewModel>()
            {
                Message = Constraints.DELETE_SUCCESS,
                result = true,
            };
        }

        public async Task<DynamicModelResponse.DynamicModelsResponse<PostCommentViewModel>> GetComments(PostCommentViewModel filter, PagingRequest paging, PostCommentFilter orderFilter)
        {
            (int, IQueryable<PostCommentViewModel>) result;
            try
            {
                var data = _repository.GetAll(includeProperties: String.Join(",", SupportingFeature.GetNameIncludedProperties<PostComment>()))
                                    .ProjectTo<PostCommentViewModel>(_mapper.ConfigurationProvider)
                                    .DynamicFilter(filter);

                string? colName = Enum.GetName(typeof(PostCommentFilter), orderFilter);

                data = SupportingFeature.Sorting(data, paging.OrderType, colName).AsQueryable();

                result = data.PagingIQueryable(paging.page, paging.pageSize, Constraints.DefaultPaging, Constraints.DefaultPage);

            }
            catch (Exception)
            {
                return new DynamicModelResponse.DynamicModelsResponse<PostCommentViewModel>()
                {
                    Message = Constraints.LOAD_FAILED,
                };
            }

            return new DynamicModelResponse.DynamicModelsResponse<PostCommentViewModel>()
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

        public async Task<ResponseResult<PostCommentViewModel>> UpdatePostComment(UpdatePostComment request, Guid id)
        {
            PostComment data = new PostComment();   
            try
            {
                data = await _repository.GetByIdGuid(id);
                if (data is null)
                {
                    return new ResponseResult<PostCommentViewModel>()
                    {
                        Message = Constraints.NOT_FOUND,
                        result = false
                    };
                }

                data.Comment = request.Comment;
                data.CommentId = id;

                if (!await _repository.UpdateComment(data))
                {
                    _repository.DetachEntity(data);
                    throw new Exception();
                }

            }catch(Exception)
            {
                return new ResponseResult<PostCommentViewModel>()
                {
                    Message = Constraints.UPDATE_FAILED,
                    result = false
                };
            }

            return new ResponseResult<PostCommentViewModel>()
            {
                Message = Constraints.UPDATE_SUCCESS,
                result = true,
            };
        }
    }
}
