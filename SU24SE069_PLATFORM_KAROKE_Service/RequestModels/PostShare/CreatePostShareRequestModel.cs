using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Service.RequestModels.PostShare
{
    public class CreatePostShareRequestModel
    {
        public string? Caption { get; set; }
        //public DateTime ShareTime { get; set; }
        //public DateTime UpdateTime { get; set; }
        public Guid MemberId { get; set; }
        public Guid PostId { get; set; }
    }
}
