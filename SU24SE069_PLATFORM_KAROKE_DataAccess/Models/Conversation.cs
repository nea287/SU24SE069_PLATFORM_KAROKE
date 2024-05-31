using System;
using System.Collections.Generic;

namespace SU24SE069_PLATFORM_KAROKE_DataAccess.Models
{
    public partial class Conversation
    {
        public Guid ConversationId { get; set; }
        public Guid MemberId1 { get; set; }
        public Guid MemberId2 { get; set; }
        public int ConversationType { get; set; }
        public Guid SupportRequestId { get; set; }
    }
}
