using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Service.ReponseModels
{
    public class FavouriteSongViewModel
    {
        public Guid? MemberId { get; set; }
        public Guid? SongId { get; set; }
        public ICollection<string>? Singers { get; set; }
        public ICollection<string>? Artists { get; set; }
        public ICollection<string>? Genres { get; set; }    
        public string? SongName { get; set; }
    }
}
