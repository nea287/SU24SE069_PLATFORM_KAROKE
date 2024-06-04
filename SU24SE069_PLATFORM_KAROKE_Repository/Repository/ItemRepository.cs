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
            try
            {
                Insert(item);
                SaveChages();
            }catch(Exception ex)
            {
                return false;
            }
            return true;
        }

        public bool DeleteItem(Item item)
        {
            try
            {
                _= UpdateGuid(item, item.ItemId);
                SaveChages();
                
            }catch(Exception ex)
            {
                return false;
            }
            return true;
        }

        public bool ExistedItem(string ItemCode)
            => Any(item => item.ItemCode.ToLower().Equals(ItemCode.ToLower()));

        //public Item GetItem(Guid id)
        //{
        //    Item item = new Item();
        //    try
        //    {
        //        item = GetByIdGuid(id).Result;
        //    }catch(Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }

        //    return item;
        //}

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
