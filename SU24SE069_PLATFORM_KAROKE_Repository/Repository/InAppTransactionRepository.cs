using SU24SE069_PLATFORM_KAROKE_DAO.DAO;
using SU24SE069_PLATFORM_KAROKE_DataAccess.Models;
using SU24SE069_PLATFORM_KAROKE_Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Repository.Repository
{
    public class InAppTransactionRepository : BaseRepository<InAppTransaction>, IInAppTransactionRepository
    {
        public bool CheckPurchasedItem(Guid memberId, Guid itemId)
        {
            return Any(x => x.MemberId == memberId && x.ItemId == itemId && x.Item.CanStack == false);
        }

        public async Task<bool> CheckPurchasedItem1(Guid memberId, Guid itemId)
        {
            var rs = await FirstOrDefaultAsync(x => x.MemberId == memberId && x.ItemId == itemId && x.Item.CanStack == false);

            return rs != null;


        }

        public bool CheckPurchasedSong(Guid memberId, Guid songId)
        {
            return Any(x => x.MemberId == memberId && x.SongId == songId);
        }

        public async Task<bool> CheckPurchasedSong1(Guid memberId, Guid songId)
        {
            var rs = await FirstOrDefaultAsync(x => x.MemberId == memberId && x.SongId == songId);

            return rs != null;
        }

        public async Task<bool> CreateInAppTransaction(InAppTransaction request)
        {
            try
            {
                await InsertAsync(request);
                SaveChages();
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> UpdateInAppTransaction(Guid id, InAppTransaction request)
        {
            try
            {
                await UpdateGuid(request, id);
                SaveChages();
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
    }
}
