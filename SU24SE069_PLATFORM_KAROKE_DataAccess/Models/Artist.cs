using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_DataAccess.Models
{
    public partial class Artist
    {
        public Artist()
        {
            SongArtists = new HashSet<SongArtist>();
        }

        public Guid ArtistId { get; set; }
        public string ArtistName { get; set; } = null!;

        public virtual ICollection<SongArtist> SongArtists { get; set; }
    }
}
