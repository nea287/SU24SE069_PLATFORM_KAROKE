using SU24SE069_PLATFORM_KAROKE_BusinessLayer.Commons;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Service.RequestModels.PurchasedSong
{
    public class PurchasedSongRequestModel
    {
        //public DateTime PurchaseDate { get; set; }
        [Required(ErrorMessage = Constraints.EMPTY_INPUT_INFORMATION)]
        public Guid MemberId { get; set; }
        [Required(ErrorMessage = Constraints.EMPTY_INPUT_INFORMATION)]
        public Guid SongId { get; set; }
        //[Required(ErrorMessage = Constraints.EMPTY_INPUT_INFORMATION)]
        //public int Quantity { get; set; }
    }
}
