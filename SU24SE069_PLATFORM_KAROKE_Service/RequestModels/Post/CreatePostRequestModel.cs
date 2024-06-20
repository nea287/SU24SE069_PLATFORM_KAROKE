using SU24SE069_PLATFORM_KAROKE_DataAccess.Models;
using SU24SE069_PLATFORM_KAROKE_Service.RequestModels.PostRate;
using SU24SE069_PLATFORM_KAROKE_Service.RequestModels.PostShare;
using SU24SE069_PLATFORM_KAROKE_Service.RequestModels.Report;
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
        public Guid RecordingId { get; set; }
        public ICollection<CreatePostShareRequestModel>? PostShares { get; set; }
        public ICollection<CreatePostRateRequestModel>? PostRates { get; set; }
        public ICollection<CreateReportRequestModel>? Reports { get; set; }
    }
}
