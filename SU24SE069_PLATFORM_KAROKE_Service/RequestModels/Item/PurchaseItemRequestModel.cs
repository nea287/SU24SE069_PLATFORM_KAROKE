using SU24SE069_PLATFORM_KAROKE_BusinessLayer.Commons;
using System.ComponentModel.DataAnnotations;

namespace SU24SE069_PLATFORM_KAROKE_Service.RequestModels.Item
{
    public class PurchaseItemRequestModel
    {
        [Required(ErrorMessage = Constraints.EMPTY_INPUT_INFORMATION)]
        public Guid MemberId { get; set; }
        [Required(ErrorMessage = Constraints.EMPTY_INPUT_INFORMATION)]
        public Guid ItemId { get; set; }
    }
}
