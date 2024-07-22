using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Service.ReponseModels
{
    public class SongArtistViewModel
    {
        public Guid? SongId { get; set; }
        public Guid? ArtistId { get; set; }
        public string? SongName { get; set; }
        public string? ArtistName { get;set; }
    }
}
