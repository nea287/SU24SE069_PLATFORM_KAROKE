﻿using System;
using System.Collections.Generic;

namespace SU24SE069_PLATFORM_KAROKE_DataAccess.Models
{
    public partial class PurchasedSong
    {
        public Guid PurchasedSongId { get; set; }
        public DateTime PurchaseDate { get; set; }
        public int SongType { get; set; }
        public Guid MemberId { get; set; }
        public Guid SongId { get; set; }
    }
}
