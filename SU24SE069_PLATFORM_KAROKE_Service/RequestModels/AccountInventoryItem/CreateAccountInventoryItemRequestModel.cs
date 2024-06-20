using SU24SE069_PLATFORM_KAROKE_BusinessLayer.Commons;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Service.RequestModels.AccountInventoryItem
{
    public class CreateAccountInventoryItemRequestModel
    {
        public ItemStatus ItemStatus { get; set; }
        // DateTime ActivateDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        [RegularExpression(Constraints.VALIDATE_NEGATIVE, ErrorMessage = Constraints.INFORMATION_INVALID)]
        public int Quantity { get; set; }
        [Required(ErrorMessage = Constraints.EMPTY_INPUT_INFORMATION)]
        public Guid ItemId { get; set; }
        [Required(ErrorMessage = Constraints.EMPTY_INPUT_INFORMATION)]
        public Guid MemberId { get; set; }
    }
}
