using SU24SE069_PLATFORM_KAROKE_DataAccess.Models;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Service.ReponseModels
{
    public class ArtistViewModel
    {
        public Guid? ArtistId { get; set; }
        public string? ArtistName { get; set; }
        [SwaggerIgnore]
        public ICollection<SongArtistViewModel>? SongArtists { get; set; }
    }
}
