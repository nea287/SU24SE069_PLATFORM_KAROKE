using SU24SE069_PLATFORM_KAROKE_BusinessLayer.Commons;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Service.RequestModels.Conversation
{
    public class ConversationRequestModel
    {
        [Required(ErrorMessage = Constraints.EMPTY_INPUT_INFORMATION)]
        public Guid MemberId1 { get; set; }
        [Required(ErrorMessage = Constraints.EMPTY_INPUT_INFORMATION)]
        public Guid MemberId2 { get; set; }
        public ConversationType ConversationType { get; set; } = ConversationType.DEFAULT;
        [Required(ErrorMessage = Constraints.EMPTY_INPUT_INFORMATION)]
        public Guid SupportRequestId { get; set; }
    }
}
