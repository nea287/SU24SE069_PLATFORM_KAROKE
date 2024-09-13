using AutoMapper;
using AutoMapper.QueryableExtensions;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.Commons;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.Helpers;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.ReponseModels.Helpers;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.RequestModels.Helpers;
using SU24SE069_PLATFORM_KAROKE_DataAccess.Models;
using SU24SE069_PLATFORM_KAROKE_Repository.IRepository;
using SU24SE069_PLATFORM_KAROKE_Service.Filters;
using SU24SE069_PLATFORM_KAROKE_Service.IServices;
using SU24SE069_PLATFORM_KAROKE_Service.ReponseModels;
using SU24SE069_PLATFORM_KAROKE_Service.RequestModels.Conversation;
using SU24SE069_PLATFORM_KAROKE_Service.RequestModels.LiveChat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Service.Services
{
    public class ConversationService : IConversationService
    {
        private readonly IConversationRepository _repository;
        private readonly IChatHubRepository _hubRepository;
        private readonly IMapper _mapper;

        public ConversationService(IConversationRepository repository, IChatHubRepository hubRepository, IMapper mapper)
        {
            _repository = repository;
            _hubRepository = hubRepository;
            _mapper = mapper;
        }
        public async Task<ResponseResult<ConversationViewModel>> CreateConversation(ConversationRequestModel request)
        {
            Conversation rs = new Conversation();
            try
            {
                rs = _mapper.Map<Conversation>(request);


                if (!await _repository.CreateConversation(rs))
                {
                    _repository.DetachEntity(rs);
                    throw new Exception();
                }
            }
            catch (Exception)
            {
                return new ResponseResult<ConversationViewModel>()
                {
                    Message = Constraints.CREATE_FAILED,
                    result = false
                };
            }
            finally { lock (_repository) { } }

            return new ResponseResult<ConversationViewModel>()
            {
                Message = Constraints.CREATE_SUCCESS,
                result = true,
                Value = _mapper.Map<ConversationViewModel>(rs)
            };
        }


        public DynamicModelResponse.DynamicModelsResponse<ConversationViewModel> GetConversationOfMember(Guid memberId, ConversationFilter filter, PagingRequest paging)
        {
            (int, IQueryable<ConversationViewModel>) result;
            try
            {
                lock (_repository)
                {
                    var data = _repository.GetConversationOfMember(
                                                include: String.Join(",",
                                                SupportingFeature.GetNameIncludedProperties<Conversation>()), memberId)
                        .ProjectTo<ConversationViewModel>(_mapper.ConfigurationProvider)
                        .DynamicFilter(_mapper.Map<ConversationViewModel>(filter));


                    //data = SupportingFeature.Sorting(data.AsEnumerable(), (SortOrder)paging.OrderType, colName).AsQueryable();
                    result = data.PagingIQueryable(paging.page, paging.pageSize,
                            Constraints.LimitPaging, Constraints.DefaultPaging);
                }


            }
            catch (Exception)
            {
                return new DynamicModelResponse.DynamicModelsResponse<ConversationViewModel>()
                {
                    Message = Constraints.LOAD_FAILED,
                };
            }

            return new DynamicModelResponse.DynamicModelsResponse<ConversationViewModel>()
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

        public async Task<DynamicModelResponse.DynamicModelsResponse<ConversationViewModel>> GetConversations(ConversationViewModel filter, PagingRequest paging, ConversationOrderFilter orderFilter)
        {
            (int, IQueryable<ConversationViewModel>) result;
            try
            {
                lock (_repository)
                {
                    var data = _repository.GetAll(
                                                includeProperties: String.Join(",",
                                                SupportingFeature.GetNameIncludedProperties<Conversation>()))
                        .AsQueryable()
                        .ProjectTo<ConversationViewModel>(_mapper.ConfigurationProvider)
                        .DynamicFilter(filter);

                    string? colName = Enum.GetName(typeof(ConversationOrderFilter), orderFilter);

                    data = SupportingFeature.Sorting(data.AsEnumerable(), (SortOrder)paging.OrderType, colName).AsQueryable();

                    result = data.PagingIQueryable(paging.page, paging.pageSize,
                            Constraints.LimitPaging, Constraints.DefaultPaging);
                }


            }
            catch (Exception)
            {
                return new DynamicModelResponse.DynamicModelsResponse<ConversationViewModel>()
                {
                    Message = Constraints.LOAD_FAILED,
                };
            }

            return new DynamicModelResponse.DynamicModelsResponse<ConversationViewModel>()
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

        public async Task<bool> SendPrivateMessage(ChatConversationRequestModel request)
        {
            try
            {
                await _hubRepository.SendPrivateMessage(request.Message.ReceiverId, request.Message.SenderName, request.Message.Message);

                var rs = _mapper.Map<Conversation>(request);

                if (!rs.Messages.Any())
                {

                    rs.Messages = rs.Messages
                         .Select(message => { message.TimeStamp = DateTime.Now; 
                             message.SenderId = rs.MemberId1; 
                             message.Content = request.Message.Message; 
                             return message; })
                         .ToList();
                }

                if (!await _repository.CreateConversation(rs))
                {
                    _repository.DetachEntity(rs);
                    throw new Exception();
                }

            }catch(Exception)
            {
                return false;
            }

            return true;

        }

        public Task<bool> SendPublicMessage(string user, string message)
        {
            throw new NotImplementedException();
        }
    }
}
