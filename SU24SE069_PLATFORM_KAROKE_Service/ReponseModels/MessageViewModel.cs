using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Service.ReponseModels
{
    public class MessageViewModel
    {
        public Guid? MessageId { get; set; }
        public string? Content { get; set; }
        public DateTime? TimeStamp { get; set; }
        public Guid? SenderId { get; set; }
        public Guid? ConversationId { get; set; }
    }
}
