using SU24SE069_PLATFORM_KAROKE_BusinessLayer.Commons;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.ReponseModels.Helpers;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.RequestModels.Helpers;
using SU24SE069_PLATFORM_KAROKE_Service.ReponseModels;
using SU24SE069_PLATFORM_KAROKE_Service.RequestModels.FavouriteSong;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Service.IServices
{
    public interface IFavouriteSongSerivce
    {
        public Task<ResponseResult<FavouriteSongViewModel>> CreateFavouteSong(CreateFavouriteSongRequestModel request);
        public Task<DynamicModelResponse.DynamicModelsResponse<FavouriteSongViewModel>> GetFavouriteSongs(FavouriteSongViewModel filter, PagingRequest paging, FavouriteSongOrderFilter orderFilter);
        public Task<ResponseResult<FavouriteSongViewModel>> DeleteFavouteSong(CreateFavouriteSongRequestModel request);

        public Task<DynamicModelResponse.DynamicModelsResponse<FavoriteSongDTO>> GetFavoriteSongsPurchasedFilter(FavouriteSongViewModel filter, PagingRequest paging, FavouriteSongOrderFilter orderFilter);
    }
}
