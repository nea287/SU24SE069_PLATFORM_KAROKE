using SU24SE069_PLATFORM_KAROKE_DataAccess.Models;
using SU24SE069_PLATFORM_KAROKE_Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Repository.Repository
{
    public class AccountInventoryItemRepository : BaseRepository<AccountInventoryItem>, IAccountInventoryItemRepository
    {
        public async Task<bool> CreateAccountInventory(AccountInventoryItem request)
        {
            try
            {
                await InsertAsync(request);
                SaveChages();
            }catch(Exception ex)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> UpdateAccountInventory(Guid id, AccountInventoryItem request)
        {
            try
            {
                await UpdateGuid(request, id);
                SaveChages();
            }catch(Exception ex)
            {
                return false;
            }
            return true;
        }
    }
}
