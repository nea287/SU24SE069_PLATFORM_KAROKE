using System;
using System.Collections.Generic;

namespace SU24SE069_PLATFORM_KAROKE_DataAccess.Models
{
    public partial class InternalSong
    {
        public Guid SongId { get; set; }
        public string SongCode { get; set; } = null!;
        public string SongName { get; set; } = null!;
        public string? SongDescription { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime PublicDate { get; set; }
        public int Category { get; set; }
        public double Tempo { get; set; }
        public int Status { get; set; }
        public Guid CreatorId { get; set; }
    }
}
