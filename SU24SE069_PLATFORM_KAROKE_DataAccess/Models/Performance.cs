using System;
using System.Collections.Generic;

namespace SU24SE069_PLATFORM_KAROKE_DataAccess.Models
{
    public partial class Performance
    {
        public Guid PerformanceId { get; set; }
        public string PerformanceName { get; set; } = null!;
        public int PerformanceType { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int Score { get; set; }
        public int SongType { get; set; }
        public Guid? InternalSongId { get; set; }
        public Guid? ExternalSongId { get; set; }
        public Guid HostId { get; set; }
        public Guid OwnerId { get; set; }
        public Guid KaraokeRoomId { get; set; }
    }
}
