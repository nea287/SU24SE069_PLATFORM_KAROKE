using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_DataAccess.Models
{
    public partial class FavouriteSong
    {
        //public int SongType { get; set; }
        public Guid MemberId { get; set; }
        public Guid SongId { get; set; }

        public virtual Account Member { get; set; } = null!;
        public virtual Song Song { get; set; } = null!;
    }
}
