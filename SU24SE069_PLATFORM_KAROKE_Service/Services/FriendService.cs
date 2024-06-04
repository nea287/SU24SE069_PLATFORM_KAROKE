using AutoMapper;
using AutoMapper.QueryableExtensions;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.Commons;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.Helpers;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.ReponseModels.Helpers;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.RequestModels.Account;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.RequestModels.Helpers;
using SU24SE069_PLATFORM_KAROKE_DataAccess.Models;
using SU24SE069_PLATFORM_KAROKE_Repository.IRepository;
using SU24SE069_PLATFORM_KAROKE_Repository.Repository;
using SU24SE069_PLATFORM_KAROKE_Service.IServices;
using SU24SE069_PLATFORM_KAROKE_Service.ReponseModels;
using SU24SE069_PLATFORM_KAROKE_Service.ReponseModels.Friend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Service.Services
{
    public class FriendService : IFriendService
    {
        private readonly IMapper _mapper;
        private readonly IFriendRepository _friendRepository;

        public FriendService(IMapper mapper, IFriendRepository friendRepository)
        {
            _mapper = mapper;
            _friendRepository = friendRepository;
        }
        #region Create
        public ResponseResult<FriendViewModel> CreateFriend(FriendRequestModel request)
        {
            Friend rs = new Friend();
            try
            {
                lock (_friendRepository)
                {
                    rs = _mapper.Map<Friend>(request);
                    rs.Status = 1;

                    _friendRepository.CreateFriend(rs);
                }
            }catch(Exception ex)
            {
                return new ResponseResult<FriendViewModel>()
                {
                    Message = Constraints.CREATE_FAILED,
                    result = false,
                    
                };
            }

            return new ResponseResult<FriendViewModel>()
            {
                Message = Constraints.CREATE_SUCCESS,
                result = true,
                Value = _mapper.Map<FriendViewModel>(rs)
            };
        }
        #endregion

        #region Delete
        public ResponseResult<FriendViewModel> DeleteFriend(Guid id)
        {
            try
            {
                lock (_friendRepository)
                {
                    _friendRepository.DeleteFriend(id);
                }
            }catch(Exception ex)
            {
                return new ResponseResult<FriendViewModel>()
                {
                    Message = Constraints.DELETE_FAILED,
                    result = false,
                };
            }
            return new ResponseResult<FriendViewModel>()
            {
                Message = Constraints.DELETE_SUCCESS,
                result = true,
            };
        }
        #endregion

        #region Read
        public DynamicModelResponse.DynamicModelsResponse<FriendViewModel> GetFriends(FriendViewModel filter, PagingRequest paging, FriendOrderFilter orderFilter)
        {
            (int, IQueryable<FriendViewModel>) result;
            try
            {
                lock (_friendRepository)
                {
                    var data = _friendRepository.GetAll(
                                                includeProperties: String.Join(",",
                                                SupportingFeature.GetNameIncludedProperties<Friend>()))
                        .AsQueryable()

                        .ProjectTo<FriendViewModel>(_mapper.ConfigurationProvider)
                        .DynamicFilter(filter);

                    string? colName = Enum.GetName(typeof(FriendOrderFilter), orderFilter);

                    data = SupportingFeature.Sorting(data.AsEnumerable(), (SortOrder)paging.OrderType, colName).AsQueryable();

                    result = data.PagingIQueryable(paging.page, paging.pageSize,
                            Constraints.LimitPaging, Constraints.DefaultPaging);
                }


            }
            catch (Exception ex)
            {
                return new DynamicModelResponse.DynamicModelsResponse<FriendViewModel>()
                {
                    Message = Constraints.LOAD_FAILED,
                };
            }

            return new DynamicModelResponse.DynamicModelsResponse<FriendViewModel>()
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
    }
}
