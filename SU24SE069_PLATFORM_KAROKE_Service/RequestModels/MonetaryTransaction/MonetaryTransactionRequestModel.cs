using SU24SE069_PLATFORM_KAROKE_BusinessLayer.Commons;
using SU24SE069_PLATFORM_KAROKE_Service.RequestModels.Package;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Service.RequestModels.MoneyTransaction
{
    public class MonetaryTransactionRequestModel
    {
        public PaymentType PaymentType { get; set; }
        [Required(ErrorMessage = Constraints.EMPTY_INPUT_INFORMATION)]
        public string PaymentCode { get; set; } = null!;
        //[RegularExpression(Constraints.VALIDATE_AMOUNT, ErrorMessage = Constraints.INFORMATION_INVALID)]
        //public decimal MoneyAmount { get; set; }
        [Required(ErrorMessage = Constraints.EMPTY_INPUT_INFORMATION)]
        public string Currency { get; set; } = null!;
        //public PaymentStatus Status { get; set; }
        //public DateTime CreatedDate { get; set; }
        [Required(ErrorMessage = Constraints.EMPTY_INPUT_INFORMATION)]
        public Guid PackageId { get; set; }
        [Required(ErrorMessage = Constraints.EMPTY_INPUT_INFORMATION)]
        public Guid MemberId { get; set; }

    }
}
