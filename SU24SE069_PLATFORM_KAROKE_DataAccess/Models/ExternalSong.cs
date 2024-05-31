using System;
using System.Collections.Generic;

namespace SU24SE069_PLATFORM_KAROKE_DataAccess.Models
{
    public partial class ExternalSong
    {
        public Guid SongId { get; set; }
        public string SongName { get; set; } = null!;
        public string? SongDescription { get; set; }
        public string SongUrl { get; set; } = null!;
        public int Source { get; set; }
        public int SongStatus { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public Guid StaffId { get; set; }
    }
}
