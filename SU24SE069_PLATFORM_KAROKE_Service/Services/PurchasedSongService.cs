using AutoMapper;
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
using SU24SE069_PLATFORM_KAROKE_Service.RequestModels.PurchasedSong;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Service.Services
{
    public class PurchasedSongService : IPurchasedSongService
    {
        private readonly IPurchasedSongRepository _repository;
        private readonly IMapper _mapper;
        private readonly IFavouriteSongRepository _favoriteSongRepository;

        public PurchasedSongService(IPurchasedSongRepository repository, IMapper mapper, IFavouriteSongRepository favoriteSongRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _favoriteSongRepository = favoriteSongRepository;
        }
        public async Task<DynamicModelResponse.DynamicModelsResponse<PurchasedSongViewModel>> GetPurchasedSongs(PurchasedSongViewModel filter, PagingRequest paging, PurchasedSongOrderFilter orderFilter)
        {
            (int, IQueryable<PurchasedSongViewModel>) result;
            try
            {
                lock (_repository)
                {
                    var data = _repository.GetAll(
                                                includeProperties: String.Join(",",
                                                SupportingFeature.GetNameIncludedProperties<PurchasedSong>()))
                        .AsQueryable()
                        .ProjectTo<PurchasedSongViewModel>(_mapper.ConfigurationProvider)
                        .DynamicFilter(filter);

                    string? colName = Enum.GetName(typeof(PurchasedSongOrderFilter), orderFilter);

                    data = SupportingFeature.Sorting(data.AsEnumerable(), (SortOrder)paging.OrderType, colName).AsQueryable();

                    result = data.PagingIQueryable(paging.page, paging.pageSize,
                            Constraints.LimitPaging, Constraints.DefaultPaging);
                }


            }
            catch (Exception)
            {
                return new DynamicModelResponse.DynamicModelsResponse<PurchasedSongViewModel>()
                {
                    Message = Constraints.LOAD_FAILED,
                };
            }

            return new DynamicModelResponse.DynamicModelsResponse<PurchasedSongViewModel>()
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

        //public async Task<ResponseResult<PurchasedSongViewModel>> PurchageSong(PurchasedSongRequestModel request)
        //{
        //    try
        //    {
        //        if (_repository.CheckPurchasedSong(request.MemberId, request.SongId))
        //        {
        //            return new ResponseResult<PurchasedSongViewModel>()
        //            {
        //                Message = Constraints.INFORMATION_EXISTED
        //            };
        //        }


        //    }
        //    catch (Exception)
        //    {

        //    }
        //}

        public async Task<DynamicModelResponse.DynamicModelsResponse<PurchasedSongDTO>> GetPurchasedSongFavoriteFilter(PurchasedSongViewModel filter, PagingRequest paging, PurchasedSongOrderFilter orderFilter)
        {
            (int, IQueryable<PurchasedSongViewModel>) result;
            try
            {
                lock (_repository)
                {
                    var data = _repository.GetAll(
                                                includeProperties: String.Join(",",
                                                SupportingFeature.GetNameIncludedProperties<PurchasedSong>()))
                        .AsQueryable()
                        .ProjectTo<PurchasedSongViewModel>(_mapper.ConfigurationProvider)
                        .DynamicFilter(filter);

                    string? colName = Enum.GetName(typeof(PurchasedSongOrderFilter), orderFilter);

                    data = SupportingFeature.Sorting(data.AsEnumerable(), (SortOrder)paging.OrderType, colName).AsQueryable();

                    result = data.PagingIQueryable(paging.page, paging.pageSize,
                            Constraints.LimitPaging, Constraints.DefaultPaging);
                }
            }
            catch (Exception)
            {
                return new DynamicModelResponse.DynamicModelsResponse<PurchasedSongDTO>()
                {
                    Message = Constraints.LOAD_FAILED,
                };
            }

            var finalResult = new DynamicModelResponse.DynamicModelsResponse<PurchasedSongDTO>()
            {
                Message = Constraints.INFORMATION,
                Metadata = new DynamicModelResponse.PagingMetadata()
                {
                    Page = paging.page,
                    Size = paging.pageSize,
                    Total = result.Item1
                },
                Results = _mapper.Map<List<PurchasedSongDTO>>(result.Item2.ToList())
            };

            if (finalResult.Results != null && finalResult.Results.Count > 0)
            {
                foreach (var song in finalResult.Results)
                {
                    song.IsFavorite = _favoriteSongRepository.GetDbSet().Any(s => s.SongId == song.SongId && s.MemberId == filter.MemberId);
                }
            }

            return finalResult;
        }
    }
}
