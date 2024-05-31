using System;
using System.Collections.Generic;

namespace SU24SE069_PLATFORM_KAROKE_DataAccess.Models
{
    public partial class VoiceRecording
    {
        public Guid RecordingId { get; set; }
        public string RecordingUrl { get; set; } = null!;
        public double DurationSecond { get; set; }
        public DateTime UploadTime { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int Pitch { get; set; }
        public Guid PerformanceId { get; set; }
        public Guid MemberId { get; set; }
    }
}
