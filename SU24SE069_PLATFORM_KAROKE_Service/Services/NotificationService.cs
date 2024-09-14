using AutoMapper;
using AutoMapper.QueryableExtensions;
using Castle.Core.Internal;
using Microsoft.AspNetCore.SignalR;
using Org.BouncyCastle.Asn1.Ocsp;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.Commons;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.Helpers;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.ReponseModels.Helpers;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.RequestModels.Helpers;
using SU24SE069_PLATFORM_KAROKE_DataAccess.Models;
using SU24SE069_PLATFORM_KAROKE_Repository.IRepository;
using SU24SE069_PLATFORM_KAROKE_Service.Filters;
using SU24SE069_PLATFORM_KAROKE_Service.IServices;
using SU24SE069_PLATFORM_KAROKE_Service.ReponseModels;
using SU24SE069_PLATFORM_KAROKE_Service.ReponseModels.Notification;
using SU24SE069_PLATFORM_KAROKE_Service.RequestModels.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Service.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IMapper _mapper;
        private readonly INotificationRepository _repository;
        private readonly IHubContext<NotificationHub> _hubContext;

        private const string NotificationSendingMethodName = "PushNotification";

        public NotificationService(IMapper mapper, INotificationRepository repository, IHubContext<NotificationHub> hubContext)
        {
            _mapper = mapper;
            _repository = repository;
            _hubContext = hubContext;
        }

        public async Task<ResponseResult<NotificationViewModel>> CreateNotification(CreateNotificationRequestModel request)
        {
            Notification rs = new Notification();
            try
            {
                rs = _mapper.Map<Notification>(request);

                rs.Status = (int)NotificationStatus.UNREAD;
                rs.CreateDate = DateTime.Now;

                if (!await _repository.CreateNotification(rs))
                {
                    _repository.DetachEntity(rs);
                    throw new Exception();
                }
            }
            catch (Exception)
            {
                return new ResponseResult<NotificationViewModel>()
                {
                    Message = Constraints.CREATE_FAILED,
                    result = false
                };
            }
            finally
            {
            }

            return new ResponseResult<NotificationViewModel>()
            {
                Message = Constraints.CREATE_SUCCESS,
                result = true,
                Value = _mapper.Map<NotificationViewModel>(rs)
            };
        }

        public async Task<ResponseResult<NotificationViewModel>> DeleteNotification(int id)
        {
            Notification data = new Notification();
            try
            {
                data = await _repository.GetById(id);
                if (data is null)
                {
                    return new ResponseResult<NotificationViewModel>()
                    {
                        Message = Constraints.NOT_FOUND,
                        result = false,
                    };
                }

                data.Status = (int)NotificationStatus.DELETE;

                _repository.MotifyEntity(data);

                if (!await _repository.UpdateNotification(data))
                {
                    _repository.DetachEntity(data);
                    throw new Exception();
                }
            }
            catch (Exception)
            {
                return new ResponseResult<NotificationViewModel>()
                {
                    Message = Constraints.DELETE_FAILED,
                    result = false,
                };
            }
            finally { await _repository.DisponseAsync(); }

            return new ResponseResult<NotificationViewModel>()
            {
                Message = Constraints.DELETE_SUCCESS,


                result = true,
                Value = _mapper.Map<NotificationViewModel>(data)
            };
        }

        public async Task<ResponseResult<NotificationViewModel>> GetNotification(int id)
        {
            Notification rs = new Notification();
            try
            {
                rs = await _repository.GetById(id);

                if (rs is null)
                {
                    return new ResponseResult<NotificationViewModel>()
                    {
                        Message = Constraints.NOT_FOUND,
                        result = false,
                    };
                }


            }
            catch (Exception)
            {
                return new ResponseResult<NotificationViewModel>()
                {
                    Message = Constraints.NOT_FOUND,
                    result = false,

                };
            }

            return new ResponseResult<NotificationViewModel>()
            {
                Message = Constraints.INFORMATION,
                result = true,
                Value = _mapper.Map<NotificationViewModel>(rs)
            };
        }

        public async Task<DynamicModelResponse.DynamicModelsResponse<NotificationViewModel>> GetNotificationByAccountId(Guid accountId, NotificationFiilter filter, PagingRequest paging, NoticationFilter orderFilter)
        {
            (int, IQueryable<NotificationViewModel>) result;
            try
            {
                lock (_repository)
                {
                    var data = _repository.GetAllNotificationsByAccountId(accountId)
                        .ProjectTo<NotificationViewModel>(_mapper.ConfigurationProvider)
                        .DynamicFilter(_mapper.Map<NotificationViewModel>(filter));

                    string? colName = Enum.GetName(typeof(NoticationFilter), orderFilter);

                    data = SupportingFeature.Sorting(data.AsEnumerable(), (SortOrder)paging.OrderType, colName).AsQueryable();

                    result = data.PagingIQueryable(paging.page, paging.pageSize,
                            Constraints.LimitPaging, Constraints.DefaultPaging);
                }


            }
            catch (Exception)
            {
                return new DynamicModelResponse.DynamicModelsResponse<NotificationViewModel>()
                {
                    Message = Constraints.LOAD_FAILED,
                };
            }

            return new DynamicModelResponse.DynamicModelsResponse<NotificationViewModel>()
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

        public async Task<ResponseResult<NotificationViewModel>> UpdateStatus(int id, NotificationStatus status)
        {
            Notification data = new Notification();
            try
            {
                data = await _repository.GetById(id);
                if (data is null)
                {
                    return new ResponseResult<NotificationViewModel>()
                    {
                        Message = Constraints.NOT_FOUND,
                        result = false,
                    };
                }

                data.Status = (int)status;

                _repository.MotifyEntity(data);

                if (!await _repository.UpdateNotification(data))
                {
                    _repository.DetachEntity(data);
                    throw new Exception();
                }
            }
            catch (Exception)
            {
                return new ResponseResult<NotificationViewModel>()
                {
                    Message = Constraints.UPDATE_FAILED,
                    result = false,
                };
            }
            finally { await _repository.DisponseAsync(); }

            return new ResponseResult<NotificationViewModel>()
            {
                Message = Constraints.UPDATE_SUCCESS,
                result = true,
                Value = _mapper.Map<NotificationViewModel>(data)
            };
        }

        public async Task CreateAndSendNotification(CreateNotificationRequestModel notificationRequestModel)
        {
            Notification notification = new Notification()
            {
                Description = notificationRequestModel.Description,
                NotificationType = (int)notificationRequestModel.NotificationType,
                AccountId = notificationRequestModel.AccountId,
                CreateDate = DateTime.Now,
                Status = (int)NotificationStatus.UNREAD,
            };

            bool createResult = false;
            try
            {
                createResult = await _repository.CreateNotification(notification);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to create notification: {ex.Message}");
                return;
            }

            NotificationResponse notificationResponse = _mapper.Map<NotificationResponse>(notification);
            await _hubContext.Clients.Groups(notificationRequestModel.AccountId.ToString()).SendAsync(NotificationSendingMethodName, notificationResponse);
        }

        public async Task<ResponseResult<List<NotificationResponse>>> GetUserUnreadNotifications(Guid userId)
        {
            var unreadNotifications = await _repository.GetUserUnreadNotification(userId);
            if (unreadNotifications == null || unreadNotifications.Count == 0)
            {
                return new ResponseResult<List<NotificationResponse>>()
                {
                    Message = "Người dùng không có thông báo nào chưa xem.",
                    Value = null,
                    result = false
                };
            }
            var responseModels = _mapper.Map<List<NotificationResponse>>(unreadNotifications);
            return new ResponseResult<List<NotificationResponse>>
            {
                Message = "Tải danh sách thông báo của người dùng thành công",
                Value = responseModels,
                result = true
            };
        }

        public async Task<ResponseResult<bool>> UpdateUnreadNotificationsToRead(Guid userId)
        {
            var unreadNotifications = await _repository.GetUserUnreadNotification(userId);
            if (unreadNotifications.IsNullOrEmpty())
            {
                return new ResponseResult<bool>()
                {
                    Message = "Người dùng không có thông báo chưa xem.",
                    Value = true,
                    result = true,
                };
            }
            try
            {
                foreach (var notification in unreadNotifications)
                {
                    notification.Status = (int)NotificationStatus.READ;
                    await _repository.Update(notification);
                }
                await _repository.SaveChagesAsync();

                return new ResponseResult<bool>()
                {
                    Message = "Thay đổi trạng thái của các thông báo thành công",
                    Value = true,
                    result = true,
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to update status of user's UNREAD notifications to READ: {ex.Message}");
                return new ResponseResult<bool>()
                {
                    Message = "Có lỗi xảy ra trong quá trình thay đổi trạng thái những thông báo của người dùng",
                    Value = false,
                    result = false,
                };
            }
        }

        public async Task<ResponseResult<NotificationResponse>> UpdateNotificationStatus(int notificationId, NotificationStatusUpdateRequest updateRequest)
        {
            var notification = await _repository.GetById(notificationId);
            if (notification == null)
            {
                return new ResponseResult<NotificationResponse>()
                {
                    Message = "Không tìm thấy thông báo cần thay đổi trạng thái.",
                    Value = null,
                    result = false,
                };
            }

            if (notification.Status == (int)updateRequest.NewStatus)
            {
                return new ResponseResult<NotificationResponse>()
                {
                    Message = "Trạng thái mới của thông báo trùng với trạng thái cũ.",
                    Value = null,
                    result = false,
                };
            }

            try
            {
                notification.Status = (int)updateRequest.NewStatus;
                await _repository.Update(notification);
                await _repository.SaveChagesAsync();
                return new ResponseResult<NotificationResponse>()
                {
                    Message = "Thay đổi trạng thái của thông báo thành công",
                    Value = _mapper.Map<NotificationResponse>(notification),
                    result = true,
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to update status of notification with Id '{notificationId}': {ex.Message}");
                return new ResponseResult<NotificationResponse>()
                {
                    Message = "Có lỗi xảy ra trong quá trình thay đổi trạng thái của thông báo. Vui lòng thử lại.",
                    Value = null,
                    result = false,
                };
            }
        }
    }
}
