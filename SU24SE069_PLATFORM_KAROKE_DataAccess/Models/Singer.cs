using System;
using System.Collections.Generic;

namespace SU24SE069_PLATFORM_KAROKE_DataAccess.Models
{
    public partial class Singer
    {
        public Singer()
        {
            SongSingers = new HashSet<SongSinger>();
        }

        public string SingerName { get; set; } = null!;
        public Guid SingerId { get; set; }
        public string? Image { get; set; }
        public int? Status { get; set; }
        public virtual ICollection<SongSinger> SongSingers { get; set; }
    }
}
