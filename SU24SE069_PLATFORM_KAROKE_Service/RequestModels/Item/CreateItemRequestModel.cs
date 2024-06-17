using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Service.RequestModels.Item
{
    public class CreateItemRequestModel
    {
        public string ItemCode { get; set; } = null!;
        public string ItemName { get; set; } = null!;
        public string ItemDescription { get; set; } = null!;
        public int ItemType { get; set; }
        public decimal ItemPrice { get; set; }
        //public int ItemStatus { get; set; }
        public bool CanExpire { get; set; }
        public bool CanStack { get; set; }
        //public DateTime CreatedDate { get; set; }
        public Guid? CreatorId { get; set; }
        public string PrefabCode { get; set; } = null!;
    }
}
