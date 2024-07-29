using System;
using System.Collections.Generic;

namespace SU24SE069_PLATFORM_KAROKE_DataAccess.Models
{
    public partial class InAppTransaction
    {
        public InAppTransaction()
        {
            PurchasedSongs = new HashSet<PurchasedSong>();
            AccountItems = new HashSet<AccountItem>();  
        }
        public Guid InAppTransactionId { get; set; }
        public int Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public int TransactionType { get; set; }
        public Guid MemberId { get; set; }
        public Guid? ItemId { get; set; }
        public Guid? SongId { get; set; }
        public decimal UpAmountBefore { get; set; }
        public Guid? MonetaryTransactionId { get; set; }
        public decimal UpTotalAmount { get; set; }

        public virtual Item? Item { get; set; }
        public virtual Account Member { get; set; } = null!;
        public virtual Song? Song { get; set; }
        public virtual MonetaryTransaction? MonetaryTransaction { get; set; }
        public virtual ICollection<PurchasedSong> PurchasedSongs { get; set; }
        public virtual ICollection<AccountItem> AccountItems { get; set; }

    }
}
