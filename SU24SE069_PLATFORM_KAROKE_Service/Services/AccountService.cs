using AutoMapper;
using AutoMapper.QueryableExtensions;
using Castle.Core.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using Org.BouncyCastle.Asn1.Ocsp;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.Commons;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.Helpers;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.IServices;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.ReponseModels;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.ReponseModels.Helpers;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.RequestModels.Account;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.RequestModels.Helpers;
using SU24SE069_PLATFORM_KAROKE_DataAccess.Models;
using SU24SE069_PLATFORM_KAROKE_Repository.IRepository;
using SU24SE069_PLATFORM_KAROKE_Service.RequestModels.Account;
using System.Security.Principal;
using System.Text;
using System.Text.RegularExpressions;

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
            catch (Exception)
            {
                result = new UserLoginResponse()
                {
                    Message = Constraints.LOAD_FAILED,
                    Result = false
                };
            }
            finally
            {
                await _accountRepository.DisponseAsync();

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
                    var data = _mapper.Map<AccountViewModel>(await _accountRepository
                        .GetAccount(id: accountId));

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
            catch (Exception)
            {
                result = new ResponseResult<AccountViewModel>()
                {
                    Message = Constraints.LOAD_FAILED,
                    result = false
                };
            }
            finally
            {
                await _accountRepository.DisponseAsync();

            }

            return result;
        }

        public DynamicModelResponse.DynamicModelsResponse<AccountViewModel> GetAccounts(
            AccountViewModel filter, PagingRequest paging, AccountOrderFilter orderFilter)
        {
            (int, IQueryable<AccountViewModel>) result;
            try
            {
                IQueryable<AccountViewModel>? data = JsonConvert.DeserializeObject<List<AccountViewModel>>(SupportingFeature.Instance.GetDataFromCache(_memoryCache, Constraints.ACCOUNTS))?.AsQueryable();

                if (data.IsNullOrEmpty())
                {
                    data = _accountRepository.GetAll(
                            includeProperties: String.Join(",",
                            SupportingFeature.GetNameIncludedProperties<Account>()))
                            .AsQueryable()

                            .ProjectTo<AccountViewModel>(_mapper.ConfigurationProvider);



                    SupportingFeature.Instance.SetDataToCache(_memoryCache, Constraints.ACCOUNTS, JsonConvert.SerializeObject(data.ToList()), 10);
                }
                lock (_accountRepository)
                {


                    data = data.DynamicFilter(filter);

                    string? colName = Enum.GetName(typeof(AccountOrderFilter), orderFilter);

                    data = SupportingFeature.Sorting(data.AsEnumerable(), (SortOrder)paging.OrderType, colName).AsQueryable();

                    result = data.PagingIQueryable(paging.page, paging.pageSize,
                            Constraints.LimitPaging, Constraints.DefaultPaging);
                }


            }
            catch (Exception)
            {
                //await _accountRepository.DisponseAsync();
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
                    data.AccountStatus = (int)AccountStatus.ACTIVE;
                    data.CreatedTime = DateTime.Now;

                    data.Password = BCrypt.Net.BCrypt.HashPassword(data.Password, 12);

                    if (!_accountRepository.CreateAccount(data).Result)
                    {
                        _accountRepository.DetachEntity(data);
                        throw new Exception();
                    }

                    result = _mapper.Map<AccountViewModel>(data);

                    SupportingFeature.Instance.RemoveDataFromCache(_memoryCache, Constraints.ACCOUNTS);
                };

            }
            catch (Exception)
            {
                return new ResponseResult<AccountViewModel>()
                {
                    Message = Constraints.CREATE_FAILED,
                    result = false,
                };
            }
            finally
            {
                await _accountRepository.DisponseAsync();

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
                    data.AccountStatus = (int)AccountStatus.ACTIVE;

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

                    if (!_accountRepository.UpdateAccount(data).Result)
                    {
                        _accountRepository.DetachEntity(data);
                        throw new Exception();
                    }

                    result = _mapper.Map<AccountViewModel>(data);
                    SupportingFeature.Instance.RemoveDataFromCache(_memoryCache, Constraints.ACCOUNTS);

                };


            }
            catch (Exception)
            {
                await _accountRepository.DisponseAsync();
                return new ResponseResult<AccountViewModel>()
                {
                    Message = Constraints.UPDATE_FAILED,
                    result = false,
                    Value = result
                };
            }
            finally
            {
                await _accountRepository.DisponseAsync();

            }

            return new ResponseResult<AccountViewModel>()
            {
                Message = Constraints.UPDATE_SUCCESS,
                result = true,
                Value = result
            };
        }

        public async Task<ResponseResult<AccountViewModel>> DeleteAccount(Guid id)
        {
            try
            {
                var data = await _accountRepository.GetAccount(id);
                if (data is null)
                {
                    return new ResponseResult<AccountViewModel>()
                    {
                        Message = Constraints.NOT_FOUND,
                        result = false,
                    };
                }

                data.AccountStatus = (int)AccountStatus.INACTIVE;

                if (!await _accountRepository.UpdateAccount(data))
                {
                    _accountRepository.DetachEntity(data);
                    throw new Exception();
                }

                SupportingFeature.Instance.RemoveDataFromCache(_memoryCache, Constraints.ACCOUNTS);

            }
            catch (Exception)
            {
                await _accountRepository.DisponseAsync();
                return new ResponseResult<AccountViewModel>()
                {
                    Message = Constraints.DELETE_FAILED,
                    result = false,
                };
            }
            finally
            {
                await _accountRepository.DisponseAsync();

            }

            return new ResponseResult<AccountViewModel>()
            {
                Message = Constraints.DELETE_SUCCESS,
                result = true,
            };
        }
        public bool SendVerificationCode(string receiverMail)
        {
            try
            {
                string code = SupportingFeature.Instance.GenerateCode();
                SupportingFeature.Instance.SendEmail(receiverMail, code, "Mã xác thực");

                SupportingFeature.Instance.SetDataToCache(_memoryCache, code, "verification-code", 5);

            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public async Task<ResponseResult<AccountViewModel>> SignUp(CreateAccount1RequestModel request, string verificationCode)
        {
            AccountViewModel result = new AccountViewModel();

            if (verificationCode != SupportingFeature.Instance.GetDataFromCache(_memoryCache, "verification-code"))
            {
                return new ResponseResult<AccountViewModel>()
                {
                    Message = Constraints.INVALID_VERIFICATION_CODE,
                    result = false,
                };
            }

            try
            {
                lock (_accountRepository)
                {
                    if (_accountRepository.ExistedAccount(email: request.Email))
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
                    data.AccountStatus = (int)AccountStatus.ACTIVE;
                    data.CreatedTime = DateTime.Now;
                    data.Role = (int)AccountRole.MEMBER;
                    data.UpBalance = 0;
                    data.AccountStatus = (int)AccountStatus.ACTIVE;

                    data.Password = BCrypt.Net.BCrypt.HashPassword(data.Password, 12);

                    if (!_accountRepository.CreateAccount(data).Result)
                    {
                        _accountRepository.DetachEntity(data);
                        throw new Exception();
                    }

                    result = _mapper.Map<AccountViewModel>(data);
                    SupportingFeature.Instance.RemoveDataFromCache(_memoryCache, Constraints.ACCOUNTS);

                };

            }
            catch (Exception)
            {
                await _accountRepository.DisponseAsync();
                return new ResponseResult<AccountViewModel>()
                {
                    Message = Constraints.CREATE_FAILED,
                    result = false,
                };
            }
            finally
            {
                await _accountRepository.DisponseAsync();

            }

            return new ResponseResult<AccountViewModel>()
            {
                Message = Constraints.CREATE_SUCCESS,
                result = true,
                Value = result
            };
        }
        #endregion


        #region Redis

        public void SetCache(string value, string nameValue, int minutes)
        {
            DistributedCacheEntryOptions options = new DistributedCacheEntryOptions()
            .SetAbsoluteExpiration(DateTime.Now.AddMinutes(minutes))
            .SetSlidingExpiration(TimeSpan.FromMinutes(minutes));

            var dataToCache = Encoding.UTF8.GetBytes(value);
            _cache.Set(nameValue, dataToCache, options);
        }

        public string? GetCache(string nameValue)
        {
            var data = _cache.Get(nameValue);

            return data != null ? Encoding.UTF8.GetString(data) : null;
        }

        public async Task<ResponseResult<AccountViewModel>> ActiveAccount(Guid id)
        {
            AccountViewModel result = new AccountViewModel();
            try
            {
                var data = await _accountRepository.GetByIdGuid(id);

                data.AccountStatus = (int)AccountStatus.ACTIVE;

                _accountRepository.MotifyEntity(data);

                if (data == null)
                {
                    _accountRepository.DetachEntity(data);
                    return new ResponseResult<AccountViewModel>()
                    {
                        Message = Constraints.NOT_FOUND,
                        result = false,
                        Value = _mapper.Map<AccountViewModel>(data)
                    };
                }

                if (!await _accountRepository.UpdateAccount(data))
                {
                    _accountRepository.DetachEntity(data);
                    throw new Exception();
                }

                result = _mapper.Map<AccountViewModel>(data);



            }
            catch (Exception)
            {
                return new ResponseResult<AccountViewModel>()
                {
                    Message = Constraints.UPDATE_FAILED,
                    result = false,
                    Value = result
                };
            }
            finally
            {
                lock (_accountRepository) { };
            }

            return new ResponseResult<AccountViewModel>()
            {
                Message = Constraints.UPDATE_SUCCESS,
                result = true,
                Value = result
            };
        }
        #endregion

        #region SignUp

        public async Task<ResponseResult<AccountViewModel>> CreateNewMemberAccount(MemberSignUpRequest signUpRequest)
        {
            // Validate email
            var emailValidation = _accountRepository.IsAccountEmailExisted(signUpRequest.Email);
            if (emailValidation)
            {
                return new ResponseResult<AccountViewModel>()
                {
                    Message = $"{Constraints.INFORMATION_EXISTED} Email {signUpRequest.Email} đã được sử dụng để đăng ký 1 tài khoản khác.",
                    result = false,
                };
            }
            // Validate username
            var usernameValidation = _accountRepository.IsAccountUsernameExisted(signUpRequest.Username);
            if (usernameValidation)
            {
                return new ResponseResult<AccountViewModel>()
                {
                    Message = $"{Constraints.INFORMATION_EXISTED} Tên hiển thị {signUpRequest.Username} đã tồn tại, vui lòng sử dụng tên hiển thị khác.",
                    result = false,
                };
            }

            // Create new account
            Guid newAccountId = Guid.NewGuid();
            Account newAccount = new Account()
            {
                AccountId = newAccountId,
                UserName = signUpRequest.Username,
                Password = BCrypt.Net.BCrypt.HashPassword(signUpRequest.Password, 12),
                Email = signUpRequest.Email,
                Gender = (int)signUpRequest.Gender,
                Role = (int)AccountRole.MEMBER,
                IsOnline = false,
                Fullname = null,
                Yob = null,
                IdentityCardNumber = null,
                PhoneNumber = null,
                CreatedTime = DateTime.Now,
                CharacterItemId = null,
                RoomItemId = null,
                AccountStatus = (int)AccountStatus.NOT_VERIFY,
                UpBalance = 0,
            };

            bool createResult = false;
            try
            {
                createResult = await _accountRepository.CreateAccount(newAccount);
            }
            catch (Exception ex)
            {
                return new ResponseResult<AccountViewModel>()
                {
                    Message = $"{Constraints.CREATE_FAILED} Có lỗi xảy ra trong quá trình lưu tài khoản member mới: {ex.Message}",
                    result = false,
                };
            }
            if (!createResult)
            {
                return new ResponseResult<AccountViewModel>()
                {
                    Message = $"{Constraints.CREATE_FAILED} Có lỗi xảy ra trong quá trình tạo tài khoản member mới.",
                    result = false,
                };
            }
            var result = _mapper.Map<AccountViewModel>(newAccount);
            return new ResponseResult<AccountViewModel>()
            {
                Message = $"{Constraints.CREATE_SUCCESS} Tài khoản member mới đã được đăng ký.",
                result = true,
                Value = result,
            };
        }

        public async Task<(bool, string)> SendVerificationEmail(string accountEmail)
        {
            Regex emailRegex = new Regex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,6}$");
            if (!emailRegex.IsMatch(accountEmail)) 
            {
                return (false, $"{accountEmail} không phải là địa chỉ email hợp lệ.");
            }
            var account = await _accountRepository.GetAccountByMail(accountEmail);
            if (account == null)
            {
                return (false, $"Không tìm thấy tài khoản được đăng ký bởi email {accountEmail}.");
            }
            if (account.AccountStatus != (int)AccountStatus.NOT_VERIFY) 
            {
                return (false, $"Tài khoản được đăng ký bởi email {accountEmail} không hợp lệ để xác thực (Trạng thái: {(AccountStatus)account.AccountStatus!}).");
            }
            try
            {
                string verifyCode = SupportingFeature.Instance.GenerateCode();
                string verifyCodeKey = accountEmail + "_verify_code";
                string emailContent = $"Mã xác thực tài khoản: {verifyCode}.\nMã xác thực sẽ có hiệu lực trong 5 phút, vui lòng xác thực tài khoản trong thời gian này.";
                SupportingFeature.Instance.SendEmail(accountEmail, emailContent, "[KOK] Xác Thực Tài Khoản");
                SupportingFeature.Instance.SetDataToCache(_memoryCache, verifyCodeKey, verifyCode, 5);
            }
            catch (Exception ex)
            {
                return (false, $"Có lỗi xảy ra trong quá trình gửi email xác thực tài khoản: {ex.Message}");
            }
            return (true, $"Gửi mã xác thực tài khoản đến email {accountEmail} thành công.");
        }

        public async Task<ResponseResult<AccountViewModel>> VerifyMemberAccount(MemberAccountVerifyRequest verifyRequest)
        {
            var account = await _accountRepository.GetAccountByMail(verifyRequest.AccountEmail);
            if (account == null)
            {
                return new ResponseResult<AccountViewModel>()
                {
                    Message = Constraints.NOT_FOUND + $" Không tìm thấy tài khoản được đăng ký bởi email {verifyRequest.AccountEmail}.",
                    result = false,
                };
            }
            if (account.AccountStatus != (int)AccountStatus.NOT_VERIFY)
            {
                return new ResponseResult<AccountViewModel>()
                {
                    Message = Constraints.UPDATE_FAILED + $" Tài khoản được đăng ký bởi email {verifyRequest.AccountEmail} không hợp lệ để xác thực (Trạng thái: {(AccountStatus)account.AccountStatus!}).",
                    result = false,
                };
            }
            string verifyCodeKey = verifyRequest.AccountEmail + "_verify_code";

            if (verifyRequest.VerifyCode != SupportingFeature.Instance.GetDataFromCache(_memoryCache, verifyCodeKey))
            {
                return new ResponseResult<AccountViewModel>()
                {
                    Message = Constraints.UPDATE_FAILED + Constraints.INVALID_VERIFICATION_CODE,
                    result = false,
                };
            }

            // Update account status to ACTIVE
            account.AccountStatus = (int)AccountStatus.ACTIVE;
            bool result = false;
            try
            {
                result = await _accountRepository.UpdateAccount(account);
            }
            catch (Exception ex)
            {
                return new ResponseResult<AccountViewModel>()
                {
                    Message = Constraints.UPDATE_FAILED + $" Có lỗi xảy ra trong quá trình xác thực tài khoản: {ex.Message}",
                    result = false,
                };
            }
            if (!result)
            {
                return new ResponseResult<AccountViewModel>()
                {
                    Message = Constraints.UPDATE_FAILED + $" Có lỗi xảy ra trong quá trình xác thực tài khoản.",
                    result = false,
                };
            }

            SupportingFeature.Instance.RemoveDataFromCache(_memoryCache, verifyCodeKey);
            var accountModel = _mapper.Map<AccountViewModel>(account);
            return new ResponseResult<AccountViewModel>()
            {
                Message = $"{Constraints.UPDATE_SUCCESS} Tài khoản member mới đã được xác thực.",
                result = true,
                Value = accountModel,
            };
        }

        #endregion
    }
}