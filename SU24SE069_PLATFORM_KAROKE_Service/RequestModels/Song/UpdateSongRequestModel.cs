using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Service.RequestModels.Song
{
    public class UpdateSongRequestModel
    {
        public string SongName { get; set; } = null!;
        public string? SongDescription { get; set; }
        public string? SongUrl { get; set; }
        public int? Source { get; set; }
        public string? SongCode { get; set; }
        public DateTime? PublicDate { get; set; }
        public int SongType { get; set; }
        public double? Tempo { get; set; }
        public Guid CreatorId { get; set; }
        public decimal Price { get; set; }
    }
}
