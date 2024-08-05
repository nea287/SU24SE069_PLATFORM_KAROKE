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
        [Required(ErrorMessage = Constraints.EMPTY_INPUT_INFORMATION)]
        public string UserName { get; set; } = null!;
        [Required(ErrorMessage = Constraints.EMPTY_INPUT_INFORMATION)]
        public string Password { get; set; } = null!;
        [EmailAddress]
        public string Email { get; set; } = null!; 
        public AccountGender? Gender { get; set; }
        ///public bool IsVerified { get; set; }
        public AccountRole Role { get; set; }
        [RegularExpression(Constraints.VALIDATE_AMOUNT, ErrorMessage = Constraints.STAR_INVALID)]
        public decimal UpBalance { get; set; }
        //public bool? IsOnline { get; set; } = false;3
        public string? Fullname { get; set; }
        [InRangeOneHundredAttribute(ErrorMessage = Constraints.INFORMATION_INVALID)]
        public int? Yob { get; set; }
        [RegularExpression(Constraints.VALIDATE_NEGATIVE, ErrorMessage =  Constraints.INFORMATION_INVALID)]
        public string? IdentityCardNumber { get; set; }
        [RegularExpression(Constraints.VALIDATE_NEGATIVE, ErrorMessage = Constraints.INFORMATION_INVALID)]
        public string? PhoneNumber { get; set; }
        public Guid? CharacterItemId { get; set; }
        public Guid? RoomItemId { get; set; }
        public AccountStatus AccountStatus { get; set; }
        public string? Description { get; set; }

    }
}
