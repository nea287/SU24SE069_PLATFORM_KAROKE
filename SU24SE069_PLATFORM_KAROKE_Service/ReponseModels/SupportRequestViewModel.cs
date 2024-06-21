using SU24SE069_PLATFORM_KAROKE_BusinessLayer.Commons;
using SU24SE069_PLATFORM_KAROKE_DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Service.ReponseModels
{
    public class SupportRequestViewModel
    {
        //public SupportRequestViewModel()
        //{
        //    Conversations = new HashSet<Conversation>();
        //}

        public Guid? RequestId { get; set; }
        public string? Problem { get; set; }
        public DateTime? CreateTime { get; set; }
        public SupportRequestCategory? Category { get; set; }
        public SupportRequestStatus? Status { get; set; }
        public Guid? SenderId { get; set; }
        //public ICollection<Conversation>? Conversations { get; set; }
    }
}
