using System;
using System.Collections.Generic;

namespace SU24SE069_PLATFORM_KAROKE_DataAccess.Models
{
    public partial class PostRate
    {
        public string? Comment { get; set; }
        public Guid MemberId { get; set; }
        public Guid PostId { get; set; }
        public int? Score { get; set; }
        public Guid RateId { get; set; }
        public int Category { get; set; }

        public virtual Account Member { get; set; } = null!;
        public virtual Post Post { get; set; } = null!;
    }
}
