using SU24SE069_PLATFORM_KAROKE_BusinessLayer.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Service.Filters
{
    public class RecordingFilter
    {
        public Guid? RecordingId { get; set; }
        public string? RecordingName { get; set; }
        public RecordingType? RecordingType { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? Score { get; set; }
        public Guid? PurchasedSongId { get; set; }
        public Guid? HostId { get; set; }
        public Guid? OwnerId { get; set; }
        public Guid? KaraokeRoomId { get; set; }
        public float? StartTime { get; set; }
        public float? EndTime { get; set; }
        public float? Volume { get; set; }
    }
}
