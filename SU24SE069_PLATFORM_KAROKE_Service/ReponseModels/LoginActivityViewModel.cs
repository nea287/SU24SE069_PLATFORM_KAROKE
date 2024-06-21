using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Service.ReponseModels
{
    public class LoginActivityViewModel
    {
        public Guid? LoginId { get; set; }
        public DateTime? LoginTime { get; set; }
        public string? LoginDevice { get; set; }
        public Guid? MemberId { get; set; }
    }
}
