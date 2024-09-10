using SU24SE069_PLATFORM_KAROKE_BusinessLayer.Commons;
using SU24SE069_PLATFORM_KAROKE_DataAccess.Models;
using SU24SE069_PLATFORM_KAROKE_Service.ReponseModels;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Service.Filters
{
    public class ItemFilter
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
        [JsonIgnore]
        public Guid? BuyerId { get; set; }
        public decimal? ItemBuyPrice { get; set; }
        public decimal? ItemSellPrice { get; set; }
        [SwaggerIgnore]

        public ICollection<InAppTransactionViewModel>? InAppTransactions { get; set; }
        [SwaggerIgnore]
        public ICollection<AccountItemViewModel>? AccountItems { get; set; }
    }
}
