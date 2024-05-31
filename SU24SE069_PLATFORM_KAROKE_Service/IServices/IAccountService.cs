using SU24SE069_PLATFORM_KAROKE_BusinessLayer.ReponseModels.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_BusinessLayer.IServices
{
    public interface IAccountService
    {
        public UserLoginResponse Login(string username, string password);
    }
}
