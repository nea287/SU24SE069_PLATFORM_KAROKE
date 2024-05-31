using System;
using System.Collections.Generic;

namespace SU24SE069_PLATFORM_KAROKE_DataAccess.Models
{
    public partial class ReportParticipant
    {
        public Guid ReporterId { get; set; }
        public Guid ReportedAccountId { get; set; }
        public Guid RoomId { get; set; }
        public string? Reason { get; set; }
        public DateTime CreateTime { get; set; }
        public int Category { get; set; }
        public int Status { get; set; }
    }
}
