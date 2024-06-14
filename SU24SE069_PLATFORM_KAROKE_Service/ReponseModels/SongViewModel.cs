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
        public int? SongStatus { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? SongCode { get; set; }
        public DateTime? PublicDate { get; set; }
        public Guid? CreatorId { get; set; }
        public decimal? Price { get; set; }
        public int? Category { get; set; }
        public string? Author { get; set; } 
        public string? Singer { get; set; }


        //public ICollection<FavouriteSong> FavouriteSongs { get; set; }
        //public ICollection<InAppTransaction> InAppTransactions { get; set; }
        //public ICollection<PurchasedSong> PurchasedSongs { get; set; }
        //public ICollection<Recording> Recordings { get; set; }

    }
}
