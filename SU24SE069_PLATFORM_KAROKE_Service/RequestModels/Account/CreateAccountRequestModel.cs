using SU24SE069_PLATFORM_KAROKE_BusinessLayer.Commons;
using SU24SE069_PLATFORM_KAROKE_DataAccess.Models;
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
        public AccountGender? Gender { get; set; }
        ///public bool IsVerified { get; set; }
        public AccountRole Role { get; set; }
        public int Star { get; set; }
        //public bool? IsOnline { get; set; } = false;
        public string? Fullname { get; set; }
        public int? Yob { get; set; }
        public string? IdentityCardNumber { get; set; }
        public string? PhoneNumber { get; set; }
        public Guid? CharacterItemId { get; set; }
        public Guid? RoomItemId { get; set; }
        public AccountStatus AccountStatus { get; set; }
    }
}
