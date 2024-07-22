using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Service.ReponseModels
{
    public class SongSingerViewModel
    {
        public Guid? SongId { get; set; }
        public Guid? SingerId { get; set; }
        public string? SongName { get; set; }
        public string? SingerName { get;set; }
    }
}
