using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Service.ReponseModels
{
    public class VoiceAudioViewModel
    {
        public Guid? VoiceId { get; set; }
        public string? VoiceUrl { get; set; }
        public double? DurationSecond { get; set; }
        public DateTime? UploadTime { get; set; }
        public float? StartTime { get; set; }
        public float? EndTime { get; set; }
        public int? Pitch { get; set; }
        public Guid? RecordingId { get; set; }
        public Guid? MemberId { get; set; }
        public float? Volume { get; set; }
    }
}
