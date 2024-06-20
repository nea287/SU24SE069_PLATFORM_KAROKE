using SU24SE069_PLATFORM_KAROKE_DataAccess.Models;
using SU24SE069_PLATFORM_KAROKE_Service.RequestModels.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Service.RequestModels.PostRate
{
    public class CreatePostRateRequestModel
    {
        public Guid MemberId { get; set; }
        public Guid PostId { get; set; }
        public int VoteType { get; set; }
        public int Category { get; set; }
        public string Comment { get; set; } = null!;
        public ICollection<CreateReportRequestModel>? Reports { get; set; }
    }
}
