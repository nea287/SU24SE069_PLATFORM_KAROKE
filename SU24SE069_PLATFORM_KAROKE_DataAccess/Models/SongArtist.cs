using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_DataAccess.Models
{
    public partial class SongArtist
    {
        public Guid SongId { get; set; }
        public Guid ArtistId { get; set; }

        public virtual Song Song { get; set; } = null!;
        public virtual Artist Artist { get; set; } = null!;
    }
}
