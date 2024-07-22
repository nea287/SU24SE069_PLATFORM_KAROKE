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
        public ICollection<PostComment>? InverseParentComment { get; set; }
    }
}
