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
        public DateTime? CreateTime { get; set; }
        [SwaggerIgnore]
        public ICollection<PostCommentViewModel>? InverseParentComment { get; set; }
    }
}
