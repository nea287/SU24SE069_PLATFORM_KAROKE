using SU24SE069_PLATFORM_KAROKE_DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Repository.IRepository
{
    public interface IAccountItemRepository : IBaseRepository<AccountItem>
    {
        public Task<bool> CreateAccountInventory(AccountItem request);
        //public bool DeleteAccountInventory(AccountInventoryItem request);
        public Task<bool> UpdateAccountInventory(Guid id, AccountItem request);
    }
}
