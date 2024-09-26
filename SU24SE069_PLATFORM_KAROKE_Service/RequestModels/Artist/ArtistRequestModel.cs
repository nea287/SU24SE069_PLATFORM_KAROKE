using SU24SE069_PLATFORM_KAROKE_BusinessLayer.Commons;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Service.RequestModels.Artist
{
    public class ArtistRequestModel
    {
        [Required(ErrorMessage = Constraints.EMPTY_INPUT_INFORMATION)]
        public string ArtistName { get; set; } = null!;
        public string? Image { get; set; }
        public ArtistStatus? ArtistStatus { get; set; }
    }
}
