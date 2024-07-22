using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Service.ReponseModels
{
    public class FavouriteSongViewModel
    {
        public Guid? MemberId { get; set; }
        public Guid? SongId { get; set; }
        [SwaggerIgnore]
        public ICollection<string>? Singers { get; set; }
        [SwaggerIgnore]
        public ICollection<string>? Artists { get; set; }
        [SwaggerIgnore]
        public ICollection<string>? Genres { get; set; }    
        public string? SongName { get; set; }
        [JsonIgnore]
        public string? ArtistName { get; set; }
        [JsonIgnore]
        public string? GenreName { get; set; }
        [JsonIgnore]
        public string? SingerName { get; set; }
    }
}
