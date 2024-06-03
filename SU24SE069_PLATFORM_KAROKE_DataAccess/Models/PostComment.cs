using System;
using System.Collections.Generic;

namespace SU24SE069_PLATFORM_KAROKE_DataAccess.Models
{
    public partial class PostComment
    {
        public Guid CommentId { get; set; }
        public string Comment { get; set; } = null!;
        public Guid MemberId { get; set; }
        public Guid PostId { get; set; }
    }
}
