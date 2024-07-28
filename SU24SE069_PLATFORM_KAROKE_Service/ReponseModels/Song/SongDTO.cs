using SU24SE069_PLATFORM_KAROKE_BusinessLayer.Commons;

namespace SU24SE069_PLATFORM_KAROKE_Service.ReponseModels
{
    public class SongDTO
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
        public List<string>? Genre { get; set; }
        public List<string>? Singer { get; set; }
        public List<string>? Artist { get; set; }
        public bool isPurchased { get; set; } = false;
        public bool isFavorite { get; set; } = false;
    }
}
