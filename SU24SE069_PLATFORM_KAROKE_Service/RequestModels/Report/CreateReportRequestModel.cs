using SU24SE069_PLATFORM_KAROKE_DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Service.RequestModels.Report
{
    public class CreateReportRequestModel
    {
        public Guid ReporterId { get; set; }
        public Guid ReportedAccountId { get; set; }
        public int ReportCategory { get; set; }
        public int Status { get; set; }
        public string? Reason { get; set; }
        //public DateTime CreateTime { get; set; }
        public int ReportType { get; set; }
        public Guid CommentId { get; set; }
        public Guid PostId { get; set; }
        public Guid RoomId { get; set; }       
    }
}
