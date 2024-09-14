using Microsoft.EntityFrameworkCore;
using SU24SE069_PLATFORM_KAROKE_DataAccess.Models;
using SU24SE069_PLATFORM_KAROKE_Repository.IRepository;

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

        public async Task<List<Notification>> GetUserUnreadNotification(Guid accountId)
        {
            // READ = 0, UNREAD = 1, DELETE = 2
            return await GetDbSet().Where(n => n.AccountId == accountId && n.Status == 1)
                .OrderByDescending(n => n.CreateDate)
                .ToListAsync();
        }
    }
}
