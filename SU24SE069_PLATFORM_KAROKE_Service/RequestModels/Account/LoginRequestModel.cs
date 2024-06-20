using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Service.RequestModels.Account
{
    public class LoginRequestModel
    {
        [EmailAddress]
        public string email { get; set; } = null!;
        public string password { get; set; } = null!;
    }
}
