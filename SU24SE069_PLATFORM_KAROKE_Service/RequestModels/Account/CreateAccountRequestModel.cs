using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_BusinessLayer.RequestModels.Account
{
    public class CreateAccountRequestModel
    {
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;
        public int? Gender { get; set; }
        public string AccountName { get; set; } = null!;
        public bool IsVerified { get; set; }
        public int Role { get; set; }
        public int Star { get; set; }
        public bool? IsOnline { get; set; }
        public string? Fullname { get; set; }
        public string? Yob { get; set; }
        public string? IdentityCardNumber { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
