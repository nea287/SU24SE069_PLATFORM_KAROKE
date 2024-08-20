using AutoMapper;
using AutoMapper.QueryableExtensions;
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

        public NotificationService(IMapper mapper, INotificationRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
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
    }
}
