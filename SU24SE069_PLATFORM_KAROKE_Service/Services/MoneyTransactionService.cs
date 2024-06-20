using AutoMapper;
using AutoMapper.QueryableExtensions;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.Commons;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.Helpers;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.ReponseModels.Helpers;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.RequestModels.Helpers;
using SU24SE069_PLATFORM_KAROKE_DataAccess.Models;
using SU24SE069_PLATFORM_KAROKE_Repository.IRepository;
using SU24SE069_PLATFORM_KAROKE_Service.IServices;
using SU24SE069_PLATFORM_KAROKE_Service.ReponseModels;
using SU24SE069_PLATFORM_KAROKE_Service.RequestModels.MoneyTransaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace SU24SE069_PLATFORM_KAROKE_Service.Services
{
    public class MoneyTransactionService : IMoneyTransactionService
    {
        private readonly IMapper _mapper;
        private readonly IMoneyTransactionRepository _repository;

        public MoneyTransactionService(IMapper mapper, IMoneyTransactionRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }
        public async Task<ResponseResult<MoneyTransactionViewModel>> CreateTransaction(MoneyTransactionRequestModel request)
        {
            MoneyTransaction rs = new MoneyTransaction();
            try
            {

                rs = _mapper.Map<MoneyTransaction>(request);

                rs.CreatedDate = DateTime.Now;
                rs.Status = (int)PaymentStatus.PENDING;

                if (!await _repository.CreateMoneyTransaction(rs))
                {
                    _repository.DetachEntity(rs);
                    throw new Exception();
                }

            }
            catch (Exception ex)
            {
                return new ResponseResult<MoneyTransactionViewModel>()
                {
                    Message = Constraints.CREATE_FAILED,
                    result = false
                };
            }

            return new ResponseResult<MoneyTransactionViewModel>()
            {
                Message = Constraints.CREATE_SUCCESS,
                result = true,
                Value = _mapper.Map<MoneyTransactionViewModel>(rs)
            };
        }

        public async Task<DynamicModelResponse.DynamicModelsResponse<MoneyTransactionViewModel>> GetTransactions(MoneyTransactionViewModel filter, PagingRequest paging, MoneyTransactionOrderFilter orderFilter)
        {
            (int, IQueryable<MoneyTransactionViewModel>) result;
            try
            {
                lock (_repository)
                {
                    var data = _repository.GetAll(
                                                includeProperties: String.Join(",",
                                                SupportingFeature.GetNameIncludedProperties<MoneyTransaction>()))
                        .AsQueryable()
                        .ProjectTo<MoneyTransactionViewModel>(_mapper.ConfigurationProvider)
                        .DynamicFilter(filter);

                    string? colName = Enum.GetName(typeof(MoneyTransactionOrderFilter), orderFilter);

                    data = SupportingFeature.Sorting(data.AsEnumerable(), (SortOrder)paging.OrderType, colName).AsQueryable();

                    result = data.PagingIQueryable(paging.page, paging.pageSize,
                            Constraints.LimitPaging, Constraints.DefaultPaging);
                }


            }
            catch (Exception ex)
            {
                return new DynamicModelResponse.DynamicModelsResponse<MoneyTransactionViewModel>()
                {
                    Message = Constraints.LOAD_FAILED,
                };
            }

            return new DynamicModelResponse.DynamicModelsResponse<MoneyTransactionViewModel>()
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

        public async Task<ResponseResult<MoneyTransactionViewModel>> UpdateStatusTransaction(Guid id, PaymentStatus status)
        {
            MoneyTransaction rs = new MoneyTransaction();
            try
            {
                rs = await _repository.GetByIdGuid(id);
                if (rs is null)
                {
                    _repository.DetachEntity(rs);

                    return new ResponseResult<MoneyTransactionViewModel>()
                    {
                        Message = Constraints.NOT_FOUND,
                        result = false,
                    };
                }

                rs.Status = (int)status;

                //_repository.DetachEntity(rs);
                //_repository.MotifyEntity(rs);

                if (!await _repository.UpdateMoneyTransaction(rs))
                {
                    _repository.DetachEntity(rs);
                    throw new Exception();
                }

            }
            catch (Exception ex)
            {
                return new ResponseResult<MoneyTransactionViewModel>()
                {
                    Message = Constraints.UPDATE_FAILED,
                    result = false,
                };
            }
            finally { lock (_repository) { } }

            return new ResponseResult<MoneyTransactionViewModel>()
            {
                Message = Constraints.UPDATE_SUCCESS,
                result = true,
                Value = _mapper.Map<MoneyTransactionViewModel>(rs)
            };
        }
    }
}
