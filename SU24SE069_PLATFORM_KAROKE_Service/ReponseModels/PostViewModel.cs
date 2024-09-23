using SU24SE069_PLATFORM_KAROKE_BusinessLayer.Commons;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.ReponseModels;
using SU24SE069_PLATFORM_KAROKE_DataAccess.Models;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Service.ReponseModels
{

    public class PostViewModel
    {
        public Guid? PostId { get; set; }
        public string? Caption { get; set; }
        public DateTime? UploadTime { get; set; }
        public DateTime? UpdateTime { get; set; }
        public Guid? MemberId { get; set; }
        public Guid? RecordingId { get; set; }
        public string? SongUrl { get; set; }
        public PostType? PostType { get; set; }
        public PostStatus? PostStatus { get; set; }
        public float? Score { get; set; }
        public AccountPostViewModel? Member { get; set; }
        public RecordingPostViewModel? Recording { get; set; }
        //[SwaggerIgnore]
        public  ICollection<PostViewModel>? InverseOriginPost { get; set; }
        //[SwaggerIgnore]
        public  ICollection<PostRatingViewModel>? PostRatings { get; set; }
    }

    public class AccountPostViewModel
    {
        public Guid? AccountId { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public AccountGender? Gender { get; set; }
        public string? AccountName { get; set; } 
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

    public class RecordingPostViewModel
    {
        public Guid? RecordingId { get; set; }
        public string? RecordingName { get; set; }
        public RecordingType? RecordingType { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? Score { get; set; }
        public Guid? PurchasedSongId { get; set; }
        public Guid? HostId { get; set; }
        public Guid? OwnerId { get; set; }
        public Guid? KaraokeRoomId { get; set; }
        public float? StartTime { get; set; }
        public float? EndTime { get; set; }
        public float? Volume { get; set; }
        //public ICollection<Post> Posts { get; set; }
        public string? SongUrl { get; set; }
        public ICollection<VoiceAudioViewModel>? VoiceAudios { get; set; }
    }


    public class PostAdminViewModel
    {
        public Guid? PostId { get; set; }
        public string? Caption { get; set; }
        public DateTime? UploadTime { get; set; }
        public DateTime? UpdateTime { get; set; }
        public Guid? MemberId { get; set; }
        public Guid? RecordingId { get; set; }
        public string? SongUrl { get; set; }
        public PostType? PostType { get; set; }
        public PostStatus? PostStatus { get; set; }
        public float? Score { get; set; }

    }
}
