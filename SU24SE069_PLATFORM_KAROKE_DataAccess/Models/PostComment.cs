using System;
using System.Collections.Generic;

namespace SU24SE069_PLATFORM_KAROKE_DataAccess.Models
{
    public partial class PostComment
    {
        public PostComment()
        {
            InverseParentComment = new HashSet<PostComment>();
        }

        public Guid CommentId { get; set; } 
        public string Comment { get; set; } = null!;
        public int CommentType { get; set; }
        public int Status { get; set; }
        public Guid? ParentCommentId { get; set; }
        public Guid MemberId { get; set; }
        public Guid PostId { get; set; }
        public DateTime CreateTime { get; set; }
        public virtual Post CommentNavigation { get; set; } = null!;
        public virtual Account Member { get; set; } = null!;
        public virtual PostComment? ParentComment { get; set; }
        public virtual ICollection<PostComment> InverseParentComment { get; set; }
    }
}
