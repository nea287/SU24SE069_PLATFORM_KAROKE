﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Service.ReponseModels
{
    public class SongGenreViewModel
    {
        public Guid? SongId { get; set; }
        public Guid? GenreId { get; set; }
        public string? SongName { get; set; }
        public string? GenreName { get;set; }
        public string? Image { get; set; }
    }
}
