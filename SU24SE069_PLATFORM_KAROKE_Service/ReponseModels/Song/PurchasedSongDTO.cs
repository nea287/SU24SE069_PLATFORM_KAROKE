namespace SU24SE069_PLATFORM_KAROKE_Service.ReponseModels
{
    public class PurchasedSongDTO
    {
        public Guid? PurchasedSongId { get; set; }
        public Guid? SongId { get; set; }
        public DateTime? PurchaseDate { get; set; }
        public Guid? MemberId { get; set; }
        public string? SongName { get; set; }
        public string? SongUrl { get; set; }
        public decimal? Price { get; set; }

        public ICollection<string>? Genres { get; set; }
        public ICollection<string>? Singers { get; set; }
        public ICollection<string>? Artists { get; set; }
        public bool IsFavorite { get; set; } = false;
    }
}
