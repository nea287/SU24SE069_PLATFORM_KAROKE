using SU24SE069_PLATFORM_KAROKE_BusinessLayer.Commons;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.ReponseModels.Helpers;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.RequestModels.Helpers;
using SU24SE069_PLATFORM_KAROKE_Service.Filters;
using SU24SE069_PLATFORM_KAROKE_Service.ReponseModels;
using SU24SE069_PLATFORM_KAROKE_Service.RequestModels.Conversation;
using SU24SE069_PLATFORM_KAROKE_Service.RequestModels.LiveChat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Service.IServices
{
    public interface IConversationService
    {
        public Task<DynamicModelResponse.DynamicModelsResponse<ConversationViewModel>> GetConversations(ConversationViewModel filter, PagingRequest paging, ConversationOrderFilter orderFilter);
        public DynamicModelResponse.DynamicModelsResponse<ConversationViewModel> GetConversationOfMember(Guid memberId, ConversationFilter filter, PagingRequest paging);
        public Task<ResponseResult<ConversationViewModel>> CreateConversation(ConversationRequestModel request);
        public Task<bool> SendPrivateMessage( ChatConversationRequestModel request);
        public Task<bool> SendPublicMessage(string user, string message);
    }
}
