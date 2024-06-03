using System;
using System.Collections.Generic;

namespace SU24SE069_PLATFORM_KAROKE_DataAccess.Models
{
    public partial class Item
    {
        public Guid ItemId { get; set; }
        public string ItemCode { get; set; } = null!;
        public string ItemName { get; set; } = null!;
        public string ItemDescription { get; set; } = null!;
        public int ItemType { get; set; }
        public decimal ItemPrice { get; set; }
        public int ItemStatus { get; set; }
        public int CanExpire { get; set; }
        public int CanStack { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid CreatorId { get; set; }
    }
}
