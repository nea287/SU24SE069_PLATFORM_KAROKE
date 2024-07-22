using SU24SE069_PLATFORM_KAROKE_DataAccess.Models;
using Swashbuckle.AspNetCore.Annotations;
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
        [SwaggerIgnore]
        public ICollection<RecordingViewModel>? Recordings { get; set; }

    }
}
