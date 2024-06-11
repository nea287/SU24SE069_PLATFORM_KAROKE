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
using SU24SE069_PLATFORM_KAROKE_Service.RequestModels.AccountInventoryItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Service.Services
{
    public class AccountInventoryItemService : IAccountInventoryItemService
    {
        private readonly IMapper _mapper;
        private readonly IAccountInventoryItemRepository _inventoryRepository;

        public AccountInventoryItemService(IMapper mapper, IAccountInventoryItemRepository inventoryRepository)
        {
            _mapper = mapper;
            _inventoryRepository = inventoryRepository;
        }
        public async Task<ResponseResult<AccountInventoryItemViewModel>> CreateAccountInventory(CreateAccountInventoryItemRequestModel request)
        {
            AccountInventoryItem rs = new AccountInventoryItem();
            try
            {
                lock (_inventoryRepository)
                {
                    rs = _mapper.Map<AccountInventoryItem>(request);    
                    rs.ActivateDate = DateTime.Now;

                    if (!_inventoryRepository.CreateAccountInventory(rs).Result)
                    {
                        _inventoryRepository.DetachEntity(rs);
                        throw new Exception();
                    }
                }
            }catch(Exception ex)
            {
                return new ResponseResult<AccountInventoryItemViewModel>()
                {
                    Message = Constraints.CREATE_FAILED,
                    result = false
                };
            }

            return new ResponseResult<AccountInventoryItemViewModel>()
            {
                Message = Constraints.CREATE_SUCCESS,
                result = true,
                Value = _mapper.Map<AccountInventoryItemViewModel>(rs)
            };
        }

        public DynamicModelResponse.DynamicModelsResponse<AccountInventoryItemViewModel> GetAccountInventories(AccountInventoryItemViewModel filter, PagingRequest paging, AccountInventoryItemOrderFilter orderFilter)
        {
            (int, IQueryable<AccountInventoryItemViewModel>) result;
            try
            {
                lock (_inventoryRepository)
                {
                    var data = _inventoryRepository.GetAll(
                                                includeProperties: String.Join(",",
                                                SupportingFeature.GetNameIncludedProperties<AccountInventoryItem>()))
                        .AsQueryable()
                        .ProjectTo<AccountInventoryItemViewModel>(_mapper.ConfigurationProvider)
                        .DynamicFilter(filter);

                    string? colName = Enum.GetName(typeof(AccountInventoryItemOrderFilter), orderFilter);

                    data = SupportingFeature.Sorting(data.AsEnumerable(), (SortOrder)paging.OrderType, colName).AsQueryable();

                    result = data.PagingIQueryable(paging.page, paging.pageSize,
                            Constraints.LimitPaging, Constraints.DefaultPaging);
                }


            }
            catch (Exception ex)
            {
                return new DynamicModelResponse.DynamicModelsResponse<AccountInventoryItemViewModel>()
                {
                    Message = Constraints.LOAD_FAILED,
                };
            }

            return new DynamicModelResponse.DynamicModelsResponse<AccountInventoryItemViewModel>()
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

        public async  Task<ResponseResult<AccountInventoryItemViewModel>> UpdateAccountInventoryItem(Guid id, CreateAccountInventoryItemRequestModel request)
        {
            AccountInventoryItem rs = new AccountInventoryItem();
            try
            {
                lock (_inventoryRepository)
                {
                    var data = _inventoryRepository.GetByIdGuid(id).Result;
                    if(data is null)
                    {
                        return new ResponseResult<AccountInventoryItemViewModel>()
                        {
                            Message = Constraints.NOT_FOUND,
                            result = false,   
                        };
                    }

                    rs = _mapper.Map<AccountInventoryItem>(request);
                    rs.ActivateDate = data.ActivateDate;

                    rs.AccountInventoryItemId = id;

                    _inventoryRepository.DetachEntity(data);
                    _inventoryRepository.MotifyEntity(rs);

                    if (!_inventoryRepository.UpdateAccountInventory(id, rs).Result)
                    {
                        _inventoryRepository.DetachEntity(rs);
                        throw new Exception();
                    }
                }
            }catch(Exception ex)
            {
                return new ResponseResult<AccountInventoryItemViewModel>()
                {
                    Message = Constraints.UPDATE_FAILED,
                    result = false,
                };
            }

            return new ResponseResult<AccountInventoryItemViewModel>()
            {
                Message = Constraints.UPDATE_SUCCESS,
                result = true,
                Value = _mapper.Map<AccountInventoryItemViewModel>(rs)
            };
        }
    }
}
