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
            Songs = new HashSet<Song>();
        }

        public Guid ArtistId { get; set; }
        public string ArtistName { get; set; } = null!;

        public virtual ICollection<Song> Songs { get; set; }
    }
}
