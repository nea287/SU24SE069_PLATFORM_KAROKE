using SU24SE069_PLATFORM_KAROKE_DataAccess.Models;
using SU24SE069_PLATFORM_KAROKE_Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Repository.IRepository
{
    public interface INotificationRepository : IBaseRepository<Notification>
    {
        public Task<bool> CreateNotification(Notification notification);
        public Task<bool> UpdateNotification(Notification notification);
        public IQueryable<Notification> GetAllNotificationsByAccountId(Guid accountId);
    }
}
