using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Service.Filters
{
    public class PostCommentFilter
    {
        public Guid? CommentId { get; set; }
        public string? Comment { get; set; }
        public int? CommentType { get; set; }
        public int? Status { get; set; }
        public Guid? ParentCommentId { get; set; }
        public Guid? MemberId { get; set; }
        public Guid? PostId { get; set; }
        public DateTime? UploadTime { get; set; }
    }
}
