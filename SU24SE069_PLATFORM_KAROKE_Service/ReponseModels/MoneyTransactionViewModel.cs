using SU24SE069_PLATFORM_KAROKE_BusinessLayer.Commons;
using SU24SE069_PLATFORM_KAROKE_DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Service.ReponseModels
{
    public class MoneyTransactionViewModel
    {
        public Guid? MoneyTransactionId { get; set; }
        public PaymentType? PaymentType { get; set; }
        public string? PaymentCode { get; set; }
        public decimal? MoneyAmount { get; set; }
        public string? Currency { get; set; }
        public PaymentStatus? Status { get; set; }
        public DateTime? CreatedDate { get; set; }
        public Guid? PackageId { get; set; }
        public Guid? MemberId { get; set; }
    }
}
