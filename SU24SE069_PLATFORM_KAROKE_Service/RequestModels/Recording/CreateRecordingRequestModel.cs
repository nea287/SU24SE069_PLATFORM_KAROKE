using SU24SE069_PLATFORM_KAROKE_DataAccess.Models;
using SU24SE069_PLATFORM_KAROKE_Service.RequestModels.Post;
using SU24SE069_PLATFORM_KAROKE_Service.RequestModels.VoiceAudio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Service.RequestModels.Recording
{
    public class CreateRecordingRequestModel
    {
        public string RecordingName { get; set; } = null!;
        public int RecordingType { get; set; }
        //public DateTime CreatedDate { get; set; }
        //public DateTime UpdatedDate { get; set; }
        public int Score { get; set; }
        public Guid SongId { get; set; }
        public Guid HostId { get; set; }
        public Guid OwnerId { get; set; }
        public Guid KaraokeRoomId { get; set; }

        public ICollection<CreatePostRequestModel>? Posts { get; set; }
        public ICollection<CreateVoiceAudioRequestModel>? VoiceAudios { get; set; }

    }
}
