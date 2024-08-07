using SU24SE069_PLATFORM_KAROKE_BusinessLayer.Commons;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Service.RequestModels.Singer
{
    public class SingerRequestModel
    {
        [Required(ErrorMessage = Constraints.EMPTY_INPUT_INFORMATION)]
        public string SingerName { get; set; } = null!;
        public string? Image { get; set; }
    }
}
