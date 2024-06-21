using SU24SE069_PLATFORM_KAROKE_BusinessLayer.Commons;
using SU24SE069_PLATFORM_KAROKE_DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Service.ReponseModels
{
    public class AccountInventoryItemViewModel
    {
        //public AccountInventoryItemViewModel()
        //{
        //    AccountCharacterItems = new HashSet<Account>();
        //    AccountRoomItems = new HashSet<Account>();
        //}

        public Guid? AccountInventoryItemId { get; set; }
        public ItemStatus? ItemStatus { get; set; }
        public DateTime? ActivateDate { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public int? Quantity { get; set; }
        public Guid? ItemId { get; set; }
        public Guid? MemberId { get; set; }
        //public ICollection<Account>? AccountCharacterItems { get; set; }
        //public ICollection<Account>? AccountRoomItems { get; set; }
    }
}
