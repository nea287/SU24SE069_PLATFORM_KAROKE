using SU24SE069_PLATFORM_KAROKE_DataAccess.Models;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Service.ReponseModels
{
    public class GenreViewModel
    {
        public Guid? GenreId { get; set; }
        public string? GenreName { get; set; }
        public string? Image { get; set; }
        [SwaggerIgnore]
        public ICollection<SongGenreViewModel>? SongGenres { get; set; }
    }
}
