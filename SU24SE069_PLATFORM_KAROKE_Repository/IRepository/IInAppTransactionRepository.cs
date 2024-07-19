using SU24SE069_PLATFORM_KAROKE_DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Repository.IRepository
{
    public interface IInAppTransactionRepository : IBaseRepository<InAppTransaction>
    {
        public Task<bool> CreateInAppTransaction(InAppTransaction request);
        //public bool DeleteAccountInventory(AccountInventoryItem request);
        public Task<bool> UpdateInAppTransaction(Guid id, InAppTransaction request);
        public bool CheckPurchasedSong(Guid memberId, Guid songId);
        public Task<bool> CheckPurchasedSong1(Guid memberId, Guid songId);
        public bool CheckPurchasedItem(Guid memberId, Guid itemId);
        public Task<bool> CheckPurchasedItem1(Guid memberId, Guid itemId);
    }
}
