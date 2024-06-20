using SU24SE069_PLATFORM_KAROKE_BusinessLayer.Commons;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Service.RequestModels.KaraokeRoom
{
    public class KaraokeRoomRequestModel
    {
        [Required(ErrorMessage = Constraints.EMPTY_INPUT_INFORMATION)]
        public string RoomLog { get; set; } = null!;
        //public DateTime CreateTime { get; set; }
        [Required(ErrorMessage = Constraints.EMPTY_INPUT_INFORMATION)]
        public Guid CreatorId { get; set; }
    }
}
