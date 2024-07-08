using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Service.ReponseModels
{
    public class DashboardResponse<TEntity>
    {
        public string? Message { get; set; }
        public Dictionary<TEntity, decimal>? Values { get; set; }
    }
}
