using SU24SE069_PLATFORM_KAROKE_BusinessLayer.Commons;

namespace SU24SE069_PLATFORM_KAROKE_Service.Filters.Song
{
    public class KaraokeSongFilter
    {
        public string? SongName { get; set; }
        public SongStatus? SongStatus { get; set; }
        public string? SongCode { get; set; }
        public string? GenreName { get; set; }
        public string? SingerName { get; set; }
        public string? ArtistName { get; set; }
    }
}
