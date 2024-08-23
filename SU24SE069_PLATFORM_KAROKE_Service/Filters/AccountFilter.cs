using SU24SE069_PLATFORM_KAROKE_BusinessLayer.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Service.Filters
{
    public class AccountFilter
    {
        public Guid? AccountId { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public AccountGender? Gender { get; set; }
        public AccountRole? Role { get; set; }
        public decimal? UpBalance { get; set; }
        public bool? IsOnline { get; set; }
        public string? Fullname { get; set; }
        public int? Yob { get; set; }
        public string? IdentityCardNumber { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime? CreatedTime { get; set; }
        public Guid? CharacterItemId { get; set; }
        public Guid? RoomItemId { get; set; }
        public string? CharaterItemCode { get; set; }
        public string? RoomItemCode { get; set; }
        public string? Image { get; set; }

        public AccountStatus? AccountStatus { get; set; }
        public string? Description { get; set; }
    }
}
