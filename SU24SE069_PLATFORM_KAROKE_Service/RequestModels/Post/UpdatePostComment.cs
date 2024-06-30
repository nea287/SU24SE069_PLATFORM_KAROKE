using SU24SE069_PLATFORM_KAROKE_BusinessLayer.Commons;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Service.RequestModels.Post
{
    public class UpdatePostComment
    {
        [Required(ErrorMessage = Constraints.EMPTY_INPUT_INFORMATION)]
        public string Comment { get; set; } = null!;
        //public PostCommentType CommentType { get; set; } = PostCommentType.PARENT;
        //public int Status { get; set; }
        //public Guid? ParentCommentId { get; set; }
        //public Guid MemberId { get; set; }
        //public Guid PostId { get; set; }
        //public DateTime CreateTime{get;set;}
    }
}
