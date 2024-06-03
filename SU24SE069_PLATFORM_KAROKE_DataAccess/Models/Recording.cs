using System;
using System.Collections.Generic;

namespace SU24SE069_PLATFORM_KAROKE_DataAccess.Models
{
    public partial class Recording
    {
        public Guid RecordingId { get; set; }
        public string RecordingName { get; set; } = null!;
        public int RecordingType { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int Score { get; set; }
        public int SongType { get; set; }
        public Guid SongId { get; set; }
        public Guid HostId { get; set; }
        public Guid OwnerId { get; set; }
        public Guid KaraokeRoomId { get; set; }
    }
}
