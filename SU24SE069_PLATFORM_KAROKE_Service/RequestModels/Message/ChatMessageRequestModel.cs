using SU24SE069_PLATFORM_KAROKE_BusinessLayer.Commons;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Service.RequestModels.Message
{
    public class ChatMessageRequestModel
    {
        [Required(ErrorMessage = Constraints.EMPTY_INPUT_INFORMATION)]
        public string Content { get; set; } = null!;
        //public DateTime TimeStamp { get; set; }
        //public Guid SenderId { get; set; }
        //public Guid ConversationId { get; set; }
    }
}
