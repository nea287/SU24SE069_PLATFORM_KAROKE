using SU24SE069_PLATFORM_KAROKE_BusinessLayer.Commons;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Service.RequestModels.Song
{
    public class UpdateSongRequestModel
    {
        //[Required(ErrorMessage = Constraints.EMPTY_INPUT_INFORMATION)]
        public string? SongName { get; set; }
        public string? SongDescription { get; set; }
        public string? SongUrl { get; set; }
        public string? SongCode { get; set; }
        public DateTime? PublicDate { get; set; }
        //[Required(ErrorMessage = Constraints.EMPTY_INPUT_INFORMATION)]
        public Guid? CreatorId { get; set; }
        [RegularExpression(Constraints.VALIDATE_AMOUNT, ErrorMessage = Constraints.STAR_INVALID)]
        public decimal? Price { get; set; }
        //[Required(ErrorMessage = Constraints.EMPTY_INPUT_INFORMATION)]
        //public string Category { get; set; } 
        //[Required(ErrorMessage = Constraints.EMPTY_INPUT_INFORMATION)]
        //public string Author { get; set; } = null!;
        //[Required(ErrorMessage = Constraints.EMPTY_INPUT_INFORMATION)]
        //public string Singer { get; set; } = null!;

        //[Required(ErrorMessage = Constraints.EMPTY_INPUT_INFORMATION)]
        public ICollection<SongGenreRequestModel>? SongGenres { get; set; } 
        //[Required(ErrorMessage = Constraints.EMPTY_INPUT_INFORMATION)]
        public ICollection<SongArtistRequestModel>? SongArtists { get; set; } 
        //[Required(ErrorMessage = Constraints.EMPTY_INPUT_INFORMATION)]
        public ICollection<SongSingerRequestModel>? SongSingers { get; set; }
    }
}
