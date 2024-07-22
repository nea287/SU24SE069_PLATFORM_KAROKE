using SU24SE069_PLATFORM_KAROKE_BusinessLayer.Commons;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.ReponseModels.Helpers;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.RequestModels.Account;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.RequestModels.Helpers;
using SU24SE069_PLATFORM_KAROKE_Service.Filters;
using SU24SE069_PLATFORM_KAROKE_Service.ReponseModels;
using SU24SE069_PLATFORM_KAROKE_Service.RequestModels.Item;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Service.IServices
{
    public interface IItemService
    {
        public Task<ResponseResult<ItemViewModel>> GetItem(Guid id);
        public DynamicModelResponse.DynamicModelsResponse<ItemViewModel> GetItems(ItemFilter filter, PagingRequest request, ItemOrderFilter orderFilter);
        public Task<ResponseResult<ItemViewModel>> CreateItem(CreateItemRequestModel request);
        public Task<ResponseResult<ItemViewModel>> DeleteItem(Guid id);
        public Task<ResponseResult<ItemViewModel>> UpdateItem(Guid id, UpdateItemRequestModel request);
    }
}
