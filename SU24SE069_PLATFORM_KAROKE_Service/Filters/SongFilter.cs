using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.Commons;
using SU24SE069_PLATFORM_KAROKE_Service.ReponseModels;
using Swashbuckle.AspNetCore.Annotations;

namespace SU24SE069_PLATFORM_KAROKE_Service.Filters
{
    public class SongFilter
    {
        public Guid? SongId { get; set; }
        public string? SongName { get; set; }
        public string? SongDescription { get; set; }
        public string? SongUrl { get; set; }
        public SongStatus? SongStatus { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? SongCode { get; set; }
        public DateTime? PublicDate { get; set; }
        public Guid? CreatorId { get; set; }
        public decimal? Price { get; set; }
        public string? GenreName { get; set; }
        public string? SingerName { get; set; }
        public string? ArtistName { get; set; }
        //[SwaggerIgnore]
        //public ICollection<string>? Genre { get; set; }
        //[SwaggerIgnore]
        //public ICollection<string>? Singer { get; set; }
        //[SwaggerIgnore]
        //public ICollection<string>? Artist { get; set; }
        [SwaggerIgnore]
        public ICollection<SongArtistViewModel>? SongArtists { get; set; }
        [SwaggerIgnore]
        public ICollection<SongGenreViewModel>? SongGenres { get; set; }
        [SwaggerIgnore]
        public ICollection<SongSingerViewModel>? SongSingers { get; set; }
    }
}
