﻿using SU24SE069_PLATFORM_KAROKE_BusinessLayer.Commons;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.ReponseModels;
using SU24SE069_PLATFORM_KAROKE_DataAccess.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace SU24SE069_PLATFORM_KAROKE_Service.ReponseModels
{
    public class PostCommentViewModel
    {

        public Guid? CommentId { get; set; }
        public string? Comment { get; set; }
        public int? CommentType { get; set; }
        public int? Status { get; set; }
        public Guid? ParentCommentId { get; set; }
        public Guid? MemberId { get; set; }
        public Guid? PostId { get; set; }
        public DateTime? UploadTime { get; set; }
        [SwaggerIgnore]
        public ICollection<PostCommentViewModel>? InverseParentComment { get; set; }
        public AccountModel? Member { get; set; }

    }
    public class AccountModel
    {
        public Guid? AccountId { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public AccountGender? Gender { get; set; }
        public AccountRole? Role { get; set; }
        public decimal? UpBalance { get; set; }
        public bool? IsOnline { get; set; }
        public string? Fullname { get; set; }
        public int? Yob { get; set; }
        public string? IdentityCardNumber { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime? CreatedTime { get; set; }
        public Guid? CharacterItemId { get; set; }
        public Guid? RoomItemId { get; set; }
        public string? CharaterItemCode { get; set; }
        public string? RoomItemCode { get; set; }
        public string? Image { get; set; }

        public AccountStatus? AccountStatus { get; set; }
        public string? Description { get; set; }
    }

}
