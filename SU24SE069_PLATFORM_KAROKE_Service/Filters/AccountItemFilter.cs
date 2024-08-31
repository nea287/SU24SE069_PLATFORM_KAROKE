using SU24SE069_PLATFORM_KAROKE_BusinessLayer.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Service.Filters
{
    public class AccountItemFilter
    {
        public Guid? AccountItemId { get; set; }
        public ItemStatus? ItemStatus { get; set; }
        public ItemType? ItemType { get; set; }
        public DateTime? ActivateDate { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public int? Quantity { get; set; }
        public Guid? ItemId { get; set; }
        public Guid? MemberId { get; set; }
        public int? ObtainMethod { get; set; }

    }
}
