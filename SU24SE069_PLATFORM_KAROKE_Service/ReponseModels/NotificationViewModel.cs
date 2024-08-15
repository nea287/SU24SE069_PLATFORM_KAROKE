using SU24SE069_PLATFORM_KAROKE_BusinessLayer.Commons;
using SU24SE069_PLATFORM_KAROKE_DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Service.ReponseModels
{
    public class NotificationViewModel
    {
        public int? NotificationId { get; set; }
        public string? Description { get; set; }
        public NotificationType? NotificationType { get; set; }
        public NotificationStatus? Status { get; set; }
        public DateTime? CreateDate { get; set; }
        public Guid? AccountId { get; set; }
        //public string? AccountEmail { get; set; }    
    }
}
