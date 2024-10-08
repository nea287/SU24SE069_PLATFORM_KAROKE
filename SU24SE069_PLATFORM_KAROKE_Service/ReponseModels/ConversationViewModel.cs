﻿using SU24SE069_PLATFORM_KAROKE_BusinessLayer.Commons;
using SU24SE069_PLATFORM_KAROKE_DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Service.ReponseModels
{
    public class ConversationViewModel
    {
        //public ConversationViewModel()
        //{
        //    Messages = new HashSet<MessageViewModel>();
        //}

        public Guid? ConversationId { get; set; }
        public Guid? MemberId1 { get; set; }
        public Guid? MemberId2 { get; set; }
        public ConversationType? ConversationType { get; set; }
        public Guid? TicketId { get; set; }
        //public ICollection<MessageViewModel>? Messages { get; set; }
    }

    public class ConversationMemberViewModel
    {
        public Guid? ConversationId { get; set; }
        public Guid? MemberId1 { get; set; }
        public Guid? MemberId2 { get; set; }
        public ConversationType? ConversationType { get; set; }
        public Guid? TicketId { get; set; }
        ICollection<MessageViewModel>? Messages { get; set; }
    }

}
