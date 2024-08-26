using Microsoft.AspNetCore.Http;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.Commons;
using SU24SE069_PLATFORM_KAROKE_DataAccess.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Service.RequestModels.Song
{
    public class CreateSongRequestModel
    {
        [Required(ErrorMessage = Constraints.EMPTY_INPUT_INFORMATION)]
        public string SongName { get; set; } = null!;
        public string? SongDescription { get; set; }
        public string? SongUrl { get; set; }
        public DateTime? PublicDate { get; set; }
        [Required(ErrorMessage = Constraints.EMPTY_INPUT_INFORMATION)]
        public Guid CreatorId { get; set; }
        [RegularExpression(Constraints.VALIDATE_AMOUNT, ErrorMessage = Constraints.STAR_INVALID)]
        public decimal Price { get; set; }
        [Required(ErrorMessage = Constraints.EMPTY_INPUT_INFORMATION)]
        public ICollection<SongGenreRequestModel> SongGenres { get; set; } = new List<SongGenreRequestModel>();
        [Required(ErrorMessage = Constraints.EMPTY_INPUT_INFORMATION)]
        public ICollection<SongArtistRequestModel> SongArtists { get; set; } = new  List<SongArtistRequestModel>();
        [Required(ErrorMessage = Constraints.EMPTY_INPUT_INFORMATION)]
        public ICollection<SongSingerRequestModel> SongSingers { get; set; } = new List<SongSingerRequestModel>();
    }

    public class FileSong
    {
        public IFormFile file { get; set; } = null!;
    }
}
