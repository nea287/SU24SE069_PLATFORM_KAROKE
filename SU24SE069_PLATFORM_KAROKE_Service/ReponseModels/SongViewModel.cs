using SU24SE069_PLATFORM_KAROKE_BusinessLayer.Commons;
using SU24SE069_PLATFORM_KAROKE_DataAccess.Models;
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
        public ICollection<string>? Genre { get; set; }
        public ICollection<string>? Singer { get; set; }
        public ICollection<string>? Artist { get; set; }


        public ICollection<FavouriteSongViewModel>? FavouriteSongs { get; set; }
        public ICollection<InAppTransactionViewModel>? InAppTransactions { get; set; }
        public ICollection<PurchasedSongViewModel>? PurchasedSongs { get; set; }
        public ICollection<RecordingViewModel>? Recordings { get; set; }
        public ICollection<SongArtistViewModel>? SongArtists { get; set; }
        public ICollection<SongGenreViewModel>? SongGenres { get; set; }
        public ICollection<SongSingerViewModel>? SongSingers { get; set; }

    }
}
