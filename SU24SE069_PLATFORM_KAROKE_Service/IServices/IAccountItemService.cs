using SU24SE069_PLATFORM_KAROKE_BusinessLayer.Commons;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.ReponseModels.Helpers;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.RequestModels.Helpers;
using SU24SE069_PLATFORM_KAROKE_Service.Filters;
using SU24SE069_PLATFORM_KAROKE_Service.ReponseModels;
using SU24SE069_PLATFORM_KAROKE_Service.RequestModels.AccountInventoryItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Service.IServices
{
    public interface IAccountItemService
    {
        public Task<ResponseResult<AccountItemViewModel>> CreateAccountInventory(CreateAccountInventoryItemRequestModel request);
        public Task<ResponseResult<AccountItemViewModel>> UpdateAccountInventoryItem(Guid id, CreateAccountInventoryItemRequestModel request);
        public DynamicModelResponse.DynamicModelsResponse<AccountItemViewModel> GetAccountInventories(AccountItemFilter filter, PagingRequest paging, AccountInventoryItemOrderFilter orderFilter);
        
    }
}
