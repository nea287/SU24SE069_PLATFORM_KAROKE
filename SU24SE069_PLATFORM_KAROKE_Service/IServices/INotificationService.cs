using SU24SE069_PLATFORM_KAROKE_BusinessLayer.Commons;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.ReponseModels.Helpers;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.RequestModels.Helpers;
using SU24SE069_PLATFORM_KAROKE_Service.Filters;
using SU24SE069_PLATFORM_KAROKE_Service.ReponseModels;
using SU24SE069_PLATFORM_KAROKE_Service.ReponseModels.Notification;
using SU24SE069_PLATFORM_KAROKE_Service.RequestModels.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Service.IServices
{
    public interface INotificationService
    {
        public Task<ResponseResult<NotificationViewModel>> GetNotification(int id);
        public Task<DynamicModelResponse.DynamicModelsResponse<NotificationViewModel>> GetNotificationByAccountId(Guid accountId, NotificationFiilter filter, PagingRequest paging, NoticationFilter orderFilter);
        public Task<ResponseResult<NotificationViewModel>> UpdateStatus(int id, NotificationStatus status);
        public Task<ResponseResult<NotificationViewModel>> CreateNotification(CreateNotificationRequestModel createNotificationRequest);
        public Task<ResponseResult<NotificationViewModel>> DeleteNotification(int id);
        Task CreateAndSendNotification(CreateNotificationRequestModel notificationRequestModel);
        Task<ResponseResult<List<NotificationResponse>>> GetUserUnreadNotifications(Guid userId);
        Task<ResponseResult<bool>> UpdateUnreadNotificationsToRead(Guid userId);
        Task<ResponseResult<NotificationResponse>> UpdateNotificationStatus(int notificationId, NotificationStatusUpdateRequest updateRequest);
        Task<ResponseResult<List<NotificationResponse>>> GetUserReadAndUnreadNotifications(Guid userId);
        Task<ResponseResult<bool>> UpdateReadNotificationsToDelete(Guid userId);
    }
}
