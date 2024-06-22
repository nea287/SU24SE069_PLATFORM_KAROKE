using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_DataAccess.Models
{
    public partial class SongSinger
    {
        public Guid SongId { get; set; }
        public Guid SingerId { get; set; }

        public virtual Song Song { get; set; } = null!;
        public virtual Singer Singer { get; set; } = null!;
    }
}
