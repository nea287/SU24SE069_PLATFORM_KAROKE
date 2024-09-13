using SU24SE069_PLATFORM_KAROKE_BusinessLayer.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Service.Filters
{
    public class ConversationFilter
    {
        public Guid? ConversationId { get; set; }
        public Guid? MemberId1 { get; set; }
        public Guid? MemberId2 { get; set; }
        public ConversationType? ConversationType { get; set; }
        public Guid? TicketId { get; set; }
        //public ICollection<MessageViewModel>? Messages { get; set; }
    }
}
