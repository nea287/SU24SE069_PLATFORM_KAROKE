﻿using SU24SE069_PLATFORM_KAROKE_DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Service.ReponseModels
{
    public class ItemViewModel
    {
        //public ItemViewModel()
        //{
        //    AccountInventoryItems = new HashSet<AccountInventoryItem>();
        //    InAppTransactions = new HashSet<InAppTransaction>();
        //}

        public Guid? ItemId { get; set; }
        public string? ItemCode { get; set; }
        public string? ItemName { get; set; }
        public string? ItemDescription { get; set; }
        public int? ItemType { get; set; }
        public decimal? ItemPrice { get; set; }
        public int? ItemStatus { get; set; }
        public bool? CanExpire { get; set; }
        public bool? CanStack { get; set; }
        public DateTime? CreatedDate { get; set; }
        public Guid? CreatorId { get; set; }

        //public ICollection<AccountInventoryItem>? AccountInventoryItems { get; set; }
        //public ICollection<InAppTransaction>? InAppTransactions { get; set; }
    }
}
