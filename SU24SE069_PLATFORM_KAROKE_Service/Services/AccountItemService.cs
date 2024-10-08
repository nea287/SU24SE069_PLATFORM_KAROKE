﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using Castle.Core.Internal;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.Commons;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.Helpers;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.ReponseModels.Helpers;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.RequestModels.Helpers;
using SU24SE069_PLATFORM_KAROKE_DataAccess.Models;
using SU24SE069_PLATFORM_KAROKE_Repository.IRepository;
using SU24SE069_PLATFORM_KAROKE_Repository.Repository;
using SU24SE069_PLATFORM_KAROKE_Service.Filters;
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
    public class AccountItemService : IAccountItemService
    {
        private readonly IMemoryCache _cache;
        private readonly IMapper _mapper;
        private readonly IAccountItemRepository _inventoryRepository;

        public AccountItemService(IMapper mapper, IAccountItemRepository inventoryRepository, IMemoryCache cache)
        {
            _cache = cache;
            _mapper = mapper;
            _inventoryRepository = inventoryRepository;
        }
        public async Task<ResponseResult<AccountItemViewModel>> CreateAccountInventory(CreateAccountInventoryItemRequestModel request)
        {
            AccountItem rs = new AccountItem();
            try
            {
                lock (_inventoryRepository)
                {
                    rs = _mapper.Map<AccountItem>(request);    
                    rs.ActivateDate = DateTime.Now;

                    if (!_inventoryRepository.CreateAccountInventory(rs).Result)
                    {
                        _inventoryRepository.DetachEntity(rs);
                        throw new Exception();
                    }
                }
            }catch(Exception)
            {
                await _inventoryRepository.DisponseAsync();
                return new ResponseResult<AccountItemViewModel>()
                {
                    Message = Constraints.CREATE_FAILED,
                    result = false
                };
            }finally
            {
                await _inventoryRepository.DisponseAsync();
            }

            return new ResponseResult<AccountItemViewModel>()
            {
                Message = Constraints.CREATE_SUCCESS,
                result = true,
                Value = _mapper.Map<AccountItemViewModel>(rs)
            };
        }

        public DynamicModelResponse.DynamicModelsResponse<AccountItemViewModel> GetAccountInventories(AccountItemFilter filter, PagingRequest paging, AccountInventoryItemOrderFilter orderFilter)
        {
            (int, IQueryable<AccountItemViewModel>) result;
            try
            {
                IQueryable<AccountItemViewModel> data = JsonConvert.DeserializeObject<IQueryable<AccountItemViewModel>>(GetCache("ACCOUNTITEMS"));



                lock (_inventoryRepository)
                {

                    if (data.IsNullOrEmpty())
                    {
                        data = _inventoryRepository.GetAll(
                               includeProperties: String.Join(",",
                               SupportingFeature.GetNameIncludedProperties<AccountItem>()))
                               .AsQueryable()
                               .ProjectTo<AccountItemViewModel>(_mapper.ConfigurationProvider);
                    }

                    data = data.DynamicFilter(_mapper.Map<AccountItemViewModel>(filter));

                    string? colName = Enum.GetName(typeof(AccountInventoryItemOrderFilter), orderFilter);

                    data = SupportingFeature.Sorting(data.AsEnumerable(), (SortOrder)paging.OrderType, colName).AsQueryable();

                    result = data.PagingIQueryable(paging.page, paging.pageSize,
                            Constraints.LimitPaging, Constraints.DefaultPaging);
                }


            }
            catch (Exception)
            {
                //await _inventoryRepository.DisponseAsync();
                return new DynamicModelResponse.DynamicModelsResponse<AccountItemViewModel>()
                {
                    Message = Constraints.LOAD_FAILED,
                };
            }

            return new DynamicModelResponse.DynamicModelsResponse<AccountItemViewModel>()
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

        public async Task<ResponseResult<AccountItemViewModel>> DeleteAccountItem(Guid id)
        {
            try
            {
                if (!_inventoryRepository.CheckAccountInventory(id))
                {
                    await _inventoryRepository.DisponseAsync();
                    return new ResponseResult<AccountItemViewModel>() { 
                        Message = Constraints.NOT_FOUND,
                        result = false
                    };
                }

                if(!await _inventoryRepository.DeleteAccountInventory(id))
                {
                    throw new Exception();
                }
            }
            catch (Exception)
            {
                return new ResponseResult<AccountItemViewModel>() {
                    Message = Constraints.DELETE_FAILED, 
                    result = false
                };
            }
            finally
            {
                await _inventoryRepository.DisponseAsync();
            }

            return new ResponseResult<AccountItemViewModel>()
            {
                Message = Constraints.DELETE_SUCCESS,
                result = true
            };
        }

        public async  Task<ResponseResult<AccountItemViewModel>> UpdateAccountInventoryItem(Guid id, CreateAccountInventoryItemRequestModel request)
        {
            AccountItem rs = new AccountItem();
            try
            {
                lock (_inventoryRepository)
                {
                    var data = _inventoryRepository.GetByIdGuid(id).Result;
                    if(data is null)
                    {
                        return new ResponseResult<AccountItemViewModel>()
                        {
                            Message = Constraints.NOT_FOUND,
                            result = false,   
                        };
                    }

                    rs = _mapper.Map<AccountItem>(request);
                    rs.ActivateDate = data.ActivateDate;

                    rs.AccountItemId = id;

                    _inventoryRepository.DetachEntity(data);
                    _inventoryRepository.MotifyEntity(rs);

                    if (!_inventoryRepository.UpdateAccountInventory(id, rs).Result)
                    {
                        _inventoryRepository.DetachEntity(rs);
                        throw new Exception();
                    }
                }
            }catch(Exception)
            {
                return new ResponseResult<AccountItemViewModel>()
                {
                    Message = Constraints.UPDATE_FAILED,
                    result = false,
                };
            }
            finally
            {
                await _inventoryRepository.DisponseAsync();

            }

            return new ResponseResult<AccountItemViewModel>()
            {
                Message = Constraints.UPDATE_SUCCESS,
                result = true,
                Value = _mapper.Map<AccountItemViewModel>(rs)
            };
        }

        public bool SetCache(String nameKey, string value, int minutes)
        {
            try
            {
                _cache.Set(nameKey, value, new TimeSpan(0, minutes, 0));
            }
            catch (Exception)
            {

                return false; 
            }

            return true;

        }

        public string GetCache(String nameKey)
        {
            string value = "";
            try
            {
                value = String.Concat(_cache.Get(nameKey));
            }
            catch (Exception)
            {
                return null;
            }

            return value;
        }
    }
}
