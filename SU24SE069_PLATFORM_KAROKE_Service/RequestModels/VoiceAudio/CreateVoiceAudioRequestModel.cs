using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Service.RequestModels.VoiceAudio
{
    public class CreateVoiceAudioRequestModel
    {
        public string VoiceUrl { get; set; } = null!;
        public double DurationSecond { get; set; }
        //public DateTime UploadTime { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        //public int Pitch { get; set; }
        //public Guid RecordingId { get; set; }
        public Guid MemberId { get; set; }
    }
}
