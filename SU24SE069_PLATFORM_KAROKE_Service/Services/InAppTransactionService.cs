﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using Castle.Core.Internal;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.Commons;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.Helpers;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.ReponseModels.Helpers;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.RequestModels.Helpers;
using SU24SE069_PLATFORM_KAROKE_DataAccess.Models;
using SU24SE069_PLATFORM_KAROKE_Repository.IRepository;
using SU24SE069_PLATFORM_KAROKE_Service.IServices;
using SU24SE069_PLATFORM_KAROKE_Service.ReponseModels;
using SU24SE069_PLATFORM_KAROKE_Service.RequestModels.InAppTransaction;
using SU24SE069_PLATFORM_KAROKE_Service.RequestModels.PurchasedSong;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace SU24SE069_PLATFORM_KAROKE_Service.Services
{
    public class InAppTransactionService : IInAppTransactionService
    {
        private readonly IMemoryCache _cache;
        private readonly IMapper _mapper;
        private readonly IInAppTransactionRepository _repository;
        private readonly IAccountRepository _accountRepository;
        private readonly ISongRepository _songRepository;

        public InAppTransactionService(IMapper mapper, IInAppTransactionRepository repository, IMemoryCache cache, IAccountRepository accountRepository, ISongRepository songRepository)
        {
            _cache = cache;
            _mapper = mapper;
            _repository = repository;
            _accountRepository = accountRepository;
            _songRepository = songRepository;
        }
        public async Task<ResponseResult<InAppTransactionViewModel>> CreateInAppTransaction(CrreateInAppTransactionRequestModel request)
        {
            InAppTransaction rs = new InAppTransaction();
            try
            {
                lock (_repository)
                {
                    rs = _mapper.Map<InAppTransaction>(request);
                    rs.CreatedDate = DateTime.Now;

                    if (!rs.PurchasedSongs.IsNullOrEmpty())
                    {
                        rs.PurchasedSongs = rs.PurchasedSongs.Select(item => { item.PurchaseDate = DateTime.Now; return item; }).ToList();
                    }

                    if (!_repository.CreateInAppTransaction(rs).Result)
                    {
                        _repository.DetachEntity(rs);
                        throw new Exception();
                    }
                }
            }
            catch (Exception)
            {
                return new ResponseResult<InAppTransactionViewModel>()
                {
                    Message = Constraints.CREATE_FAILED,
                    result = false
                };
            }

            return new ResponseResult<InAppTransactionViewModel>()
            {
                Message = Constraints.CREATE_SUCCESS,
                result = true,
                Value = _mapper.Map<InAppTransactionViewModel>(rs)
            };
        }

        public async Task<ResponseResult<InAppTransactionViewModel>> GetTransaction(Guid id)
        {
            InAppTransaction data = new InAppTransaction();
            try
            {
                data = await _repository.GetByIdGuid(id);

                if(data is null)
                {
                    return new ResponseResult<InAppTransactionViewModel>()
                    {
                        Message = Constraints.NOT_FOUND,
                        result = false,
                    };
                }
            }catch(Exception)
            {
                return new ResponseResult<InAppTransactionViewModel>()
                {
                    Message = Constraints.LOAD_FAILED,
                    result = false
                };
            }

            return new ResponseResult<InAppTransactionViewModel>()
            {
                Message = Constraints.INFORMATION,
                Value = _mapper.Map<InAppTransactionViewModel>(data),
                result = true,
            };
        }

        public async Task<DynamicModelResponse.DynamicModelsResponse<InAppTransactionViewModel>> GetTransactions(InAppTransactionViewModel filter, PagingRequest paging, InAppTransactionOrderFilter orderFilter)
        {
            (int, IQueryable<InAppTransactionViewModel>) result;
            try
            {
                lock (_repository)
                {
                    var data = _repository.GetAll(
                                                includeProperties: String.Join(",",
                                                SupportingFeature.GetNameIncludedProperties<InAppTransaction>()))
                        .AsQueryable()
                        .ProjectTo<InAppTransactionViewModel>(_mapper.ConfigurationProvider)
                        .DynamicFilter(filter);

                    string? colName = Enum.GetName(typeof(InAppTransactionOrderFilter), orderFilter);

                    data = SupportingFeature.Sorting(data.AsEnumerable(), (SortOrder)paging.OrderType, colName).AsQueryable();

                    result = data.PagingIQueryable(paging.page, paging.pageSize,
                            Constraints.LimitPaging, Constraints.DefaultPaging);
                }


            }
            catch (Exception)
            {
                return new DynamicModelResponse.DynamicModelsResponse<InAppTransactionViewModel>()
                {
                    Message = Constraints.LOAD_FAILED,
                };
            }

            return new DynamicModelResponse.DynamicModelsResponse<InAppTransactionViewModel>()
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

        public async Task<ResponseResult<InAppTransactionViewModel>> UpdateInAppTransaction(UpdateInAppTransactionRequestModel request, Guid id)
        {
            InAppTransaction rs = new InAppTransaction();
            try
            {
                lock (_repository)
                {
                    var data = _repository.GetByIdGuid(id).Result;
                    if (data is null)
                    {
                        return new ResponseResult<InAppTransactionViewModel>()
                        {
                            Message = Constraints.NOT_FOUND,
                            result = false,
                        };
                    }
                    rs = data;
                    rs.Status = (int)request.Status;

                    rs.InAppTransactionId = id;

                    _repository.DetachEntity(data);
                    _repository.MotifyEntity(rs);

                    if (!_repository.UpdateInAppTransaction(id, rs).Result)
                    {
                        _repository.DetachEntity(rs);
                        throw new Exception();
                    }
                }
            }
            catch (Exception)
            {
                return new ResponseResult<InAppTransactionViewModel>()
                {
                    Message = Constraints.UPDATE_FAILED,
                    result = false,
                };
            }

            return new ResponseResult<InAppTransactionViewModel>()
            {
                Message = Constraints.UPDATE_SUCCESS,
                result = true,
                Value = _mapper.Map<InAppTransactionViewModel>(rs)
            };
        }


        public async Task<ResponseResult<InAppTransactionViewModel>> PurchaseSong(PurchasedSongRequestModel request)
        {
            InAppTransaction transation = new InAppTransaction();
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
                    return new ResponseResult<InAppTransactionViewModel>()
                    {
                        Message = Constraints.INSUFFICIENT_FUNDS,
                        result = false,
                    };
                }

                data.UpBalance = data.UpBalance - dataSong.Price;
                transation = new InAppTransaction()
                {
                    CreatedDate = DateTime.UtcNow,
                    MemberId = request.MemberId,
                    SongId = request.SongId,
                    Status = (int)PaymentStatus.PENDING,
                    UpAmountBefore = dataSong.Price,
                    UpTotalAmount = dataSong.Price,
                    TransactionType = (int)PaymentType.MOMO,
                    PurchasedSongs = new List<PurchasedSong>()
                    {
                        new PurchasedSong()
                        {
                            PurchaseDate = DateTime.Now,
                            MemberId=request.MemberId,
                            SongId=request.SongId,  
                        }
                    }

                };

                await _accountRepository.SaveChagesAsync();
                
                if(!await _repository.CreateInAppTransaction(transation))
                {
                    _accountRepository.DetachEntity(data);
                    _songRepository.DetachEntity(dataSong);
                    _repository.DetachEntity(transation);   

                    throw new Exception();
                }


            }
            catch (Exception)
            {
                return new ResponseResult<InAppTransactionViewModel>()
                {
                    Message = Constraints.PURCHASE_FAILED,
                    result = false,
                };
            }

            return new ResponseResult<InAppTransactionViewModel>()
            {
                Message = Constraints.PURCHASE_SUCCESS,
                result = true,
                Value = _mapper.Map<InAppTransactionViewModel>(transation)
            };
        }

    }
}
