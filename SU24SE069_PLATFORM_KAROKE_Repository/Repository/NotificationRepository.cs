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
    public class NotificationRepository : BaseRepository<Notification>, INotificationRepository
    {
        public async Task<bool> CreateNotification(Notification notification)
        {
            try
            {
                await InsertAsync(notification);
                await SaveChagesAsync();
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;    
        }

        public IQueryable<Notification> GetAllNotificationsByAccountId(Guid accountId)
        {
            IQueryable<Notification> result;
            try
            {
                result = GetAll(x => x.AccountId == accountId);
            }catch(Exception ex)
            {
                return null;
            }

            return result;
        }

        public async Task<bool> UpdateNotification(Notification notification)
        {
            try
            {
                await Update(notification);
                await SaveChagesAsync();
            }catch (Exception ex)
            {
                return false;
            }

            return true;
        }
    }
}
