using SU24SE069_PLATFORM_KAROKE_BusinessLayer.Commons;
using SU24SE069_PLATFORM_KAROKE_Service.Commons;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Service.RequestModels.Account
{
    public class CreateAccount1RequestModel
    {
        [Required(ErrorMessage = Constraints.EMPTY_INPUT_INFORMATION)]
        public string UserName { get; set; } = null!;
        [Required(ErrorMessage = Constraints.EMPTY_INPUT_INFORMATION)]
        public string Password { get; set; } = null!;
        [EmailAddress]
        public string Email { get; set; } = null!;
        public AccountGender? Gender { get; set; }
        //public AccountRole Role { get; set; }
        //[RegularExpression(Constraints.VALIDATE_AMOUNT, ErrorMessage = Constraints.STAR_INVALID)]
        //public decimal Star { get; set; }
        public string? Fullname { get; set; }
        [InRangeOneHundredAttribute(ErrorMessage = Constraints.INFORMATION_INVALID)]
        public int? Yob { get; set; }
        //public AccountStatus AccountStatus { get; set; }
        public string? Description { get; set; }

    }
}
