using SU24SE069_PLATFORM_KAROKE_DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Repository.IRepository
{
    public interface IMonetaryTransactionRepository : IBaseRepository<MonetaryTransaction>
    {
        public Task<bool> CreateMoneyTransaction(MonetaryTransaction transaction);
        public Task<bool> UpdateMoneyTransaction(MonetaryTransaction transaction);
        Task<MonetaryTransaction?> FindTransactionByPaymentCode(string paymentCode);
    }
}
