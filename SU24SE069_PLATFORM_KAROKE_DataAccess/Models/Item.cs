﻿using System;
using System.Collections.Generic;

namespace SU24SE069_PLATFORM_KAROKE_DataAccess.Models
{
    public partial class Item
    {
        public Item()
        {
            AccountItems = new HashSet<AccountItem>();
            InAppTransactions = new HashSet<InAppTransaction>();
        }

        public Guid ItemId { get; set; }
        public string ItemCode { get; set; } = null!;
        public string ItemName { get; set; } = null!;
        public string ItemDescription { get; set; } = null!;
        public int ItemType { get; set; }
        public int ItemStatus { get; set; }
        public bool? CanExpire { get; set; }
        public bool? CanStack { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid? CreatorId { get; set; }
        public string? PrefabCode { get; set; }

        public decimal ItemBuyPrice { get; set; }
        public decimal ItemSellPrice { get; set; }



        public virtual Account? Creator { get; set; }
        public virtual ICollection<InAppTransaction> InAppTransactions { get; set; }
        public virtual ICollection<AccountItem> AccountItems { get; set; }

    }
}
