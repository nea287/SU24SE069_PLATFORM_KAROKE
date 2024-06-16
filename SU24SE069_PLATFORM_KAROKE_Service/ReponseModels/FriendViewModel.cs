using SU24SE069_PLATFORM_KAROKE_BusinessLayer.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Service.ReponseModels
{
    public class FriendViewModel
    {
        public Guid? SenderId { get; set; }
        public Guid? ReceiverId { get; set; }
        public FriendStatus? Status { get; set; }
    }
}
