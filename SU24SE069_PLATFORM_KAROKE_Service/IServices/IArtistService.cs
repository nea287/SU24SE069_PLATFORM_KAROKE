using SU24SE069_PLATFORM_KAROKE_BusinessLayer.Commons;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.ReponseModels.Helpers;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.RequestModels.Helpers;
using SU24SE069_PLATFORM_KAROKE_Service.ReponseModels;
using SU24SE069_PLATFORM_KAROKE_Service.RequestModels.Artist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Service.IServices
{
    public interface IArtistService
    {
        public Task<ResponseResult<ArtistViewModel>> GetArtist(Guid id);
        public Task<DynamicModelResponse.DynamicModelsResponse<ArtistViewModel>> GetArtists(ArtistViewModel filter, PagingRequest paging, ArtistOrderFilter orderFilter);
        public Task<ResponseResult<ArtistViewModel>> CreateArtist(ArtistRequestModel request);
        public Task<ResponseResult<ArtistViewModel>> DeleteArtist(Guid id);
        public Task<ResponseResult<ArtistViewModel>> UpdateArtist(Guid id, ArtistRequestModel request);
    }
}
