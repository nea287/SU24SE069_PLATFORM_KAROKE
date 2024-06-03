using System;
using System.Collections.Generic;

namespace SU24SE069_PLATFORM_KAROKE_DataAccess.Models
{
    public partial class InAppTransaction
    {
        public Guid InAppTransactionId { get; set; }
        public decimal StarAmount { get; set; }
        public int Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public int TransactionType { get; set; }
        public Guid MemberId { get; set; }
        public Guid ItemId { get; set; }
        public int SongType { get; set; }
        public Guid SongId { get; set; }
    }
}
