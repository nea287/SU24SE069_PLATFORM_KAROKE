using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_DataAccess.Models
{
    public partial class Genre
    {
        public Genre()
        {
            SongGenres = new HashSet<SongGenre>();
        }

        public Guid GenreId { get; set; }
        public string GenreName { get; set; } = null!;

        public virtual ICollection<SongGenre> SongGenres { get; set; }
    }
}
