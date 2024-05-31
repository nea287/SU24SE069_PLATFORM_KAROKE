using System;
using System.Collections.Generic;

namespace SU24SE069_PLATFORM_KAROKE_DataAccess.Models
{
    public partial class Account
    {
        public Guid AccountId { get; set; }
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;
        public int Gender { get; set; }
        public string AccountName { get; set; } = null!;
        public bool IsVerified { get; set; }
        public int Role { get; set; }
        public int Star { get; set; }
        public bool IsOnline { get; set; }
        public Guid CharacterId { get; set; }
        public string? Fullname { get; set; }
        public string? Yob { get; set; }
        public string? IdentityCardNumber { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
