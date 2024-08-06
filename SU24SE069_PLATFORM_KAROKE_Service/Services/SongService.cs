using AutoMapper;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.Commons;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.Helpers;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.ReponseModels.Helpers;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.RequestModels.Helpers;
using SU24SE069_PLATFORM_KAROKE_DataAccess.Models;
using SU24SE069_PLATFORM_KAROKE_Repository.IRepository;
using SU24SE069_PLATFORM_KAROKE_Service.IServices;
using SU24SE069_PLATFORM_KAROKE_Service.ReponseModels;
using AutoMapper.QueryableExtensions;
using SU24SE069_PLATFORM_KAROKE_Service.RequestModels.Song;
using SU24SE069_PLATFORM_KAROKE_Service.Filters;
using System.Linq.Expressions;
using SU24SE069_PLATFORM_KAROKE_Service.Helpers;
using SU24SE069_PLATFORM_KAROKE_Service.Filters.Song;

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
        public async Task<ResponseResult<SongDTO>> GetSong(Guid songId)
        {
            var song = await _songRepository.GetSongById(songId);

            if (song == null) 
            {
                return new ResponseResult<SongDTO>()
                {
                    Message = Constraints.NOT_FOUND,
                    result = false,
                    Value = null,
                };
            }
            return new ResponseResult<SongDTO>()
            {
                Message = Constraints.LOAD_SUCCESS,
                result = true,
                Value = _mapper.Map<SongDTO>(song),
            };
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
                                                includeProperties: "SongArtists,SongGenres,SongSingers")
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
                if (!await _songRepository.DeleteSong(id))
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

        public async Task<DynamicModelResponse.DynamicModelsResponse<SongDTO>> GetSongsPurchaseFavorite(Guid accountId, KaraokeSongFilter filter, PagingRequest paging, SongOrderFilter orderFilter = SongOrderFilter.SongName)
        {
            (int totalCount, var queryResult) = await _songRepository.GetSongsPurchaseFavoriteFiltered(paging.page, paging.pageSize, GenerateSongFilterExpression(filter), GenerateSongSortOrderExpression(orderFilter, paging.OrderType));

            var finalResult = new DynamicModelResponse.DynamicModelsResponse<SongDTO>()
            {
                Message = Constraints.INFORMATION,
                Metadata = new DynamicModelResponse.PagingMetadata()
                {
                    Page = paging.page,
                    Size = paging.pageSize,
                    Total = totalCount
                },
                Results = _mapper.Map<List<SongDTO>>(queryResult)
            };
            // If result has song, check if the account (accountId) has purchase or favorite the songs
            if (finalResult.Results != null && finalResult.Results.Count > 0 && accountId != Guid.Empty)
            {
                foreach (var song in finalResult.Results)
                {
                    song.isPurchased = await _purchasedSongRepository.HasUserPurchaseSong(accountId, song.SongId);
                    song.isFavorite = await _favouriteSongRepository.HasUserFavoriteSong(accountId, song.SongId);
                }
            }
            return finalResult;
        }

        private Expression<Func<Song, bool>> GenerateSongFilterExpression(KaraokeSongFilter filter)
        {
            Expression<Func<Song, bool>>? songNameExp = filter.SongName == null ? null : s => s.SongName.Contains(filter.SongName);
            Expression<Func<Song, bool>>? songCodeExp = filter.SongCode == null ? null : s => s.SongCode.Contains(filter.SongCode);
            Expression<Func<Song, bool>>? artistNameExp = filter.ArtistName == null ? null : s => s.SongArtists.Any(sa => sa.Artist.ArtistName.Contains(filter.ArtistName));
            Expression<Func<Song, bool>>? singerNameExp = filter.SingerName == null ? null : s => s.SongSingers.Any(sg => sg.Singer.SingerName.Contains(filter.SingerName));
            Expression<Func<Song, bool>>? genreNameExp = filter.GenreName == null ? null : s => s.SongGenres.Any(sge => sge.Genre.GenreName.Contains(filter.GenreName));
            Expression<Func<Song, bool>>? songStatusExp = filter.SongStatus == null ? null : s => s.SongStatus == (int)filter.SongStatus;

            var keywordExp = ExpressionCombiner.CombineExpressionsWithOr(songNameExp,
                songCodeExp,
                artistNameExp,
                singerNameExp,
                genreNameExp);

            return ExpressionCombiner.CombineExpressionsWithAnd(keywordExp, songStatusExp);
        }

        private Func<IQueryable<Song>, IOrderedQueryable<Song>> GenerateSongSortOrderExpression(SongOrderFilter orderFilter, SortOrder sortOrder)
        {
            Type type = typeof(Song);
            string propertyName = orderFilter.ToString();
            var property = type.GetProperty(propertyName);

            if (property == null)
            {
                throw new ArgumentException($"Property '{propertyName}' not found on type '{type.FullName}'");
            }

            // Create parameter expression for the lambda
            var parameter = Expression.Parameter(type, "s");

            // Create the property access expression
            var propertyAccess = Expression.MakeMemberAccess(parameter, property);

            // Convert the property access to object
            var convertedPropertyAccess = Expression.Convert(propertyAccess, typeof(object));

            // Create the lambda expression
            var orderByExpression = Expression.Lambda<Func<Song, object>>(convertedPropertyAccess, parameter);

            // Return the appropriate ordering function
            if (sortOrder == SortOrder.Ascending)
            {
                return q => q.OrderBy(orderByExpression);
            }
            else
            {
                return q => q.OrderByDescending(orderByExpression);
            }
        }

        #endregion
    }

}
