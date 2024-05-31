using System;
using System.Collections.Generic;

namespace SU24SE069_PLATFORM_KAROKE_DataAccess.Models
{
    public partial class FavouriteSong
    {
        public Guid FavourteId { get; set; }
        public int SongType { get; set; }
        public Guid MemberId { get; set; }
        public Guid? InternalSongId { get; set; }
        public Guid? ExternalSongId { get; set; }
    }
}
