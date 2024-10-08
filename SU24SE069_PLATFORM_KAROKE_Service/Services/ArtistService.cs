﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.Commons;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.Helpers;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.ReponseModels.Helpers;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.RequestModels.Helpers;
using SU24SE069_PLATFORM_KAROKE_DataAccess.Models;
using SU24SE069_PLATFORM_KAROKE_Repository.IRepository;
using SU24SE069_PLATFORM_KAROKE_Repository.Repository;
using SU24SE069_PLATFORM_KAROKE_Service.IServices;
using SU24SE069_PLATFORM_KAROKE_Service.ReponseModels;
using SU24SE069_PLATFORM_KAROKE_Service.RequestModels.Artist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Service.Services
{
    public class ArtistService : IArtistService
    {
        private readonly IMapper _mapper;
        private readonly IArtistRepository _repository;

        public ArtistService(IMapper mapper, IArtistRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }
        public async Task<ResponseResult<ArtistViewModel>> CreateArtist(ArtistRequestModel request)
        {
            Artist rs = new Artist();
            try
            {
                rs = _mapper.Map<Artist>(request);

                rs.Status = (int)GenreStatus.ACTIVE;

                if (!await _repository.AddArtist(rs))
                {
                    _repository.DetachEntity(rs);
                    throw new Exception();
                }
            }
            catch (Exception)
            {
                return new ResponseResult<ArtistViewModel>()
                {
                    Message = Constraints.CREATE_FAILED,
                    result = false
                };
            }
            finally
            {
                await _repository.DisponseAsync();
            }

            return new ResponseResult<ArtistViewModel>()
            {
                Message = Constraints.CREATE_SUCCESS,
                result = true,
                Value = _mapper.Map<ArtistViewModel>(rs)
            };
        }

        public async Task<ResponseResult<ArtistViewModel>> DeleteArtist(Guid id)
        {
            try
            {
                var data = await _repository.GetByIdGuid(id);
                if (data is null)
                {
                    return new ResponseResult<ArtistViewModel>()
                    {
                        Message = Constraints.NOT_FOUND,
                        result = false,
                    };
                }
                data.Status = (int)ArtistStatus.INACTIVE;
                data.ArtistId = id; 
                if (!await _repository.DeleteArtist(data))
                {
                    _repository.DetachEntity(data);
                    throw new Exception();

                }
            }
            catch (Exception)
            {
                return new ResponseResult<ArtistViewModel>()
                {
                    Message = Constraints.DELETE_FAILED,
                    result = false,
                };
            }finally { await _repository.DisponseAsync(); }

            return new ResponseResult<ArtistViewModel>()
            {
                Message = Constraints.DELETE_SUCCESS,
                result = true
            };
        }

        public async Task<ResponseResult<ArtistViewModel>> GetArtist(Guid id)
        {
            Artist rs = new Artist();
            try
            {
                rs = await _repository.GetByIdGuid(id);

                if (rs is null)
                {
                    return new ResponseResult<ArtistViewModel>()
                    {
                        Message = Constraints.NOT_FOUND,
                        result = false,
                    };
                }


            }
            catch (Exception)
            {
                return new ResponseResult<ArtistViewModel>()
                {
                    Message = Constraints.NOT_FOUND,
                    result = false,

                };
            }finally{ await _repository.DisponseAsync(); }

            return new ResponseResult<ArtistViewModel>()
            {
                Message = Constraints.INFORMATION,
                result = true,
                Value = _mapper.Map<ArtistViewModel>(rs)
            };
        }

        public async Task<DynamicModelResponse.DynamicModelsResponse<ArtistViewModel>> GetArtists(ArtistViewModel filter, PagingRequest paging, ArtistOrderFilter orderFilter)
        {
            (int, IQueryable<ArtistViewModel>) result;
            try
            {
                lock (_repository)
                {
                    var data = _repository.GetAll(
                                                includeProperties: String.Join(",",
                                                SupportingFeature.GetNameIncludedProperties<Artist>()))
                        .AsQueryable()
                        .ProjectTo<ArtistViewModel>(_mapper.ConfigurationProvider)
                        .DynamicFilter(filter);

                    string? colName = Enum.GetName(typeof(ArtistOrderFilter), orderFilter);

                    data = SupportingFeature.Sorting(data.AsEnumerable(), (SortOrder)paging.OrderType, colName).AsQueryable();

                    result = data.PagingIQueryable(paging.page, paging.pageSize,
                            Constraints.LimitPaging, Constraints.DefaultPaging);
                }


            }
            catch (Exception)
            {
                return new DynamicModelResponse.DynamicModelsResponse<ArtistViewModel>()
                {
                    Message = Constraints.LOAD_FAILED,
                };
            }
            finally
            {
                //await _repository.DisponseAsync();
            }

            return new DynamicModelResponse.DynamicModelsResponse<ArtistViewModel>()
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
        
        public async Task<DynamicModelResponse.DynamicModelsResponse<ArtistViewModel>> GetArtistsForAdmin(string? filter, PagingRequest paging, ArtistOrderFilter orderFilter)
        {
            (int, IQueryable<ArtistViewModel>) result;
            try
            {
                lock (_repository)
                {
                    var data = _repository.GetAll(
                                                includeProperties: String.Join(",",
                                                SupportingFeature.GetNameIncludedProperties<Artist>()))
                        .AsQueryable()
                        .ProjectTo<ArtistViewModel>(_mapper.ConfigurationProvider)
                        .DynamicFilterForAdmin(filter);

                    string? colName = Enum.GetName(typeof(ArtistOrderFilter), orderFilter);

                    data = SupportingFeature.Sorting(data.AsEnumerable(), (SortOrder)paging.OrderType, colName).AsQueryable();

                    result = data.PagingIQueryable(paging.page, paging.pageSize,
                            Constraints.LimitPaging, Constraints.DefaultPaging);
                }


            }
            catch (Exception)
            {
                return new DynamicModelResponse.DynamicModelsResponse<ArtistViewModel>()
                {
                    Message = Constraints.LOAD_FAILED,
                };
            }
            finally
            {
                //await _repository.DisponseAsync();
            }

            return new DynamicModelResponse.DynamicModelsResponse<ArtistViewModel>()
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

        public async Task<ResponseResult<ArtistViewModel>> UpdateArtist(Guid id, ArtistRequestModel request)
        {
            Artist rs = new Artist();
            try
            {
                var data = await _repository.GetByIdGuid(id);
                if (data is null)
                {
                    return new ResponseResult<ArtistViewModel>()
                    {
                        Message = Constraints.NOT_FOUND,
                        result = false,
                    };
                }



                request.GetType().GetProperties().Where(pro => pro.GetValue(request) == null)
                .ToList().ForEach(e =>
                {
                    if (e.PropertyType.IsEnum ||
                    (Nullable.GetUnderlyingType(e.PropertyType)?.IsEnum ?? false))
                {

                    Type? enumType = Type.GetType(Regex.Match(e.PropertyType.FullName, @"(SU24SE069_PLATFORM_KAROKE_BusinessLayer\.Commons[^,]*)").Value);
                    var value = Enum.GetName(enumType, data.GetType().GetProperty(e.Name)?.GetValue(data));

                    var value1 = Enum.Parse(enumType, value);
                    e.SetValue(request, value1);


            }
            else
            {
                e.SetValue(request, data.GetType().GetProperty(e.Name)?.GetValue(data));

            }   
        });


                rs = _mapper.Map<Artist>(request);
                rs.ArtistId = id;

                _repository.DetachEntity(data);
                _repository.MotifyEntity(rs);

                if (!await _repository.UpdateArtist(rs))
                {
                    _repository.DetachEntity(rs);
                    throw new Exception();
                }

            }
            catch (Exception)
            {
                return new ResponseResult<ArtistViewModel>()
                {
                    Message = Constraints.UPDATE_FAILED,
                    result = false,
                };
            }
            finally
            {
                await _repository.DisponseAsync();
            }

            return new ResponseResult<ArtistViewModel>()
            {
                Message = Constraints.UPDATE_SUCCESS,
                result = true,
                Value = _mapper.Map<ArtistViewModel>(rs)
            };
        }
    }
}
