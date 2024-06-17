using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Service.RequestModels.Post
{
    public class CreatePostRequestModel
    {
        public string? Caption { get; set; }
        //public DateTime UploadTime { get; set; }
        //public DateTime UpdateTime { get; set; }
        public Guid MemberId { get; set; }
        //public Guid RecordingId { get; set; }

        //public virtual ICollection<PostShare> PostShares { get; set; }
        //public virtual ICollection<PostRate> PostRates { get; set; }
        //public virtual ICollection<Report> Reports { get; set; }
    }
}
