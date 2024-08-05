using SU24SE069_PLATFORM_KAROKE_BusinessLayer.Commons;
using SU24SE069_PLATFORM_KAROKE_Service.Commons;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Service.RequestModels.VoiceAudio
{
    public class CreateVoiceAudioRequestModel
    {
        [Required(ErrorMessage = Constraints.EMPTY_INPUT_INFORMATION)]
        public string VoiceUrl { get; set; } = null!;
        [Required(ErrorMessage = Constraints.EMPTY_INPUT_INFORMATION)]
        public double DurationSecond { get; set; }
        //public DateTime UploadTime { get; set; }
        public float StartTime { get; set; }
        public float Volume { get; set; } = 1.0f;
        //[GreaterThanOrEqualDate(nameof(StartTime), ErrorMessage = Constraints.VALIDATE_ENDDATE)]
        public float EndTime { get; set; }
        //public int Pitch { get; set; }
        //public Guid RecordingId { get; set; }
        [Required(ErrorMessage = Constraints.EMPTY_INPUT_INFORMATION)]
        public Guid MemberId { get; set; }
    }
}
