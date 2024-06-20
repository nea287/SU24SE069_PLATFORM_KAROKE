using SU24SE069_PLATFORM_KAROKE_BusinessLayer.Commons;
using SU24SE069_PLATFORM_KAROKE_Service.Commons;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_BusinessLayer.RequestModels.Account
{
    public class UpdateAccountByMailRequestModel
    {
        public string Password { get; set; } = null!;
        public AccountGender Gender { get; set; }
        //public string AccountName { get; set; } = null!;
        [RegularExpression(Constraints.VALIDATE_AMOUNT, ErrorMessage = Constraints.STAR_INVALID)]
        public decimal Star { get; set; }
        public string? Fullname { get; set; }
        [InRangeOneHundredAttribute(ErrorMessage = Constraints.INFORMATION_INVALID)]
        public int? Yob { get; set; }
        [RegularExpression(Constraints.VALIDATE_NEGATIVE, ErrorMessage = Constraints.INFORMATION_INVALID)]
        public string? IdentityCardNumber { get; set; }
        [RegularExpression(Constraints.VALIDATE_NEGATIVE, ErrorMessage = Constraints.INFORMATION_INVALID)]
        public string? PhoneNumber { get; set; }
    }
}
