using System;
using System.Collections.Generic;

namespace SU24SE069_PLATFORM_KAROKE_DataAccess.Models
{
    public partial class Genre
    {
        public Genre()
        {
            SongGenres = new HashSet<SongGenre>();
        }

        public string GenreName { get; set; } = null!;
        public Guid GenreId { get; set; }

        public virtual ICollection<SongGenre> SongGenres { get; set; }
    }
}
