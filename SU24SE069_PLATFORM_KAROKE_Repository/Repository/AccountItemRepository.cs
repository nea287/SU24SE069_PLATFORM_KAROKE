using SU24SE069_PLATFORM_KAROKE_DataAccess.Models;
using SU24SE069_PLATFORM_KAROKE_Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Repository.Repository
{
    public class AccountItemRepository : BaseRepository<AccountItem>, IAccountItemRepository
    {
        public async Task<bool> CreateAccountInventory(AccountItem request)
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

        public async Task<bool> UpdateAccountInventory(Guid id, AccountItem request)
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
