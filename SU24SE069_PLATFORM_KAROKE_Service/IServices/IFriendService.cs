using SU24SE069_PLATFORM_KAROKE_BusinessLayer.Commons;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.ReponseModels.Helpers;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.RequestModels.Account;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.RequestModels.Helpers;
using SU24SE069_PLATFORM_KAROKE_Service.ReponseModels;
using SU24SE069_PLATFORM_KAROKE_Service.ReponseModels.Friend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Service.IServices
{
    public interface IFriendService
    {
        public Task<ResponseResult<FriendViewModel>> DeleteFriend(Guid id);
        public DynamicModelResponse.DynamicModelsResponse<FriendViewModel> GetFriends(FriendViewModel filter, PagingRequest paging, FriendOrderFilter orderFilter);
        public Task<ResponseResult<FriendViewModel>> CreateFriend(FriendRequestModel request);
    }
}
