using SU24SE069_PLATFORM_KAROKE_BusinessLayer.Commons;
using SU24SE069_PLATFORM_KAROKE_DataAccess.Models;
using SU24SE069_PLATFORM_KAROKE_Service.Commons;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_BusinessLayer.RequestModels.Account
{
    public class CreateAccountRequestModel
    {
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
        [EmailAddress]
        public string Email { get; set; } = null!; 
        public AccountGender? Gender { get; set; }
        ///public bool IsVerified { get; set; }
        public AccountRole Role { get; set; }
        [RegularExpression(@"^\d+(\.\d+)?$", ErrorMessage = "Vui lòng nhập số sao hợp lệ!")]
        public decimal Star { get; set; }
        //public bool? IsOnline { get; set; } = false;
        public string? Fullname { get; set; }
        [InRangeOneHundredAttribute(ErrorMessage = "Over the course of 100 years")]
        public int? Yob { get; set; }
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Vui lòng nhập thông tin hợp lệ!")]
        public string? IdentityCardNumber { get; set; }
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Vui lòng nhập thông tin hợp lệ!")]
        public string? PhoneNumber { get; set; }
        public Guid? CharacterItemId { get; set; }
        public Guid? RoomItemId { get; set; }
        public AccountStatus AccountStatus { get; set; }
    }
}
