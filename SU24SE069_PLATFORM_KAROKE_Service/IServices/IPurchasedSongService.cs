using SU24SE069_PLATFORM_KAROKE_BusinessLayer.Commons;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.ReponseModels.Helpers;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.RequestModels.Helpers;
using SU24SE069_PLATFORM_KAROKE_DataAccess.Models;
using SU24SE069_PLATFORM_KAROKE_Service.ReponseModels;
using SU24SE069_PLATFORM_KAROKE_Service.RequestModels.PurchasedSong;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Service.IServices
{
    public interface IPurchasedSongService
    {
        public Task<DynamicModelResponse.DynamicModelsResponse<PurchasedSongViewModel>> GetPurchasedSongs(PurchasedSongViewModel filter, PagingRequest paging, PurchasedSongOrderFilter orderFilter);
        //public Task<ResponseResult<PurchasedSongViewModel>> PurchageSong(PurchasedSongRequestModel request);
    }
}
