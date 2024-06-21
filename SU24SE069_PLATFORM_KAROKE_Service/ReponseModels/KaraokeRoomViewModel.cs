using SU24SE069_PLATFORM_KAROKE_DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Service.ReponseModels
{
    public class KaraokeRoomViewModel
    {
        public Guid? RoomId { get; set; }
        public string? RoomLog { get; set; }
        public DateTime? CreateTime { get; set; }
        public Guid? CreatorId { get; set; }
        public ICollection<RecordingViewModel>? Recordings { get; set; }

    }
}
