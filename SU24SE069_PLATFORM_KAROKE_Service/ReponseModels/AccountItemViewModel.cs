using SU24SE069_PLATFORM_KAROKE_BusinessLayer.Commons;
using SU24SE069_PLATFORM_KAROKE_DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Service.ReponseModels
{
    public class AccountItemViewModel
    {
        //public AccountInventoryItemViewModel()
        //{
        //    AccountCharacterItems = new HashSet<Account>();
        //    AccountRoomItems = new HashSet<Account>();
        //}

        public Guid? AccountItemId { get; set; }
        public ItemStatus? ItemStatus { get; set; }
        public ItemType? ItemType { get; set; }

        //public ItemType? ItemType { get; set; }

        public DateTime? ActivateDate { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public int? Quantity { get; set; }
        public Guid? ItemId { get; set; }
        public Guid? MemberId { get; set; }
        public int? ObtainMethod { get; set; }
 

        public ItemModel? Item { get; set; }
        //public ICollection<Account>? AccountCharacterItems { get; set; }
        //public ICollection<Account>? AccountRoomItems { get; set; }
    }

    public class ItemModel
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
}
