using Microsoft.EntityFrameworkCore;
using SU24SE069_PLATFORM_KAROKE_DataAccess.Models;
using SU24SE069_PLATFORM_KAROKE_Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Repository.Repository
{
    public class MonetaryTransactionRepository : BaseRepository<MonetaryTransaction>, IMonetaryTransactionRepository
    {
        public async Task<bool> CreateMoneyTransaction(MonetaryTransaction transaction)
        {
            try
            {
                await InsertAsync(transaction);
                await SaveChagesAsync();
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        public async Task<MonetaryTransaction?> FindTransactionByPaymentCode(string paymentCode)
        {
            return await GetDbSet().FirstOrDefaultAsync(t => t.PaymentCode == paymentCode);
        }

        public async Task<bool> UpdateMoneyTransaction(MonetaryTransaction transaction)
        {
            try
            {
                await Update(transaction);
                await SaveChagesAsync();

            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        public async Task<List<MonetaryTransaction>> GetTransactionsByStatus(int transactionStatus)
        {
            return await GetDbSet().Where(t => t.Status == transactionStatus).ToListAsync();
        }
    }
}
