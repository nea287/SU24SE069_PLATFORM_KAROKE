using SU24SE069_PLATFORM_KAROKE_BusinessLayer.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Service.ReponseModels
{
    public class ReportViewModel
    {
        public Guid? ReportId { get; set; }
        public Guid? ReporterId { get; set; }
        public Guid? ReportedAccountId { get; set; }
        public ReportCatagory? ReportCategory { get; set; }
        public ReportStatus? Status { get; set; }
        public string? Reason { get; set; }
        public DateTime? CreateTime { get; set; }
        public ReportType? ReportType { get; set; }
        public Guid? CommentId { get; set; }
        public Guid? PostId { get; set; }
        public Guid? RoomId { get; set; }
        public string? Comment {  get; set; }   
        public string? RoomLog { get; set; }
        public string? PostCaption { get; set; }   
    }
}
