﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using Castle.Core.Internal;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.Commons;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.Helpers;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.ReponseModels.Helpers;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.RequestModels.Helpers;
using SU24SE069_PLATFORM_KAROKE_DataAccess.Models;
using SU24SE069_PLATFORM_KAROKE_Repository.IRepository;
using SU24SE069_PLATFORM_KAROKE_Service.Filters;
using SU24SE069_PLATFORM_KAROKE_Service.IServices;
using SU24SE069_PLATFORM_KAROKE_Service.ReponseModels;
using SU24SE069_PLATFORM_KAROKE_Service.RequestModels.Post;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Service.Services
{
    public class PostService : IPostService
    {
        private readonly IMapper _mapper;
        private readonly IPostRepository _postRepository;

        public PostService(IMapper mapper, IPostRepository postRepository)
        {
            _mapper = mapper;
            _postRepository = postRepository;
        }


        public async Task<DynamicModelResponse.DynamicModelsResponse<PostAdminViewModel>> GetPostAdmins(PostFilter filter, PagingRequest paging, PostOrderFilter orderFilter)
        {
            (int, IQueryable<PostAdminViewModel>) result;
            try
            {
                lock (_postRepository)
                {
                    var data1 = _postRepository.UpdateScores(null).Result;
                    var data = _mapper.Map<List<PostAdminViewModel>>(data1).AsQueryable()
                        .DynamicFilter(_mapper.Map<PostAdminViewModel>(filter));


                    string? colName = Enum.GetName(typeof(PostOrderFilter), orderFilter);

                    data = SupportingFeature.Sorting(data.AsEnumerable(), (SortOrder)paging.OrderType, colName).AsQueryable();

                    result = data.AsQueryable().PagingIQueryable(paging.page, paging.pageSize,
                            Constraints.LimitPaging, Constraints.DefaultPaging);

                }
            }
            catch (Exception)
            {
                return new DynamicModelResponse.DynamicModelsResponse<PostAdminViewModel>()
                {
                    Message = Constraints.LOAD_FAILED,
                };
            }

            return new DynamicModelResponse.DynamicModelsResponse<PostAdminViewModel>()
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

        public async Task<ResponseResult<PostViewModel>> CreatePost(CreatePostRequestModel request)
        {
            PostViewModel result = new PostViewModel();
            try
            {

                var data = _mapper.Map<Post>(request);

                data.UploadTime = DateTime.Now;
                data.UpdateTime = DateTime.Now;

                data.Status = (int)PostStatus.ACTIVE;

                if (!data.PostShares.IsNullOrEmpty())
                {

                    data.PostShares = data.PostShares
                        .Select(item => { item.UpdateTime = DateTime.Now; item.ShareTime = DateTime.Now; return item; })
                        .ToList();
                }



                else if (!data.Reports.IsNullOrEmpty())
                {

                    data.Reports = data.Reports
                        .Select(item => { item.CreateTime = DateTime.Now; return item; })
                        .ToList();
                }

                if (!await _postRepository.AddPost(data))
                {
                    _postRepository.DetachEntity(data);
                    throw new Exception();
                }

                await _postRepository.SaveChagesAsync();
                result = _mapper.Map<PostViewModel>(data);
            }
            catch (Exception)
            {
                return new ResponseResult<PostViewModel>()
                {
                    Message = Constraints.CREATE_FAILED,
                    result = false,
                };
            }

            return new ResponseResult<PostViewModel>()
            {
                Message = Constraints.CREATE_SUCCESS,
                result = true,
                Value = result
            };
        }

        public async Task<ResponseResult<PostViewModel>> Delete(Guid id)
        {
            try
            {
                var data = await _postRepository.GetPost(id);
                if (data == null)
                    return new ResponseResult<PostViewModel>()
                    {
                        Message = Constraints.NOT_FOUND,
                        result = false,
                    };

                _postRepository.MotifyEntity(data);

                data.PostId = id;

                data.Status = (int)PostStatus.TEMPORARY_DISABLE;

                if (!await _postRepository.UpdatePost(data))
                {
                    throw new Exception();
                }
            }
            catch (Exception)
            {
                return new ResponseResult<PostViewModel>()
                {
                    Message = Constraints.DELETE_FAILED,
                    result = false,
                };
            }

            return new ResponseResult<PostViewModel>()
            {
                Message = Constraints.DELETE_SUCCESS,
                result = true,
            };
        }

        

        public async Task<ResponseResult<PostViewModel>> UploadScore(Guid id)
        {
            PostViewModel result = new PostViewModel();
            try
            {
                lock (_postRepository)
                {
                    var data1 = _postRepository.GetPost(id).Result;

                    if (data1 is null)
                    {
                        return new ResponseResult<PostViewModel>()
                        {
                            Message = Constraints.NOT_FOUND,
                            result = false,
                        };
                    }

                    var data = _mapper.Map<Post>(data1);

                    data.UpdateTime = DateTime.Now;
                    data.Score =  _postRepository.GetPostScore(id).Result;


                    if (data == null)
                    {
                        return new ResponseResult<PostViewModel>()
                        {
                            Message = Constraints.NOT_FOUND,
                            result = false,
                            Value = _mapper.Map<PostViewModel>(data)
                        };
                    }

                    _postRepository.DetachEntity(data1);
                    _postRepository.MotifyEntity(data);

                    if (!_postRepository.UpdatePost(data1).Result)
                    {
                        _postRepository.DetachEntity(data);
                        throw new Exception();
                    }

                    result = _mapper.Map<PostViewModel>(data);
                };


            }
            catch (Exception)
            {
                return new ResponseResult<PostViewModel>()
                {
                    Message = Constraints.UPDATE_FAILED,
                    result = false,
                    Value = result
                };
            }

            return new ResponseResult<PostViewModel>()
            {
                Message = Constraints.UPDATE_SUCCESS,
                result = true,
                Value = result
            };
        }
        public async Task<DynamicModelResponse.DynamicModelsResponse<PostViewModel>> GetPosts(PostFilter filter, PagingRequest paging, PostOrderFilter orderFilter)
        {
            (int, IQueryable<PostViewModel>) result;
            try
            {
                lock (_postRepository)
                {
                    var data1 = _postRepository.UpdateScores(
                                                 "InverseOriginPost,PostRatings").Result

                     ;
                    var data = _mapper.Map<List<PostViewModel>>(data1).AsQueryable()
                        .DynamicFilter(_mapper.Map<PostViewModel>(filter));




                    string? colName = Enum.GetName(typeof(PostOrderFilter), orderFilter);

                    data = SupportingFeature.Sorting(data.AsEnumerable(), (SortOrder)paging.OrderType, colName).AsQueryable();

                    result = data.AsQueryable().PagingIQueryable(paging.page, paging.pageSize,
                            Constraints.LimitPaging, Constraints.DefaultPaging);

                }
            }
            catch (Exception)
            {
                return new DynamicModelResponse.DynamicModelsResponse<PostViewModel>()
                {
                    Message = Constraints.LOAD_FAILED,
                };
            }

            return new DynamicModelResponse.DynamicModelsResponse<PostViewModel>()
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

        public async Task<ResponseResult<PostViewModel>> GetAveragedScoreByIdOrignPost(Guid id)
        {
            PostViewModel result = new PostViewModel();
            try
            {
                lock (_postRepository)
                {
                    result = _mapper.Map<PostViewModel>(_postRepository.GetPostOrign(id).Result);
                }
            }catch(Exception)
            {
                return new ResponseResult<PostViewModel>()
                {
                    Message = Constraints.LOAD_FAILED,
                    result = false
                };
            }

            return new ResponseResult<PostViewModel>()
            {
                Message = Constraints.INFORMATION,
                Value = result,
                result = true
            };
        }


        public async Task<ResponseResult<PostViewModel>> ChangeStatus(Guid id, PostStatus status)
        {
            PostViewModel result = new PostViewModel();
            try
            {
                lock (_postRepository)
                {
                    var data1 = _postRepository.GetPost(id).Result;

                    if (data1 is null)
                    {
                        return new ResponseResult<PostViewModel>()
                        {
                            Message = Constraints.NOT_FOUND,
                            result = false,
                        };
                    }

                    var data = _mapper.Map<Post>(data1);

                    data.UpdateTime = DateTime.Now;
                    data.Status = (int)status;

                    if (data == null)
                    {
                        return new ResponseResult<PostViewModel>()
                        {
                            Message = Constraints.NOT_FOUND,
                            result = false,
                            Value = _mapper.Map<PostViewModel>(data)
                        };
                    }

                    _postRepository.DetachEntity(data1);
                    _postRepository.MotifyEntity(data);

                    if (!_postRepository.UpdatePost(data1).Result)
                    {
                        _postRepository.DetachEntity(data);
                        throw new Exception();
                    }

                    result = _mapper.Map<PostViewModel>(data);
                };


            }
            catch (Exception)
            {
                return new ResponseResult<PostViewModel>()
                {
                    Message = Constraints.UPDATE_FAILED,
                    result = false,
                    Value = result
                };
            }

            return new ResponseResult<PostViewModel>()
            {
                Message = Constraints.UPDATE_SUCCESS,
                result = true,
                Value = result
            };
        }

        public async Task<ResponseResult<PostViewModel>> UpdatePost(Guid id, string? caption)
        {
            PostViewModel result = new PostViewModel();
            try
            {
                lock (_postRepository)
                {
                    var data1 = _postRepository.GetPost(id).Result;

                    if (data1 is null)
                    {
                        return new ResponseResult<PostViewModel>()
                        {
                            Message = Constraints.NOT_FOUND,
                            result = false,
                        };
                    }

                    var data = _mapper.Map<Post>(data1);

                    data.UpdateTime = DateTime.Now;
                    data.Caption = caption??data.Caption;

                    if (data == null)
                    {
                        return new ResponseResult<PostViewModel>()
                        {
                            Message = Constraints.NOT_FOUND,
                            result = false,
                            Value = _mapper.Map<PostViewModel>(data)
                        };
                    }

                    _postRepository.DetachEntity(data1);
                    _postRepository.MotifyEntity(data);

                    if (!_postRepository.UpdatePost(data1).Result)
                    {
                        _postRepository.DetachEntity(data);
                        throw new Exception();
                    }

                    result = _mapper.Map<PostViewModel>(data);
                };


            }
            catch (Exception)
            {
                return new ResponseResult<PostViewModel>()
                {
                    Message = Constraints.UPDATE_FAILED,
                    result = false,
                    Value = result
                };
            }

            return new ResponseResult<PostViewModel>()
            {
                Message = Constraints.UPDATE_SUCCESS,
                result = true,
                Value = result
            };
        }


    }
}
