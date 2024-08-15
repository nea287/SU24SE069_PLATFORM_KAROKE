using SU24SE069_PLATFORM_KAROKE_BusinessLayer.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Service.RequestModels.Notification
{
    public class CreateNotificationRequestModel
    {
        public string Description { get; set; } = null!;
        public NotificationType NotificationType { get; set; }
        //public NotificationStatus? Status { get; set; }
        //public DateTime? CreateDate { get; set; }
        public Guid AccountId { get; set; }
    }
}
