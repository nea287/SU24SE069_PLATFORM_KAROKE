using System;
using System.Collections.Generic;

namespace SU24SE069_PLATFORM_KAROKE_DataAccess.Models
{
    public partial class AccountInventoryItem
    {
        public Guid AccountInventoryItemId { get; set; }
        public int ItemStatus { get; set; }
        public DateTime ActivateDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int Quantity { get; set; }
        public Guid ItemId { get; set; }
        public Guid MemberId { get; set; }
    }
}
