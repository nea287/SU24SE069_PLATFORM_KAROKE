using SU24SE069_PLATFORM_KAROKE_BusinessLayer.Commons;
using SU24SE069_PLATFORM_KAROKE_DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Service.ReponseModels
{
    public class RecordingViewModel
    {
        //public RecordingViewModel()
        //{
        //    Posts = new HashSet<Post>();
        //    VoiceAudios = new HashSet<VoiceAudio>();
        //}

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
        //public ICollection<Post> Posts { get; set; }
        public ICollection<VoiceAudioViewModel>? VoiceAudios { get; set; }
    }
}
