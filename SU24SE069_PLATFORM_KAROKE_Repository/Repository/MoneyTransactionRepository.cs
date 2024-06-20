using SU24SE069_PLATFORM_KAROKE_DataAccess.Models;
using SU24SE069_PLATFORM_KAROKE_Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Repository.Repository
{
    public class MoneyTransactionRepository : BaseRepository<MoneyTransaction>, IMoneyTransactionRepository
    {
        public async Task<bool> CreateMoneyTransaction(MoneyTransaction transaction)
        {
            try
            {
                await InsertAsync(transaction);
                await SaveChagesAsync();
            }catch(Exception ex)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> UpdateMoneyTransaction(MoneyTransaction transaction)
        {
            try
            {
                await Update(transaction);
                await SaveChagesAsync();

            }catch(Exception ex)
            {
                return false;
            }

            return true;
        }
    }
}
