using SU24SE069_PLATFORM_KAROKE_BusinessLayer.Commons;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Service.RequestModels.Package
{
    public class PackageRequestModel
    {
        [Required(ErrorMessage = Constraints.EMPTY_INPUT_INFORMATION)]
        public string PackageName { get; set; } = null!;

        [Required(ErrorMessage = Constraints.EMPTY_INPUT_INFORMATION)]
        public string Description { get; set; } = null!;

        [Required(ErrorMessage = Constraints.EMPTY_INPUT_INFORMATION)]
        [RegularExpression(Constraints.VALIDATE_AMOUNT,ErrorMessage = Constraints.INFORMATION_INVALID)]
        public decimal MoneyAmount { get; set; }

        [RegularExpression(Constraints.VALIDATE_NEGATIVE, ErrorMessage = Constraints.INFORMATION_INVALID)]
        public int StarNumber { get; set; }

        //public int Status { get; set; }
        //public DateTime CreatedDate { get; set; }
        [Required(ErrorMessage = Constraints.EMPTY_INPUT_INFORMATION)]
        public Guid CreatorId { get; set; }
    }
}
