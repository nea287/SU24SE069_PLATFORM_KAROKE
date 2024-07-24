using AutoMapper;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.Commons;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.Helpers;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.ReponseModels.Helpers;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.RequestModels.Helpers;
using SU24SE069_PLATFORM_KAROKE_DataAccess.Models;
using SU24SE069_PLATFORM_KAROKE_Repository.IRepository;
using SU24SE069_PLATFORM_KAROKE_Repository.Repository;
using SU24SE069_PLATFORM_KAROKE_Service.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SU24SE069_PLATFORM_KAROKE_Service.ReponseModels;
using AutoMapper.QueryableExtensions;
using SU24SE069_PLATFORM_KAROKE_Service.RequestModels.Song;
using SU24SE069_PLATFORM_KAROKE_Service.Filters;

namespace SU24SE069_PLATFORM_KAROKE_Service.Services
{
    public class SongService : ISongService
    {
        private readonly IMapper _mapper;
        private readonly ISongRepository _songRepository;
        private readonly IPurchasedSongRepository _purchasedSongRepository;
        private readonly IFavouriteSongRepository _favouriteSongRepository;

        public SongService(IMapper mapper, ISongRepository songRepository, IPurchasedSongRepository purchasedSongRepository, IFavouriteSongRepository favouriteSongRepository)
        {
            _mapper = mapper;
            _songRepository = songRepository;
            _purchasedSongRepository = purchasedSongRepository;
            _favouriteSongRepository = favouriteSongRepository;
        }
        #region Read
        public async Task<ResponseResult<SongViewModel>> GetSong(Guid accountId)
        {
            ResponseResult<SongViewModel> result = new ResponseResult<SongViewModel>();
            try
            {
                lock (_songRepository)
                {
                    var data = _mapper.Map<SongViewModel>(_songRepository
                        .GetSong(id: accountId).Result);

                    result = data == null ?
                        new ResponseResult<SongViewModel>()
                        {
                            Message = Constraints.NOT_FOUND,
                            result = false
                        }
                        :
                        new ResponseResult<SongViewModel>()
                        {
                            Message = Constraints.INFORMATION,
                            Value = data,
                            result = true
                        };

                }
            }
            catch (Exception)
            {
                result = new ResponseResult<SongViewModel>()
                {
                    Message = Constraints.LOAD_FAILED,
                    result = false
                };
            }

            return result;
        }

