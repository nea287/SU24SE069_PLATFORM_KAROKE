using SU24SE069_PLATFORM_KAROKE_BusinessLayer.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Service.RequestModels.Account
{
    public class UpdateAccountRequestModel
    {
        public AccountGender? Gender { get; set; }
        public AccountRole? Role { get; set; }
        public string? Fullname { get; set; }
        public int? Yob { get; set; }
        public string? IdentityCardNumber { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }

    }
}
