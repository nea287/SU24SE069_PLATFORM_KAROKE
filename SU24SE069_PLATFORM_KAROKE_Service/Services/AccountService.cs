using AutoMapper;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.Commons;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.IServices;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.ReponseModels;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.ReponseModels.Helpers;
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
        public UserLoginResponse Login(string username, string password)
        {
            UserLoginResponse result = new UserLoginResponse();
            try
            {
                lock (_accountRepository)
                {
                    var data = _mapper.Map<AccountViewModel>(_accountRepository
                        .Login(username, password));

                    if (data != null)
                    {
                        string roleName = Enum.GetName(typeof(AccountRole), data.Role);

                        var dataToken = _token.GenerateAccessToken(username, roleName);
                        result = new UserLoginResponse()
                        {
                            Message = Constraints.INFORMATION,
                            Value = data,
                            Result = true,
                            AccessToken = dataToken.accessToken,
                            RefreshToken = dataToken.refreshToken
                        };
                    }
                    else
                    {
                        result = new UserLoginResponse()
                        {
                            Message = Constraints.NOT_FOUND,
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
    }
}
