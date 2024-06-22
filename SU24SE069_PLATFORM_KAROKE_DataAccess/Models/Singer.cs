using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_DataAccess.Models
{
    public partial class Singer
    {
        public Singer()
        {
            SongSingers = new HashSet<SongSinger>();
        }

        public Guid SingerId { get; set; }
        public string SingerName { get; set; } = null!;

        public virtual ICollection<SongSinger> SongSingers { get; set; }
    }
 }
