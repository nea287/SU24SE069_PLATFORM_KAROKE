using System;
using System.Collections.Generic;

namespace SU24SE069_PLATFORM_KAROKE_DataAccess.Models
{
    public partial class Song
    {
        public Guid SongId { get; set; }
        public string SongName { get; set; } = null!;
        public string? SongDescription { get; set; }
        public string? SongUrl { get; set; }
        public int? Source { get; set; }
        public int SongStatus { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string? SongCode { get; set; }
        public DateTime? PublicDate { get; set; }
        public int Category { get; set; }
        public double? Tempo { get; set; }
        public Guid CreatorId { get; set; }
        public decimal Price { get; set; }
    }
}
