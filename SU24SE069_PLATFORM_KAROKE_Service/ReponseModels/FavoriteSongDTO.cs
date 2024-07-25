using Swashbuckle.AspNetCore.Annotations;

namespace SU24SE069_PLATFORM_KAROKE_Service.ReponseModels
{
    public class FavoriteSongDTO
    {
        public Guid? MemberId { get; set; }
        public Guid? SongId { get; set; }
        [SwaggerIgnore]
        public ICollection<string>? Singers { get; set; }
        [SwaggerIgnore]
        public ICollection<string>? Artists { get; set; }
        [SwaggerIgnore]
        public ICollection<string>? Genres { get; set; }
        public string? SongName { get; set; }
        public string? ArtistName { get; set; }
        public string? GenreName { get; set; }
        public string? SingerName { get; set; }
        public string? SongUrl { get; set; }
        public decimal? Price { get; set; }
        public bool IsPurchased { get; set; } = false;
    }
}
