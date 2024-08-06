using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SU24SE069_PLATFORM_KAROKE_DataAccess.Models
{
    public partial class AccountItem
    {
        public AccountItem()
        {
            AccountCharacterItems = new HashSet<Account>();
            AccountRoomItems = new HashSet<Account>();
        }

        public Guid AccountItemId { get; set; }
        public int ItemStatus { get; set; }
        public DateTime ActivateDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int Quantity { get; set; }
        public Guid ItemId { get; set; }
        public Guid MemberId { get; set; }
        public int ObtainMethod { get; set; }
        public Guid? InAppTransactionId { get; set; }

        public virtual Item Item { get; set; } = null!;
        //[JsonIgnore]
        public virtual Account Member { get; set; } = null!;
        public virtual InAppTransaction? InAppTransaction { get; set; }
        //[JsonIgnore]
        public virtual ICollection<Account> AccountCharacterItems { get; set; }
        //[JsonIgnore]
        public virtual ICollection<Account> AccountRoomItems { get; set; }
    }
}
