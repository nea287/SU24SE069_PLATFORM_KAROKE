using SU24SE069_PLATFORM_KAROKE_BusinessLayer.Commons;
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
        [SwaggerIgnore]
        public  ICollection<PostViewModel>? InverseOriginPost { get; set; }
        [SwaggerIgnore]
        public  ICollection<PostRatingViewModel>? PostRatings { get; set; }
        //[SwaggerIgnore]
        //public  ICollection<PostShareViewModel>? PostShares { get; set; }
        //[SwaggerIgnore]
        //public  ICollection<ReportViewModel>? Reports { get; set; }
    }
}
