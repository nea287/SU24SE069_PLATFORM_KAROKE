using AutoMapper;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.Commons;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.Helpers;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.ReponseModels.Helpers;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.ReponseModels;
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

namespace SU24SE069_PLATFORM_KAROKE_Service.Services
{
    public class SongService : ISongService
    {
        private readonly IMapper _mapper;
        private readonly ISongRepository _songRepository;

        public SongService(IMapper mapper, ISongRepository songRepository)
        {
            _mapper = mapper;
            _songRepository = songRepository;
        }
        #region Read
        public ResponseResult<SongViewModel> GetSong(Guid accountId)
        {
            ResponseResult<SongViewModel> result = new ResponseResult<SongViewModel>();
            try
            {
                lock (_songRepository)
                {
                    var data = _mapper.Map<SongViewModel>(_songRepository
                        .GetSong(id: accountId));

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
            catch (Exception ex)
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
            SongViewModel filter, PagingRequest paging, SongOrderFilter orderFilter)
        {
            (int, IQueryable<SongViewModel>) result;
            try
            {
                lock (_songRepository)
                {
                    var data = _songRepository.GetAll(
                                                includeProperties: String.Join(",",
                                                SupportingFeature.GetNameIncludedProperties<Song>()))
                        .AsQueryable()

                        .ProjectTo<SongViewModel>(_mapper.ConfigurationProvider)
                        .DynamicFilter(filter);

                    string? colName = Enum.GetName(typeof(SongOrderFilter), orderFilter);

                    data = SupportingFeature.Sorting(data.AsEnumerable(), (SortOrder)paging.OrderType, colName).AsQueryable();

                    result = data.PagingIQueryable(paging.page, paging.pageSize,
                            Constraints.LimitPaging, Constraints.DefaultPaging);
                }


            }
            catch (Exception ex)
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
        public ResponseResult<SongViewModel> CreateSong(CreateSongRequestModel request)
        {
            SongViewModel result = new SongViewModel();
            try
            {
                lock (_songRepository)
                {
                    if (_songRepository.ExistedSong(request.SongCode))
                    {
                        return new ResponseResult<SongViewModel>()
                        {
                            Message = Constraints.INFORMATION_EXISTED,
                            result = false,
                        };
                    }

                    var data = _mapper.Map<Song>(request);

                    data.SongStatus = (int)SongStatus.ENABLE;
                    data.CreatedDate = DateTime.Now;
                    data.UpdatedDate = DateTime.Now;

                    _= _songRepository.CreateSong(data);

                    result = _mapper.Map<SongViewModel>(data);
                };

            }
            catch (Exception ex)
            {
                return new ResponseResult<SongViewModel>()
                {
                    Message = Constraints.CREATE_FAILED,
                    result = false,
                };
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
        public ResponseResult<SongViewModel> UpdateSong(Guid id, UpdateSongRequestModel request)
        {
            SongViewModel result = new SongViewModel();
            try
            {
                lock (_songRepository)
                {
                    var data = _songRepository.GetSong(id);

                    data = _mapper.Map<Song>(request);

                    if (data == null)
                    {
                        return new ResponseResult<SongViewModel>()
                        {
                            Message = Constraints.NOT_FOUND,
                            result = false,
                            Value = _mapper.Map<SongViewModel>(data)
                        };
                    }

                    _songRepository.UpdateSong(id, data);

                    result = _mapper.Map<SongViewModel>(data);
                };


            }
            catch (Exception ex)
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

        public ResponseResult<SongViewModel> DeleteSong(Guid id)
        {
            try
            {
               _= _songRepository.DeleteSong(id);
            }catch(Exception ex)
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
    }

}