        public DynamicModelResponse.DynamicModelsResponse<SongViewModel> GetSongs(
            SongFilter filter, PagingRequest paging, SongOrderFilter orderFilter)
        {
            (int, IQueryable<SongViewModel>) result;
            try
            {
                lock (_songRepository)
                {
                    var data1 = _songRepository.GetAll(
                                                includeProperties: String.Join(",",
                                                SupportingFeature.GetNameIncludedProperties<Song>()))
                        .ProjectTo<SongFilter>(_mapper.ConfigurationProvider)
                        .DynamicFilter(filter).ToList();

                    var data = _mapper.Map<ICollection<SongViewModel>>(data1).AsQueryable();

                    string? colName = Enum.GetName(typeof(SongOrderFilter), orderFilter);

                    data = SupportingFeature.Sorting(data.AsEnumerable(), (SortOrder)paging.OrderType, colName).AsQueryable();

                    result = data.PagingIQueryable(paging.page, paging.pageSize,
                            Constraints.LimitPaging, Constraints.DefaultPaging);
                }


            }
            catch (Exception)
            {
                return new DynamicModelResponse.DynamicModelsResponse<SongViewModel>()
                {
                    Message = Constraints.LOAD_FAILED,
                };
            }

            return new DynamicModelResponse.DynamicModelsResponse<SongViewModel>()
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
        #endregion

        #region Create
        public async Task<ResponseResult<SongViewModel>> CreateSong(CreateSongRequestModel request)
        {
            SongViewModel result = new SongViewModel();
            try
            {
                lock (_songRepository)
                {
                    string songCode = "SONG" + string.Concat(_songRepository.Count() + 1);
                    if (_songRepository.ExistedSong(songCode))
                    {
                        return new ResponseResult<SongViewModel>()
                        {
                            Message = Constraints.INFORMATION_EXISTED,
                            result = false,
                        };
                    }

                    var data = _mapper.Map<Song>(request);

                    data.SongCode = songCode;
                    data.SongStatus = (int)SongStatus.ENABLE;
                    data.CreatedDate = DateTime.Now;
                    data.UpdatedDate = DateTime.Now;



                    if (!_songRepository.CreateSong(data).Result)
                    {
                        _songRepository.DetachEntity(data);
                        throw new Exception();
                    }
                    result = _mapper.Map<SongViewModel>(data);

                };

            }
            catch (Exception)
            {
                return new ResponseResult<SongViewModel>()
                {
                    Message = Constraints.CREATE_FAILED,
                    result = false,
                };
            }
            finally
            {
                await _songRepository.DisponseAsync();
            }

            return new ResponseResult<SongViewModel>()
            {
                Message = Constraints.CREATE_SUCCESS,
                result = true,
                Value = result
            };
        }
        #endregion

        #region Update
        public async Task<ResponseResult<SongViewModel>> UpdateSong(Guid id, UpdateSongRequestModel request)
        {
            SongViewModel result = new SongViewModel();
            try
            {
                lock (_songRepository)
                {
                    var data1 = _songRepository.GetSong(id).Result;

                    var data = _mapper.Map<Song>(request);

                    data.CreatedDate = data1.CreatedDate;
                    data.UpdatedDate = DateTime.Now;
                    data.SongStatus = data1.SongStatus;
                    data.SongId = id;

                    if (data == null)
                    {
                        return new ResponseResult<SongViewModel>()
                        {
                            Message = Constraints.NOT_FOUND,
                            result = false,
                            Value = _mapper.Map<SongViewModel>(data)
                        };
                    }

                    _songRepository.DetachEntity(data1);
                    _songRepository.MotifyEntity(data);

                    if (!_songRepository.UpdateSong(id, data).Result)
                    {
                        _songRepository.DetachEntity(data);
                        throw new Exception();
                    }

                    result = _mapper.Map<SongViewModel>(data);
                };


            }
            catch (Exception)
            {
                return new ResponseResult<SongViewModel>()
                {
                    Message = Constraints.UPDATE_FAILED,
                    result = false,
                    Value = result
                };
            }

            return new ResponseResult<SongViewModel>()
            {
                Message = Constraints.UPDATE_SUCCESS,
                result = true,
                Value = result
            };
        }

        public async Task<ResponseResult<SongViewModel>> DeleteSong(Guid id)
        {
            try
            {
                if (!_songRepository.DeleteSong(id).Result)
                {
                    throw new Exception();
                }
            }
            catch (Exception)
            {
                return new ResponseResult<SongViewModel>()
                {
                    Message = Constraints.DELETE_FAILED,
                    result = false,
                };
            }

            return new ResponseResult<SongViewModel>()
            {
                Message = Constraints.DELETE_SUCCESS,
                result = true,
            };
        }
        #endregion

        #region Query

        public async Task<DynamicModelResponse.DynamicModelsResponse<SongDTO>> GetSongsPurchaseFavorite(Guid accountId, SongFilter filter, PagingRequest paging, SongOrderFilter orderFilter = SongOrderFilter.SongName)
        {
            (int, IQueryable<SongViewModel>) result;
            try
            {
                lock (_songRepository)
                {
                    var data1 = _songRepository.GetAll(
                                                includeProperties: String.Join(",",
                                                SupportingFeature.GetNameIncludedProperties<Song>()))
                        .ProjectTo<SongFilter>(_mapper.ConfigurationProvider)
                        .DynamicFilter(filter)
                        .ToList();

                    var data = _mapper.Map<ICollection<SongViewModel>>(data1).AsQueryable();

                    string? colName = Enum.GetName(typeof(SongOrderFilter), orderFilter);

                    data = SupportingFeature.Sorting(data.AsEnumerable(), (SortOrder)paging.OrderType, colName).AsQueryable();

                    result = data.PagingIQueryable(paging.page, paging.pageSize,
                            Constraints.LimitPaging, Constraints.DefaultPaging);
                }
            }
            catch (Exception ex)
            {
                return new DynamicModelResponse.DynamicModelsResponse<SongDTO>()
                {
                    Message = Constraints.LOAD_FAILED,
                };
            }

            var finalResult = new DynamicModelResponse.DynamicModelsResponse<SongDTO>()
            {
                Message = Constraints.INFORMATION,
                Metadata = new DynamicModelResponse.PagingMetadata()
                {
                    Page = paging.page,
                    Size = paging.pageSize,
                    Total = result.Item1
                },
                Results = _mapper.Map<List<SongDTO>>(result.Item2.ToList())
            };
            // If result has song, check if the account (accountId) has purchase or favorite the songs
            if(finalResult.Results != null && finalResult.Results.Count > 0)
            {
                foreach (var song in finalResult.Results)
                {
                    song.isPurchased = _purchasedSongRepository.GetDbSet().Any(s => s.SongId == song.SongId && s.MemberId == accountId);
                    song.isFavorite = _favouriteSongRepository.GetDbSet().Any(s => s.SongId == song.SongId && s.MemberId == accountId);
                }
            }
            return finalResult;
        }

        #endregion
    }

}
