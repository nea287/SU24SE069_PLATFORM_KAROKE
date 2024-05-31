using System;
using System.Collections.Generic;

namespace SU24SE069_PLATFORM_KAROKE_DataAccess.Models
{
    public partial class KaraokeRoom
    {
        public Guid RoomId { get; set; }
        public string RoomLog { get; set; } = null!;
        public DateTime CreateTime { get; set; }
        public Guid CreatorId { get; set; }
    }
}
