using SU24SE069_PLATFORM_KAROKE_BusinessLayer.Commons;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.ReponseModels.Helpers;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.RequestModels.Helpers;
using SU24SE069_PLATFORM_KAROKE_Service.ReponseModels;
using SU24SE069_PLATFORM_KAROKE_Service.RequestModels.Genre;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Service.IServices
{
    public interface IGenreService
    {
        public Task<ResponseResult<GenreViewModel>> GetGenre(Guid id);
        public Task<DynamicModelResponse.DynamicModelsResponse<GenreViewModel>> GetGenres(GenreViewModel filter, PagingRequest paging, GenreOrderFilter orderFilter);
        public Task<DynamicModelResponse.DynamicModelsResponse<GenreViewModel>> GetGenresForAdmin(string filter, PagingRequest paging, GenreOrderFilter orderFilter);
        public Task<ResponseResult<GenreViewModel>> CreateGenre(GenreRequestModel request);
        public Task<ResponseResult<GenreViewModel>> DeleteGenre(Guid id);
        public Task<ResponseResult<GenreViewModel>> UpdateGenre(Guid id, GenreRequestModel request);
    }
}
