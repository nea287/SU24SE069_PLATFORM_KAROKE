using System;
using System.Collections.Generic;

namespace SU24SE069_PLATFORM_KAROKE_DataAccess.Models
{
    public partial class Post
    {
        public Guid PostId { get; set; }
        public string? Caption { get; set; }
        public DateTime UploadTime { get; set; }
        public DateTime UpdateTime { get; set; }
        public Guid MemberId { get; set; }
        public Guid RecordingId { get; set; }
    }
}
