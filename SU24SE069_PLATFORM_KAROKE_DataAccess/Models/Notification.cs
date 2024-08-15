using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_DataAccess.Models
{
    public partial class Notification
    {
        public int NotificationId { get; set; }
        public string Description { get; set; }
        public int NotificationType { get; set; }
        public int Status { get; set; }
        public DateTime CreateDate { get; set; }
        public Guid AccountId { get; set; }
        public Account Account { get; set; } = null!;
    }
}
