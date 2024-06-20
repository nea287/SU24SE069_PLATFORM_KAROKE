using SU24SE069_PLATFORM_KAROKE_BusinessLayer.Commons;
using SU24SE069_PLATFORM_KAROKE_DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Service.ReponseModels
{
    public class PackageViewModel
    {
        //public PackageViewModel()
        //{
        //    MoneyTransactions = new HashSet<MoneyTransaction>();
        //}

        public Guid? PackageId { get; set; }
        public string? PackageName { get; set; }
        public string? Description { get; set; }
        public decimal? MoneyAmount { get; set; }
        public int? StarNumber { get; set; }
        public PackageStatus? Status { get; set; }
        public DateTime? CreatedDate { get; set; }
        public Guid? CreatorId { get; set; }

        //public virtual ICollection<MoneyTransaction> MoneyTransactions { get; set; }
    }
}
