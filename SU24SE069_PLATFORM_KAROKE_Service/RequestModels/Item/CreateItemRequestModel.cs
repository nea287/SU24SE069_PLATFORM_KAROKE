using SU24SE069_PLATFORM_KAROKE_BusinessLayer.Commons;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Service.RequestModels.Item
{
    public class CreateItemRequestModel
    {
        [Required(ErrorMessage = Constraints.EMPTY_INPUT_INFORMATION)]
        public string ItemCode { get; set; } = null!;
        [Required(ErrorMessage = Constraints.EMPTY_INPUT_INFORMATION)]
        public string ItemName { get; set; } = null!;
        [Required(ErrorMessage = Constraints.EMPTY_INPUT_INFORMATION)]
        public string ItemDescription { get; set; } = null!;
        public ItemType ItemType { get; set; }
        [RegularExpression(Constraints.VALIDATE_AMOUNT, ErrorMessage = Constraints.VALIDATE_AMOUNT)]
        public decimal ItemPrice { get; set; }
        //public int ItemStatus { get; set; }
        public bool CanExpire { get; set; }
        public bool CanStack { get; set; }
        //public DateTime CreatedDate { get; set; }
        public Guid? CreatorId { get; set; }
        public string PrefabCode { get; set; } = null!;
    }
}
