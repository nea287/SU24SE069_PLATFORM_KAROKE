﻿using SU24SE069_PLATFORM_KAROKE_BusinessLayer.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Service.Filters
{
    public class InAppTransactionFilter
    {
        public Guid? InAppTransactionId { get; set; }
        public InAppTransactionStatus? Status { get; set; }
        public DateTime? CreatedDate { get; set; }
        public InAppTransactionType? TransactionType { get; set; }
        public Guid? MemberId { get; set; }
        public string? UserName { get; set; }
        public Guid? ItemId { get; set; }
        public Guid? SongId { get; set; }
        public decimal? UpAmountBefore { get; set; }
        public Guid? MonetaryTransactionId { get; set; }
        public decimal? UpTotalAmount { get; set; }
    }
}
