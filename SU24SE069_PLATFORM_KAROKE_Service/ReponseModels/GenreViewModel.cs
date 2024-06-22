using SU24SE069_PLATFORM_KAROKE_DataAccess.Models;
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

        public ICollection<SongViewModel>? Songs { get; set; }
    }
}
