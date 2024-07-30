using SU24SE069_PLATFORM_KAROKE_BusinessLayer.Commons;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Service.RequestModels.VoiceAudio
{
    public class UpdateVoiceAudioRequestModel
    {
        public Guid VoiceId { get; set; }
        public double DurationSecond { get; set; }
        //public DateTime UploadTime { get; set; }
        public float StartTime { get; set; }
        //[GreaterThanOrEqualDate(nameof(StartTime), ErrorMessage = Constraints.VALIDATE_ENDDATE)]
        public float EndTime { get; set; }
        //public int Pitch { get; set; }
        //public Guid RecordingId { get; set; }
    }
}
