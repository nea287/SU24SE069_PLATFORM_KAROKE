using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_BusinessLayer.IServices
{
    public interface ITokenService
    {
        public string GenerateRefreshToken(string email, string roleName);
        public (string accessToken, string refreshToken) GenerateAccessToken(string email, string roleName);
    }
}
