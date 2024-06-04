using Microsoft.EntityFrameworkCore;
using SU24SE069_PLATFORM_KAROKE_DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Repository.IRepository
{
    public interface IItemRepository : IBaseRepository<Item>
    {
        //public Item GetItem(Guid id);
        public IEnumerable<Item> GetItems();
        public bool UpdateItem(Guid id,Item item);
        public bool CreateItem(Item item);
        public bool DeleteItem(Item item);
        public bool ExistedItem(string ItemCode);

    }
}
