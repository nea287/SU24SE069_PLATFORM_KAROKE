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
    public class InAppTransactionViewModel
    {
        public Guid? InAppTransactionId { get; set; }
        public InAppTransactionStatus? Status { get; set; }
        public DateTime? CreatedDate { get; set; }
        public InAppTransactionType? TransactionType { get; set; }
        public Guid? MemberId { get; set; }
        public string? UserName { get; set; }
        public Guid? ItemId { get; set; }
        public Guid? SongId { get; set; }
        public decimal? UpAmountBefore { get; set; }
        public Guid? MonetaryTransactionId { get; set; }
        public decimal? UpTotalAmount { get; set; }
        public ItemInAppViewModel? Item { get; set; }
        public SongInAppViewModel? Song { get; set; }
        public MonetaryTransactionViewModel? MonetaryTransaction { get; set; }

    }

    public class ItemInAppViewModel
    {
        public Guid? ItemId { get; set; }
        public string? ItemCode { get; set; }
        public string? ItemName { get; set; }
        public string? ItemDescription { get; set; }
        public ItemType? ItemType { get; set; }
        public ItemStatus? ItemStatus { get; set; }
        public bool? CanExpire { get; set; }
        public bool? CanStack { get; set; }
        public DateTime? CreatedDate { get; set; }
        public Guid? CreatorId { get; set; }
        public string? CreatorMail { get; set; }
        public string? PrefabCode { get; set; }

        public decimal? ItemBuyPrice { get; set; }
        public decimal? ItemSellPrice { get; set; }
    }

    public class SongInAppViewModel
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
        public ICollection<string>? Genre { get; set; }
        public ICollection<string>? Singer { get; set; }
        public ICollection<string>? Artist { get; set; }
    }
}
