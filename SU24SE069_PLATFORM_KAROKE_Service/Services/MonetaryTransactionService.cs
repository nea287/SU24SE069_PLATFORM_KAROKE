using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.Commons;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.Helpers;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.ReponseModels.Helpers;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.RequestModels.Helpers;
using SU24SE069_PLATFORM_KAROKE_DataAccess.Models;
using SU24SE069_PLATFORM_KAROKE_Repository.IRepository;
using SU24SE069_PLATFORM_KAROKE_Repository.Repository;
using SU24SE069_PLATFORM_KAROKE_Service.IServices;
using SU24SE069_PLATFORM_KAROKE_Service.ReponseModels;
using SU24SE069_PLATFORM_KAROKE_Service.RequestModels.MoneyTransaction;
using SU24SE069_PLATFORM_KAROKE_Service.RequestModels.PurchasedSong;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace SU24SE069_PLATFORM_KAROKE_Service.Services
{
    public class MonetaryTransactionService : IMonetaryTransactionService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ISongRepository _songRepository;
        private readonly IMemoryCache _cache;
        private readonly IMapper _mapper;
        private readonly IMonetaryTransactionRepository _repository;

        public MonetaryTransactionService(IMapper mapper, IMonetaryTransactionRepository repository, IMemoryCache cache, IAccountRepository accountRepository, ISongRepository songRepository)
        {
            _accountRepository = accountRepository;
            _songRepository = songRepository;
            _cache = cache;
            _mapper = mapper;
            _repository = repository;
        }
        public async Task<ResponseResult<MonetaryTransactionViewModel>> CreateTransaction(MonetaryTransactionRequestModel request)
        {
            MonetaryTransaction rs = new MonetaryTransaction();
            try
            {

                rs = _mapper.Map<MonetaryTransaction>(request);

                rs.CreatedDate = DateTime.Now;
                rs.Status = (int)PaymentStatus.PENDING;

                if (!await _repository.CreateMoneyTransaction(rs))
                {
                    _repository.DetachEntity(rs);
                    throw new Exception();
                }

            }
            catch (Exception)
            {
                return new ResponseResult<MonetaryTransactionViewModel>()
                {
                    Message = Constraints.CREATE_FAILED,
                    result = false
                };
            }

            return new ResponseResult<MonetaryTransactionViewModel>()
            {
                Message = Constraints.CREATE_SUCCESS,
                result = true,
                Value = _mapper.Map<MonetaryTransactionViewModel>(rs)
            };
        }

        public async Task<DynamicModelResponse.DynamicModelsResponse<MonetaryTransactionViewModel>> GetTransactions(MonetaryTransactionViewModel filter, PagingRequest paging, MonetaryTransactionOrderFilter orderFilter)
        {
            (int, IQueryable<MonetaryTransactionViewModel>) result;
            try
            {
                lock (_repository)
                {
                    var data = _repository.GetAll(
                                                includeProperties: String.Join(",",
                                                SupportingFeature.GetNameIncludedProperties<MonetaryTransaction>()))
                        .AsQueryable()
                        .ProjectTo<MonetaryTransactionViewModel>(_mapper.ConfigurationProvider)
                        .DynamicFilter(filter);

                    string? colName = Enum.GetName(typeof(MonetaryTransactionOrderFilter), orderFilter);

                    data = SupportingFeature.Sorting(data.AsEnumerable(), (SortOrder)paging.OrderType, colName).AsQueryable();

                    result = data.PagingIQueryable(paging.page, paging.pageSize,
                            Constraints.LimitPaging, Constraints.DefaultPaging);
                }


            }
            catch (Exception)
            {
                return new DynamicModelResponse.DynamicModelsResponse<MonetaryTransactionViewModel>()
                {
                    Message = Constraints.LOAD_FAILED,
                };
            }

            return new DynamicModelResponse.DynamicModelsResponse<MonetaryTransactionViewModel>()
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

        public async Task<ResponseResult<MonetaryTransactionViewModel>> UpdateStatusTransaction(Guid id, PaymentStatus status)
        {
            MonetaryTransaction rs = new MonetaryTransaction();
            try
            {
                rs = await _repository.GetByIdGuid(id);
                if (rs is null)
                {
                    _repository.DetachEntity(rs);

                    return new ResponseResult<MonetaryTransactionViewModel>()
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
            catch (Exception)
            {
                return new ResponseResult<MonetaryTransactionViewModel>()
                {
                    Message = Constraints.UPDATE_FAILED,
                    result = false,
                };
            }
            finally { lock (_repository) { } }

            return new ResponseResult<MonetaryTransactionViewModel>()
            {
                Message = Constraints.UPDATE_SUCCESS,
                result = true,
                Value = _mapper.Map<MonetaryTransactionViewModel>(rs)
            };
        }


        public async Task<ResponseResult<MonetaryTransactionViewModel>> PurchaseSong(PurchasedSongRequestModel request)
        {
            try
            {
                Account? data = JsonConvert.DeserializeObject<List<Account>>(SupportingFeature.Instance.GetDataFromCache(_cache, Constraints.ACCOUNTS))?
                                                                                       .FirstOrDefault(x => x.AccountId == request.MemberId);

                Song? dataSong = JsonConvert.DeserializeObject<List<Song>>(SupportingFeature.Instance.GetDataFromCache(_cache, Constraints.SONGS))?
                                                                                       .FirstOrDefault(x => x.SongId == request.SongId);

                if (data is null)
                {
                    data = await _accountRepository.GetByIdGuid(request.MemberId);
                }
                if (dataSong is null)
                {
                    dataSong = await _songRepository.GetByIdGuid(request.SongId);
                }

                if (data.UpBalance < dataSong.Price)
                {
                    return new ResponseResult<MonetaryTransactionViewModel>()
                    {
                        Message = Constraints.INSUFFICIENT_FUNDS,
                        result = false,
                    };
                }

                data.UpBalance = data.UpBalance - dataSong.Price;




            }
            catch (Exception)
            {

            }

            return new ResponseResult<MonetaryTransactionViewModel>()
            {

            };
        }
    }
}
