using Microsoft.EntityFrameworkCore;
using SU24SE069_PLATFORM_KAROKE_DataAccess.Models;
using SU24SE069_PLATFORM_KAROKE_Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Repository.Repository
{
    public class ItemRepository : BaseRepository<Item>, IItemRepository
    {
        public bool CreateItem(Item item)
        {
            throw new NotImplementedException();
        }

        public bool DeleteItem(Guid id)
        {
            throw new NotImplementedException();
        }

        public Item GetItem(Guid id)
        {
            Item item = new Item();
            try
            {
                item = GetFirstOrDefault(x => x.ItemId == id);
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return item;
        }

        public IEnumerable<Item> GetItems()
        {
            IEnumerable<Item> items = new List<Item>();
            try
            {
                items = GetWhere(x => true).Result;
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return items;
        }

        public bool UpdateItem(Guid id, Item item)
        {
            try
            {
                _ = UpdateGuid(item, id);
                SaveChages();

            }catch(Exception ex)
            {
                return false;
            }

            return true;
        }
    }
}
