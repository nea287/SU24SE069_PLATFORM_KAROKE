using AutoMapper;
using AutoMapper.QueryableExtensions;
using Org.BouncyCastle.Asn1.Ocsp;
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
using SU24SE069_PLATFORM_KAROKE_Service.RequestModels.Item;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.RegularExpressions;
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

        public async Task<ResponseResult<ItemViewModel>> CreateItem(CreateItemRequestModel request)
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

                    if (!_itemRepository.CreateItem(rs).Result)
                    {
                        _itemRepository.DetachEntity(rs);
                        throw new Exception();
                    }
                }
            }catch(Exception)
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

        public async Task<ResponseResult<ItemViewModel>> DeleteItem(Guid id)
        {
            try
            {
                lock (_itemRepository)
                {
                    var data = _itemRepository.GetByIdGuid(id).Result;
                    data.ItemStatus = (int)ItemStatus.DISABLE;

                    if (!_itemRepository.DeleteItem(data).Result)
                    {
                        _itemRepository.DetachEntity(data);
                        throw new Exception();
                    }
                }

            }catch(Exception)
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

        public async Task<ResponseResult<ItemViewModel>> GetItem(Guid id)
        {
            Item rs = new Item();
            try
            {
                lock (_itemRepository)
                {
                    rs = _itemRepository.GetByIdGuid(id).Result;
                }
            }catch(Exception)
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

        public DynamicModelResponse.DynamicModelsResponse<ItemViewModel> GetItemsForAdmin(string? filter, PagingRequest request, ItemOrderFilter orderFilter)
        {
            (int, IQueryable<ItemViewModel>) result;
            try
            {
                lock (_itemRepository)
                {
                    var data3 = _itemRepository.GetAll(
                                                includeProperties: String.Join(",",
                                                SupportingFeature.GetNameIncludedProperties<Item>())).ToList();


                    var data2 = _mapper.Map<List<ItemFilter>>(data3);

                   var data1 = data2.AsQueryable()
                        .DynamicFilterForAdmin(filter).ToList();

                    var data = _mapper.Map<List<ItemViewModel>>(data1).AsQueryable();

                    string? colName = Enum.GetName(typeof(ItemOrderFilter), orderFilter);
                    data = SupportingFeature.Sorting(data.AsEnumerable(), (SortOrder)request.OrderType, colName).AsQueryable();

                    result = data.PagingIQueryable(request.page, request.pageSize,
                            Constraints.LimitPaging, Constraints.DefaultPaging);
                }

            }
            catch (Exception)
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


        public DynamicModelResponse.DynamicModelsResponse<ItemViewModel> GetItems(ItemFilter filter, PagingRequest request, ItemOrderFilter orderFilter)
        {
            (int, IQueryable<ItemViewModel>) result;
            try
            {
                lock (_itemRepository)
                {
                    var data3 = _itemRepository.GetAll(
                                                includeProperties: String.Join(",",
                                                SupportingFeature.GetNameIncludedProperties<Item>()))
                        .ToList();

                    var data2 = _mapper.Map<ICollection<ItemFilter>>(data3);

                   var data1 =  data2.AsQueryable()
                        .DynamicFilter(_mapper.Map<ItemFilter>(filter)).ToList();

                    var data = _mapper.Map<List<ItemViewModel>>(data1).AsQueryable();

                    string? colName = Enum.GetName(typeof(ItemOrderFilter), orderFilter);
                    data = SupportingFeature.Sorting(data.AsEnumerable(), (SortOrder)request.OrderType, colName).AsQueryable();

                    result = data.PagingIQueryable(request.page, request.pageSize,
                            Constraints.LimitPaging, Constraints.DefaultPaging);
                }

            }
            catch (Exception)
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


        public async Task<ResponseResult<ItemViewModel>> UpdateItem(Guid id, UpdateItemRequestModel request)
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

                    //request.GetType().GetProperties().Select(x =>
                    //{
                    //    if (x.GetType().GetProperty(x.Name).GetValue(x, null) == null)
                    //    {
                    //        x.SetValue(x, data.GetType().GetProperty(x.Name).GetValue(x, null));
                    //    }
                    //    return x;
                    //});

                    request.GetType().GetProperties().Where(pro => pro.GetValue(request) == null)
                    .ToList().ForEach(e =>
                    {
                        if (e.PropertyType.IsEnum ||
                            (Nullable.GetUnderlyingType(e.PropertyType)?.IsEnum ?? false))
                        {

                            Type? enumType = Type.GetType(Regex.Match(e.PropertyType.FullName, @"(SU24SE069_PLATFORM_KAROKE_BusinessLayer\.Commons[^,]*)").Value);
                            var value = Enum.GetName(enumType, data.GetType().GetProperty(e.Name)?.GetValue(data));

                            var value1 = Enum.Parse(enumType, value);
                            e.SetValue(request, value1);


                        }
                        else
                        {
                            e.SetValue(request, data.GetType().GetProperty(e.Name)?.GetValue(data));

                        }
                    });

                    rs = _mapper.Map<Item>(request);
                    rs.CreatedDate = data.CreatedDate;
                    rs.ItemId = data.ItemId;

                    _itemRepository.DetachEntity(data);
                    _itemRepository.MotifyEntity(rs);

                    if(!_itemRepository.UpdateItem(id, rs).Result)
                    {
                        _itemRepository.DetachEntity(rs);
                        throw new Exception();
                    }

                    
                }
            }catch(Exception)
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

        public DynamicModelResponse.DynamicModelsResponse<ItemShopViewModel> GetShopItemOfAMember(Guid memberId, ItemFilter filter, PagingRequest paging, ItemOrderFilter orderFilter)
        {
            (int, IQueryable<ItemShopViewModel>) result;
            try
            {
                var data1 = _itemRepository.GetAll(
                                                includeProperties: String.Join(",",
                                                SupportingFeature.GetNameIncludedProperties<Item>()))
                        .ProjectTo<ItemFilter>(_mapper.ConfigurationProvider)
                        .DynamicFilter(_mapper.Map<ItemFilter>(filter)).ToList();


                var data = _mapper.Map<List<ItemShopViewModel>>(data1);


                data = data.Select(x =>
                {
                    if(x.CanStack == false)
                    {
                        x.IsOwned = data1.SelectMany(a => a.AccountItems).Any(t => t.MemberId == memberId && t.ItemId == x.ItemId) ? true : false;
                    }
                    else
                    {
                        if(data1.SelectMany(a => a.AccountItems).Any(t => t.MemberId == memberId && t.ItemId == x.ItemId))
                        {
                            x.IsOwned = true;
                        }
                        else
                        {
                            x.IsOwned = false;
                        }
                    }

                    return x;
                }).ToList();

                string? colName = Enum.GetName(typeof(ItemOrderFilter), orderFilter);
                data = SupportingFeature.Sorting(data.AsEnumerable(), (SortOrder)paging.OrderType, colName).ToList();

                result = data.AsQueryable().PagingIQueryable(paging.page, paging.pageSize,
                        Constraints.LimitPaging, Constraints.DefaultPaging);

            }
            catch (Exception)
            {
                return new DynamicModelResponse.DynamicModelsResponse<ItemShopViewModel>()
                {
                    Message = Constraints.LOAD_FAILED,

                };
            }

            return new DynamicModelResponse.DynamicModelsResponse<ItemShopViewModel>()
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
    }
}
