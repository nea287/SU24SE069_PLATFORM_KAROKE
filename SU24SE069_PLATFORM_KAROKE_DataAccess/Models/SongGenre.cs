using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_DataAccess.Models
{
    public partial class SongGenre
    {
        public Guid SongId { get; set; }
        public Guid GenreId { get; set; }

        public virtual Song Song { get; set; } = null!;
        public virtual Genre Genre { get; set; } = null!;
    }
}
