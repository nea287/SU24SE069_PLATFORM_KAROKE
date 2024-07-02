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
using SU24SE069_PLATFORM_KAROKE_Service.RequestModels.KaraokeRoom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Service.Services
{
    public class KaraokeRoomService : IKaraokeRoomService
    {
        private readonly IMapper _mapper;
        private readonly IKaraokeRoomRepository _repository;

        public KaraokeRoomService(IMapper mapper, IKaraokeRoomRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }
        public async Task<ResponseResult<KaraokeRoomViewModel>> CreateRoom(KaraokeRoomRequestModel request)
        {
            KaraokeRoom rs = new KaraokeRoom();
            try
            {

                if (_repository.ExistedRoom(roomLog: request.RoomLog))
                {
                    return new ResponseResult<KaraokeRoomViewModel>()
                    {
                        Message = Constraints.INFORMATION_EXISTED,
                        result = false,
                    };
                }
                rs = _mapper.Map<KaraokeRoom>(request);

                rs.CreateTime = DateTime.Now;

                if (!await _repository.CreateRoom(rs))
                {
                    _repository.DetachEntity(rs);
                    throw new Exception();
                }
            }
            catch (Exception)
            {
                return new ResponseResult<KaraokeRoomViewModel>()
                {
                    Message = Constraints.CREATE_FAILED,
                    result = false
                };
            }
            finally { lock (_repository) { } }

            return new ResponseResult<KaraokeRoomViewModel>()
            {
                Message = Constraints.CREATE_SUCCESS,
                result = true,
                Value = _mapper.Map<KaraokeRoomViewModel>(rs)
            };
        }

        public async Task<DynamicModelResponse.DynamicModelsResponse<KaraokeRoomViewModel>> GetRooms(KaraokeRoomViewModel filter, PagingRequest paging, KaraokeRoomOrderFilter orderFilter)
        {
            (int, IQueryable<KaraokeRoomViewModel>) result;
            try
            {
                lock (_repository)
                {
                    var data = _repository.GetAll(
                                                includeProperties: String.Join(",",
                                                SupportingFeature.GetNameIncludedProperties<KaraokeRoom>()))
                        .AsQueryable()
                        .ProjectTo<KaraokeRoomViewModel>(_mapper.ConfigurationProvider)
                        .DynamicFilter(filter);

                    string? colName = Enum.GetName(typeof(KaraokeRoomOrderFilter), orderFilter);

                    data = SupportingFeature.Sorting(data.AsEnumerable(), (SortOrder)paging.OrderType, colName).AsQueryable();

                    result = data.PagingIQueryable(paging.page, paging.pageSize,
                            Constraints.LimitPaging, Constraints.DefaultPaging);
                }


            }
            catch (Exception)
            {
                return new DynamicModelResponse.DynamicModelsResponse<KaraokeRoomViewModel>()
                {
                    Message = Constraints.LOAD_FAILED,
                };
            }

            return new DynamicModelResponse.DynamicModelsResponse<KaraokeRoomViewModel>()
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

        public async Task<ResponseResult<KaraokeRoomViewModel>> UpdateRoom(KaraokeRoomRequestModel request, Guid id)
        {
            KaraokeRoom rs = new KaraokeRoom();
            try
            {
                var data = await _repository.GetRoom(id: id);

                if (data is null)
                {
                    return new ResponseResult<KaraokeRoomViewModel>()
                    {
                        Message = Constraints.NOT_FOUND,
                        result = false,
                    };
                }
                rs = _mapper.Map<KaraokeRoom>(request);
                rs.CreateTime = data.CreateTime;
                rs.RoomId = id;

                _repository.DetachEntity(data);
                _repository.MotifyEntity(rs);

                if (!await _repository.UpdateRoom(rs))
                {
                    _repository.DetachEntity(rs);
                    throw new Exception();
                }
            }
            catch (Exception)
            {
                return new ResponseResult<KaraokeRoomViewModel>()
                {
                    Message = Constraints.UPDATE_FAILED,
                    result = false,

                };
            }

            return new ResponseResult<KaraokeRoomViewModel>()
            {
                Message = Constraints.UPDATE_SUCCESS,
                result = true,
                Value = _mapper.Map<KaraokeRoomViewModel>(rs)
            };
        }
    }
}
