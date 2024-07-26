using System;
using System.Collections.Generic;

namespace SU24SE069_PLATFORM_KAROKE_DataAccess.Models
{
    public partial class PurchasedSong
    {
        public PurchasedSong()
        {
            Recordings = new HashSet<Recording>();
        }
        public Guid PurchasedSongId { get; set; }
        public DateTime PurchaseDate { get; set; }
        public Guid MemberId { get; set; }
        public Guid SongId { get; set; }
        public Guid? InAppTransactionId { get; set; }

        public virtual Account Member { get; set; } = null!;
        public virtual Song Song { get; set; } = null!;
        public virtual InAppTransaction? InAppTransaction { get; set; }
        public virtual ICollection<Recording> Recordings { get; set; }
    }
}
