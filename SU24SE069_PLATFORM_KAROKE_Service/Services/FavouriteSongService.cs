using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.Commons;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.Helpers;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.ReponseModels.Helpers;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.RequestModels.Helpers;
using SU24SE069_PLATFORM_KAROKE_DataAccess.Models;
using SU24SE069_PLATFORM_KAROKE_Repository.IRepository;
using SU24SE069_PLATFORM_KAROKE_Repository.Repository;
using SU24SE069_PLATFORM_KAROKE_Service.IServices;
using SU24SE069_PLATFORM_KAROKE_Service.ReponseModels;
using SU24SE069_PLATFORM_KAROKE_Service.RequestModels.FavouriteSong;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Service.Services
{
    public class FavouriteSongService : IFavouriteSongSerivce
    {
        private readonly IMapper _mapper;
        private readonly IFavouriteSongRepository _repository;
        private readonly IPurchasedSongRepository _purchasedSongRepository;

        public FavouriteSongService(IMapper mapper, IFavouriteSongRepository repository, IPurchasedSongRepository purchasedSongRepository)
        {
            _mapper = mapper;
            _repository = repository;
            _purchasedSongRepository = purchasedSongRepository;
        }

        public async Task<ResponseResult<FavouriteSongViewModel>> CreateFavouteSong(CreateFavouriteSongRequestModel request)
        {
            try
            {
                if (_repository.ExistedFavouriteSong(songId: request.SongId, memberId: request.MemberId))
                {
                    return new ResponseResult<FavouriteSongViewModel>()
                    {
                        Message = Constraints.INFORMATION_EXISTED,
                        result = false,
                    };
                }

                if (!await _repository.CreateFavouriteSong(_mapper.Map<FavouriteSong>(request)))
                {
                    throw new Exception();
                }
            }
            catch (Exception)
            {
                return new ResponseResult<FavouriteSongViewModel>()
                {
                    Message = Constraints.CREATE_FAILED,
                    result = false,
                };
            }
            finally
            {
               await  _repository.DisponseAsync();
            }

            return new ResponseResult<FavouriteSongViewModel>()
            {
                Message = Constraints.CREATE_SUCCESS,
                result = true,
                Value = _mapper.Map<FavouriteSongViewModel>(request)
            };
        }

        public async Task<ResponseResult<FavouriteSongViewModel>> DeleteFavouteSong(CreateFavouriteSongRequestModel request)
        {
            try
            {
                if (!_repository.ExistedFavouriteSong(request.SongId, request.MemberId))
                {
                    return new ResponseResult<FavouriteSongViewModel>()
                    {
                        Message = Constraints.NOT_FOUND,
                        result = false,
                    };
                }


                var data = _mapper.Map<FavouriteSong>(request);

                if (!await _repository.DeleteFavouriteSong(data))
                {
                    throw new Exception();
                }

            }
            catch (Exception)
            {
                return new ResponseResult<FavouriteSongViewModel>()
                {
                    Message = Constraints.DELETE_FAILED,
                    result = false,
                };
            }
            finally
            {
                await _repository.DisponseAsync();
            }

            return new ResponseResult<FavouriteSongViewModel>()
            {
                Message = Constraints.DELETE_SUCCESS,
                result = true,
            };
        }

        public async Task<DynamicModelResponse.DynamicModelsResponse<FavouriteSongViewModel>> GetFavouriteSongs(FavouriteSongViewModel filter, PagingRequest paging, FavouriteSongOrderFilter orderFilter)
        {
            (int, IQueryable<FavouriteSongViewModel>) result;
            try
            {
                lock (_repository)
                {
                    var data = _repository.GetAll(
                                                includeProperties: String.Join(",",
                                                SupportingFeature.GetNameIncludedProperties<FavouriteSong>()))
                        .AsQueryable()
                        .ProjectTo<FavouriteSongViewModel>(_mapper.ConfigurationProvider)
                        .DynamicFilter(filter);

                    string? colName = Enum.GetName(typeof(FavouriteSongOrderFilter), orderFilter);

                    data = SupportingFeature.Sorting(data.AsEnumerable(), (SortOrder)paging.OrderType, colName).AsQueryable();

                    result = data.PagingIQueryable(paging.page, paging.pageSize,
                            Constraints.LimitPaging, Constraints.DefaultPaging);
                }


            }
            catch (Exception)
            {
                return new DynamicModelResponse.DynamicModelsResponse<FavouriteSongViewModel>()
                {
                    Message = Constraints.LOAD_FAILED,
                };
            }

            return new DynamicModelResponse.DynamicModelsResponse<FavouriteSongViewModel>()
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

        public async Task<DynamicModelResponse.DynamicModelsResponse<FavoriteSongDTO>> GetFavoriteSongsPurchasedFilter(FavouriteSongViewModel filter, PagingRequest paging, FavouriteSongOrderFilter orderFilter)
        {
            (int, IQueryable<FavouriteSongViewModel>) result;
            try
            {
                lock (_repository)
                {
                    var data = _repository.GetAll(
                                                includeProperties: String.Join(",",
                                                SupportingFeature.GetNameIncludedProperties<FavouriteSong>()))
                        .AsQueryable()
                        .ProjectTo<FavouriteSongViewModel>(_mapper.ConfigurationProvider)
                        .DynamicFilter(filter);

                    string? colName = Enum.GetName(typeof(FavouriteSongOrderFilter), orderFilter);

                    data = SupportingFeature.Sorting(data.AsEnumerable(), (SortOrder)paging.OrderType, colName).AsQueryable();

                    result = data.PagingIQueryable(paging.page, paging.pageSize,
                            Constraints.LimitPaging, Constraints.DefaultPaging);
                }
            }
            catch (Exception)
            {
                return new DynamicModelResponse.DynamicModelsResponse<FavoriteSongDTO>()
                {
                    Message = Constraints.LOAD_FAILED,
                };
            }

            var finalResult = new DynamicModelResponse.DynamicModelsResponse<FavoriteSongDTO>()
            {
                Message = Constraints.INFORMATION,
                Metadata = new DynamicModelResponse.PagingMetadata()
                {
                    Page = paging.page,
                    Size = paging.pageSize,
                    Total = result.Item1
                },
                Results = _mapper.Map<List<FavoriteSongDTO>>(result.Item2.ToList())
            };

            if (finalResult.Results != null && finalResult.Results.Count > 0) 
            {
                foreach (var song in finalResult.Results)
                {
                    song.IsPurchased = _purchasedSongRepository.GetDbSet().Any(s => s.SongId == song.SongId && s.MemberId == filter.MemberId);
                }
            }

            return finalResult;
        }
    }
}
