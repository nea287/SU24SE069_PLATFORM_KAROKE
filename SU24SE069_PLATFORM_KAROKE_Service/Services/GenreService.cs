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
using SU24SE069_PLATFORM_KAROKE_Service.RequestModels.Genre;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Service.Services
{
    public class GenreService : IGenreService
    {
        private readonly IMapper _mapper;
        private readonly IGenreRepository _repository;

        public GenreService(IMapper mapper, IGenreRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }
        public async Task<ResponseResult<GenreViewModel>> CreateGenre(GenreRequestModel request)
        {
            Genre rs = new Genre();
            try
            {
                rs = _mapper.Map<Genre>(request);

                if (!await _repository.AddGenre(rs))
                {
                    _repository.DetachEntity(rs);
                    throw new Exception();
                }
            }
            catch (Exception)
            {
                return new ResponseResult<GenreViewModel>()
                {
                    Message = Constraints.CREATE_FAILED,
                    result = false
                };
            }
            finally { lock (_repository) { } }

            return new ResponseResult<GenreViewModel>()
            {
                Message = Constraints.CREATE_SUCCESS,
                result = true,
                Value = _mapper.Map<GenreViewModel>(rs)
            };
        }

        public async Task<ResponseResult<GenreViewModel>> DeleteGenre(Guid id)
        {
            try
            {
                var data = await _repository.GetByIdGuid(id);
                if (data is null)
                {
                    return new ResponseResult<GenreViewModel>()
                    {
                        Message = Constraints.NOT_FOUND,
                        result = false,
                    };
                }

                if (!await _repository.DeleteGenre(data))
                {
                    _repository.DetachEntity(data);
                    throw new Exception();

                }
            }
            catch (Exception)
            {
                return new ResponseResult<GenreViewModel>()
                {
                    Message = Constraints.DELETE_FAILED,
                    result = false,
                };
            }

            return new ResponseResult<GenreViewModel>()
            {
                Message = Constraints.DELETE_SUCCESS,
                result = true
            };
        }

        public async Task<ResponseResult<GenreViewModel>> GetGenre(Guid id)
        {
            Genre rs = new Genre();
            try
            {
                rs = await _repository.GetByIdGuid(id);

                if (rs is null)
                {
                    return new ResponseResult<GenreViewModel>()
                    {
                        Message = Constraints.NOT_FOUND,
                        result = false,
                    };
                }


            }
            catch (Exception)
            {
                return new ResponseResult<GenreViewModel>()
                {
                    Message = Constraints.NOT_FOUND,
                    result = false,

                };
            }

            return new ResponseResult<GenreViewModel>()
            {
                Message = Constraints.INFORMATION,
                result = true,
                Value = _mapper.Map<GenreViewModel>(rs)
            };
        }

        public async Task<DynamicModelResponse.DynamicModelsResponse<GenreViewModel>> GetGenres(GenreViewModel filter, PagingRequest paging, GenreOrderFilter orderFilter)
        {
            (int, IQueryable<GenreViewModel>) result;
            try
            {
                lock (_repository)
                {
                    var data = _repository.GetAll(
                                                includeProperties: String.Join(",",
                                                SupportingFeature.GetNameIncludedProperties<Genre>()))
                        .AsQueryable()
                        .ProjectTo<GenreViewModel>(_mapper.ConfigurationProvider)
                        .DynamicFilter(filter);

                    string? colName = Enum.GetName(typeof(GenreOrderFilter), orderFilter);

                    data = SupportingFeature.Sorting(data.AsEnumerable(), (SortOrder)paging.OrderType, colName).AsQueryable();

                    result = data.PagingIQueryable(paging.page, paging.pageSize,
                            Constraints.LimitPaging, Constraints.DefaultPaging);
                }


            }
            catch (Exception)
            {
                return new DynamicModelResponse.DynamicModelsResponse<GenreViewModel>()
                {
                    Message = Constraints.LOAD_FAILED,
                };
            }

            return new DynamicModelResponse.DynamicModelsResponse<GenreViewModel>()
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
        
        public async Task<DynamicModelResponse.DynamicModelsResponse<GenreViewModel>> GetGenresForAdmin(string filter, PagingRequest paging, GenreOrderFilter orderFilter)
        {
            (int, IQueryable<GenreViewModel>) result;
            try
            {
                lock (_repository)
                {
                    var data = _repository.GetAll(
                                                includeProperties: String.Join(",",
                                                SupportingFeature.GetNameIncludedProperties<Genre>()))
                        .AsQueryable()
                        .ProjectTo<GenreViewModel>(_mapper.ConfigurationProvider)
                        .DynamicFilterForAdmin(filter);

                    string? colName = Enum.GetName(typeof(GenreOrderFilter), orderFilter);

                    data = SupportingFeature.Sorting(data.AsEnumerable(), (SortOrder)paging.OrderType, colName).AsQueryable();

                    result = data.PagingIQueryable(paging.page, paging.pageSize,
                            Constraints.LimitPaging, Constraints.DefaultPaging);
                }


            }
            catch (Exception)
            {
                return new DynamicModelResponse.DynamicModelsResponse<GenreViewModel>()
                {
                    Message = Constraints.LOAD_FAILED,
                };
            }

            return new DynamicModelResponse.DynamicModelsResponse<GenreViewModel>()
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

        public async Task<ResponseResult<GenreViewModel>> UpdateGenre(Guid id, GenreRequestModel request)
        {
            Genre rs = new Genre();
            try
            {
                var data = await _repository.GetByIdGuid(id);
                if (data is null)
                {
                    return new ResponseResult<GenreViewModel>()
                    {
                        Message = Constraints.NOT_FOUND,
                        result = false,
                    };
                }

                rs = _mapper.Map<Genre>(request);
                rs.GenreId = id;

                _repository.DetachEntity(data);
                _repository.MotifyEntity(rs);

                if (!await _repository.UpdateGenre(rs))
                {
                    _repository.DetachEntity(rs);
                    throw new Exception();
                }

            }
            catch (Exception)
            {
                return new ResponseResult<GenreViewModel>()
                {
                    Message = Constraints.UPDATE_FAILED,
                    result = false,
                };
            }
            finally { lock (_repository) { } }

            return new ResponseResult<GenreViewModel>()
            {
                Message = Constraints.UPDATE_SUCCESS,
                result = true,
                Value = _mapper.Map<GenreViewModel>(rs)
            };
        }
    }
}
