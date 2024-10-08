﻿using SU24SE069_PLATFORM_KAROKE_BusinessLayer.Commons;
using SU24SE069_PLATFORM_KAROKE_DataAccess.Models;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Service.ReponseModels
{
    public class SongViewModel
    {
        //public SongViewModel()
        //{
        //    FavouriteSongs = new HashSet<FavouriteSong>();
        //    InAppTransactions = new HashSet<InAppTransaction>();
        //    PurchasedSongs = new HashSet<PurchasedSong>();
        //    Recordings = new HashSet<Recording>();
        //}


        public Guid? SongId { get; set; }
        public string? SongName { get; set; }
        public string? SongDescription { get; set; }
        public string? SongUrl { get; set; }
        public SongStatus? SongStatus { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? SongCode { get; set; }
        public DateTime? PublicDate { get; set; }
        public Guid? CreatorId { get; set; }
        public decimal? Price { get; set; }
        [SwaggerIgnore]
        public ICollection<string>? Genre { get; set; }
        [SwaggerIgnore]
        public ICollection<string>? Singer { get; set; }
        [SwaggerIgnore]
        public ICollection<string>? Artist { get; set; }
        [SwaggerIgnore]

        //public ICollection<FavouriteSongViewModel>? FavouriteSongs { get; set; }
        //[SwaggerIgnore]
        //public ICollection<InAppTransactionViewModel>? InAppTransactions { get; set; }
        //[SwaggerIgnore] 
        //public ICollection<PurchasedSongViewModel>? PurchasedSongs { get; set; }
        public ICollection<SongArtistViewModel>? SongArtists { get; set; }
        [SwaggerIgnore]
        public ICollection<SongGenreViewModel>? SongGenres { get; set; } 
        [SwaggerIgnore]
        public ICollection<SongSingerViewModel>? SongSingers { get; set; }


    }
    public class SongModel1
    {
        public Guid? SongId { get; set; }
        public string? SongName { get; set; } 
        public string? SongDescription { get; set; }
        public string? SongUrl { get; set; }
        public int? SongStatus { get; set; } //
        public DateTime? CreatedDate { get; set; } //
        public DateTime? UpdatedDate { get; set; } //
        public string? SongCode { get; set; }
        public DateTime? PublicDate { get; set; }
        public Guid? CreatorId { get; set; }
        public decimal? Price { get; set; }

        public  ICollection<InAppTransaction>? InAppTransactions { get; set; } //
        public  ICollection<PurchasedSong>? PurchasedSongs { get; set; } //
                                                                               // public  ICollection<Recording> Recordings { get; set; }

        public  ICollection<SongArtist>? SongArtists { get; set; }
        public  ICollection<SongGenre>? SongGenres { get; set; }
        public  ICollection<FavouriteSong>? FavouriteSongs { get; set; } //
        public  ICollection<SongSinger>? SongSingers { get; set; }
    }
}
