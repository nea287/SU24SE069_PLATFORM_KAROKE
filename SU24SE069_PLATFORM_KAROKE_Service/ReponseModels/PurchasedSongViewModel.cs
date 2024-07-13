﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Service.ReponseModels
{
    public class PurchasedSongViewModel
    {
        public Guid? PurchasedSongId { get; set; }
        public DateTime? PurchaseDate { get; set; }
        public Guid? MemberId { get; set; }
        public Guid? SongId { get; set; }
        public string? SongName { get; set; }
        public string? Genres { get; set; }
        public string? Singers { get; set; }
        public decimal? Price { get; set; }
        public string? Artists { get; set; }
    }
}
