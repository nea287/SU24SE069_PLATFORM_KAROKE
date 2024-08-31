using SU24SE069_PLATFORM_KAROKE_BusinessLayer.Commons;
using SU24SE069_PLATFORM_KAROKE_DataAccess.Models;
using Swashbuckle.AspNetCore.Annotations;
using System;
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
        public ICollection<string>? Genres { get; set; }
        public ICollection<string>? Singers { get; set; }
        public decimal? Price { get; set; }
        [SwaggerIgnore]
        public ICollection<string>? Artists { get; set; }
        public string? SongUrl { get; set; }
        public SongModel? Song { get; set; }
    }

    public class SongModel
    {
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
    }
}
