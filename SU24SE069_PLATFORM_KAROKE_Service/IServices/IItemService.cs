using SU24SE069_PLATFORM_KAROKE_BusinessLayer.ReponseModels.Helpers;
using SU24SE069_PLATFORM_KAROKE_Service.ReponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Service.IServices
{
    public interface IItemService
    {
        public ResponseResult<ItemViewModel> GetItem(Guid id);
       // public ResponseResult<ItemViewModel> UpdateItem(Guid id,  item);
    }
}
