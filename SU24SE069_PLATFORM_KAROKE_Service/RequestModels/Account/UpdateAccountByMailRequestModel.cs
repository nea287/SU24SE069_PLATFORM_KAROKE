using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_BusinessLayer.RequestModels.Account
{
    public class UpdateAccountByMailRequestModel
    {
        public string Password { get; set; } = null!;
        public int Gender { get; set; }
        public string AccountName { get; set; } = null!;
        public int Star { get; set; }
        public string? Fullname { get; set; }
        public int? Yob { get; set; }
        public string? IdentityCardNumber { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
