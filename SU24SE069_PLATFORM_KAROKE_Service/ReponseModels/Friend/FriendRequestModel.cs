﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Service.ReponseModels.Friend
{
    public class FriendRequestModel
    {
        public Guid SenderId { get; set; }
        public Guid ReceiverId { get; set; }
    }
}
