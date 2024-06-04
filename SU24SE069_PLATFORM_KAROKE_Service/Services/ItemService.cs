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
using SU24SE069_PLATFORM_KAROKE_Service.RequestModels.Item;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Service.Services
{
    public class ItemService : IItemService
    {
        private readonly IMapper _mapper;
        private readonly IItemRepository _itemRepository;

        public ItemService(IMapper mapper, IItemRepository itemRepository)
        {
            _mapper = mapper;
            _itemRepository = itemRepository;
        }
        public ResponseResult<ItemViewModel> CreateItem(CreateItemRequestModel request)
        {
            Item rs = new Item();
            try
            {
                lock (_itemRepository)
                {
                    if (_itemRepository.ExistedItem(request.ItemCode))
                    {
                        return new ResponseResult<ItemViewModel>()
                        {
                            Message = Constraints.INFORMATION_EXISTED,
                            result = false,
                        };
                    }
                    rs = _mapper.Map<Item>(request);

                    rs.ItemStatus = (int)ItemStatus.PENDING;
                    rs.CreatedDate = DateTime.Now;
                    rs.CanExpire = true;
                    rs.CanStack = true;

                    if (!_itemRepository.CreateItem(rs))
                    {
                        throw new Exception();
                    }
                }
            }catch(Exception ex)
            {
                return new ResponseResult<ItemViewModel>()
                {
                    Message = Constraints.CREATE_FAILED,
                    result = false,
                };
            }
            return new ResponseResult<ItemViewModel>()
            {
                Message = Constraints.CREATE_SUCCESS,
                result = true,
                Value = _mapper.Map<ItemViewModel>(rs)
            };
        }

        public ResponseResult<ItemViewModel> DeleteItem(Guid id)
        {
            try
            {
                lock (_itemRepository)
                {
                    var data = _itemRepository.GetByIdGuid(id).Result;
                    data.ItemStatus = (int)ItemStatus.DISABLE;

                    if (!_itemRepository.DeleteItem(data))
                    {
                        throw new Exception();
                    }
                }

            }catch(Exception ex)
            {
                return new ResponseResult<ItemViewModel>()
                {
                    Message = Constraints.DELETE_FAILED,
                    result = false
                };
            }

            return new ResponseResult<ItemViewModel>()
            {
                Message = Constraints.DELETE_SUCCESS,
                result = true,
            };
        }

        public ResponseResult<ItemViewModel> GetItem(Guid id)
        {
            Item rs = new Item();
            try
            {
                lock (_itemRepository)
                {
                    rs = _itemRepository.GetByIdGuid(id).Result;
                }
            }catch(Exception ex)
            {
                return new ResponseResult<ItemViewModel>()
                {
                    Message = Constraints.LOAD_FAILED,
                    result = false,
                };
            }

            return new ResponseResult<ItemViewModel>()
            {
                Message = Constraints.INFORMATION,
                result = true,
                Value = _mapper.Map<ItemViewModel>(rs)
            };
        }

        public DynamicModelResponse.DynamicModelsResponse<ItemViewModel> GetItems(ItemViewModel filter, PagingRequest request, ItemOrderFilter orderFilter)
        {
            (int, IQueryable<ItemViewModel>) result;
            try
            {
                lock (_itemRepository)
                {
                    var data = _itemRepository.GetAll(
                                                includeProperties: String.Join(",",
                                                SupportingFeature.GetNameIncludedProperties<Song>()))
                        .AsQueryable()

                        .ProjectTo<ItemViewModel>(_mapper.ConfigurationProvider)
                        .DynamicFilter(filter);

                    string? colName = Enum.GetName(typeof(ItemOrderFilter), orderFilter);
                    data = SupportingFeature.Sorting(data.AsEnumerable(), (SortOrder)request.OrderType, colName).AsQueryable();

                    result = data.PagingIQueryable(request.page, request.pageSize,
                            Constraints.LimitPaging, Constraints.DefaultPaging);
                }

            }
            catch (Exception ex)
            {
                return new DynamicModelResponse.DynamicModelsResponse<ItemViewModel>()
                {
                    Message = Constraints.LOAD_FAILED,
                };
            }

            return new DynamicModelResponse.DynamicModelsResponse<ItemViewModel>()
            {
                Message = Constraints.INFORMATION,
                Metadata = new DynamicModelResponse.PagingMetadata()
                {
                    Page = request.page,
                    Size = request.pageSize,
                    Total = result.Item1
                },
                Results = result.Item2.ToList()
            };
        }

        public ResponseResult<ItemViewModel> UpdateItem(Guid id, UpdateItemRequestModel request)
        {
            Item rs = new Item();
            try
            {
                lock (_itemRepository)
                {
                    var data = _itemRepository.GetByIdGuid(id).Result;

                    if(data is null)
                    {
                        return new ResponseResult<ItemViewModel>()
                        {
                            Message = Constraints.NOT_FOUND,
                            result = false,
                        };
                    }
                    rs = _mapper.Map<Item>(request);
                    rs.CreatedDate = data.CreatedDate;
                    rs.ItemId = data.ItemId;

                    if(!_itemRepository.UpdateItem(id, rs))
                    {
                        throw new Exception();
                    }

                    
                }
            }catch(Exception ex)
            {
                return new ResponseResult<ItemViewModel>()
                {
                    Message = Constraints.UPDATE_FAILED,
                    result = false,
                    
                };
            }

            return new ResponseResult<ItemViewModel>()
            {
                Message = Constraints.UPDATE_SUCCESS,
                result = true,
                Value = _mapper.Map<ItemViewModel>(rs)
            };
        }
    }
}
