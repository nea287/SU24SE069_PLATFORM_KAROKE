﻿using SU24SE069_PLATFORM_KAROKE_BusinessLayer.Commons;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Service.RequestModels.PostComment
{
    public class CreatePostCommentRequestModel
    {
        [Required(ErrorMessage = Constraints.EMPTY_INPUT_INFORMATION)]
        public string Comment { get; set; } = null!;
        public PostCommentType CommentType { get; set; } = PostCommentType.PARENT;
        //public int Status { get; set; }
        //public DateTime CreateTime {get;set;}
        public float? Score { get; set; }   
        public Guid? ParentCommentId { get; set; }
        [Required(ErrorMessage = Constraints.EMPTY_INPUT_INFORMATION)]
        public Guid MemberId { get; set; }
        [Required(ErrorMessage = Constraints.EMPTY_INPUT_INFORMATION)]
        public Guid PostId { get; set; }
    }
}
