using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.Commons;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.Helpers;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.IServices;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.ReponseModels;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.ReponseModels.Helpers;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.RequestModels.Account;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.RequestModels.Helpers;
using SU24SE069_PLATFORM_KAROKE_DataAccess.Models;
using SU24SE069_PLATFORM_KAROKE_Repository.IRepository;
using SU24SE069_PLATFORM_KAROKE_Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_BusinessLayer.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;
        private readonly IDistributedCache _cache;
        private readonly IMemoryCache _memoryCache;
        private readonly ITokenService _token;

        public AccountService(IAccountRepository repository, IMapper mapper, ITokenService token, IDistributedCache cache, IMemoryCache memoryCache)
        {
            _accountRepository = repository;
            _mapper = mapper;
            _cache = cache;
            _memoryCache = memoryCache;
            _token = token;
        }
        #region Authenticate
        public async Task<UserLoginResponse> Login(string email, string password)
        {
            UserLoginResponse result = new UserLoginResponse();
            try
            {
                lock (_accountRepository)
                {
                    var data = _accountRepository
                        .Login(email).Result;

                    if (data != null && BCrypt.Net.BCrypt.Verify(password, data.Password))
                    {
                        string roleName = Enum.GetName(typeof(AccountRole), data.Role);

                        var dataToken = _token.GenerateAccessToken(email, roleName);
                        result = new UserLoginResponse()
                        {
                            Message = Constraints.INFORMATION,
                            Value = _mapper.Map<AccountViewModel>(data),
                            Result = true,
                            AccessToken = dataToken.accessToken,
                            RefreshToken = dataToken.refreshToken
                        };
                    }
                    else
                    {
                        result = new UserLoginResponse()
                        {
                            Message = Constraints.EMAIL_PASSWORD_INVALID,
                            Result = false
                        };

                    }

                }
            }
            catch (Exception ex)
            {
                result = new UserLoginResponse()
                {
                    Message = Constraints.LOAD_FAILED,
                    Result = false
                };
            }

            return result;
        }
        #endregion

        #region Read
        public async Task<ResponseResult<AccountViewModel>> GetAccount(Guid accountId)
        {
            ResponseResult<AccountViewModel> result = new ResponseResult<AccountViewModel>();
            try
            {
                lock (_accountRepository)
                {
                    var data = _mapper.Map<AccountViewModel>(_accountRepository
                        .GetAccount(id: accountId).Result);

                    result = data == null ?
                        new ResponseResult<AccountViewModel>()
                        {
                            Message = Constraints.NOT_FOUND,
                            result = false
                        }
                        :
                        new ResponseResult<AccountViewModel>()
                        {
                            Message = Constraints.INFORMATION,
                            Value = data,
                            result = true
                        };

                }
            }
            catch (Exception ex)
            {
                result = new ResponseResult<AccountViewModel>()
                {
                    Message = Constraints.LOAD_FAILED,
                    result = false
                };
            }

            return result;
        }

        public DynamicModelResponse.DynamicModelsResponse<AccountViewModel> GetAccounts(
            AccountViewModel filter, PagingRequest paging, AccountOrderFilter orderFilter)
        {
            (int, IQueryable<AccountViewModel>) result;
            try
            {
                lock (_accountRepository)
                {
                    var data = _accountRepository.GetAll(
                                                includeProperties: String.Join(",",
                                                SupportingFeature.GetNameIncludedProperties<Account>()))
                        .AsQueryable()

                        .ProjectTo<AccountViewModel>(_mapper.ConfigurationProvider)
                        .DynamicFilter(filter);

                    string? colName = Enum.GetName(typeof(AccountOrderFilter), orderFilter);

                    data = SupportingFeature.Sorting(data.AsEnumerable(), (SortOrder)paging.OrderType, colName).AsQueryable();

                    result = data.PagingIQueryable(paging.page, paging.pageSize,
                            Constraints.LimitPaging, Constraints.DefaultPaging);
                }


            }
            catch (Exception ex)
            {
                return new DynamicModelResponse.DynamicModelsResponse<AccountViewModel>()
                {
                    Message = Constraints.LOAD_FAILED,
                };
            }

            return new DynamicModelResponse.DynamicModelsResponse<AccountViewModel>()
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
            #endregion

            #region Create
            public async Task<ResponseResult<AccountViewModel>> CreateAccount(CreateAccountRequestModel request)
        {
            AccountViewModel result = new AccountViewModel();
            try
            {
                lock (_accountRepository)
                {
                    if (_accountRepository.ExistedAccount(request.Email, request.UserName))
                    {
                        return new ResponseResult<AccountViewModel>()
                        {
                            Message = Constraints.INFORMATION_EXISTED,
                            result = false,
                        };
                    }

                    var data = _mapper.Map<Account>(request);

                    data.Email = data.Email.ToLower();
                    data.UserName = data.UserName.ToLower();
                    data.IsOnline = false;
                    data.IsVerified = true;
                    data.CreatedTime = DateTime.Now;

                    data.Password = BCrypt.Net.BCrypt.HashPassword(data.Password, 12);

                    if (!_accountRepository.CreateAccount(data).Result)
                    {
                        throw new Exception();
                    } 

                    result = _mapper.Map<AccountViewModel>(data);
                };

            }
            catch (Exception ex)
            {
                return new ResponseResult<AccountViewModel>()
                {
                    Message = Constraints.CREATE_FAILED,
                    result = false,
                };
            }

            return new ResponseResult<AccountViewModel>()
            {
                Message = Constraints.CREATE_SUCCESS,
                result = true,
                Value = result
            };
        }
        #endregion

        #region Update
        public async Task<ResponseResult<AccountViewModel>> UpdateAccountByEmail(string email, UpdateAccountByMailRequestModel request)
        {
            AccountViewModel result = new AccountViewModel();
            try
            {
                lock (_accountRepository)
                {
                    var data1 = _accountRepository.GetAccountByMail(email).Result;

                    

                    var data = _mapper.Map<Account>(request);

                    data.UserName = data1.UserName;
                    data.Email = data1.Email;
                    data.Role = data1.Role;
                    data.IsOnline = true;
                    data.Role = data1.Role;
                    data.AccountId = data1.AccountId;
                    data.CreatedTime = data1.CreatedTime;

                    data.Password = BCrypt.Net.BCrypt.HashPassword(request.Password, 12);

                    _accountRepository.DetachEntity(data1);
                    _accountRepository.MotifyEntity(data);  

                    if (data == null)
                    {
                        return new ResponseResult<AccountViewModel>()
                        {
                            Message = Constraints.NOT_FOUND,
                            result = false,
                            Value = _mapper.Map<AccountViewModel>(data)
                        };
                    }

                    if(!_accountRepository.UpdateAccountByMail(email, data).Result)
                    {
                        throw new Exception();
                    }

                    result = _mapper.Map<AccountViewModel>(data);
                };


            }
            catch (Exception ex)
            {
                return new ResponseResult<AccountViewModel>()
                {
                    Message = Constraints.UPDATE_FAILED,
                    result = false,
                    Value = result
                };
            }

            return new ResponseResult<AccountViewModel>()
            {
                Message = Constraints.UPDATE_SUCCESS,
                result = true,
                Value = result
            };
        }
        #endregion
    }
}
//0.48 48.764