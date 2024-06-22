using SU24SE069_PLATFORM_KAROKE_DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Service.ReponseModels
{
    public class SingerViewModel
    {

        public Guid? SingerId { get; set; }
        public string? SingerName { get; set; } 

        public ICollection<SongViewModel>? Songs { get; set; }
    }
}
