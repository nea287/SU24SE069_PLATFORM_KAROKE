using SU24SE069_PLATFORM_KAROKE_BusinessLayer.Commons;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.ReponseModels;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.ReponseModels.Helpers;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.RequestModels.Helpers;
using SU24SE069_PLATFORM_KAROKE_Service.ReponseModels;
using SU24SE069_PLATFORM_KAROKE_Service.RequestModels.Song;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Service.IServices
{
    public interface ISongService
    {
        public ResponseResult<SongViewModel> GetSong(Guid id);
        public ResponseResult<SongViewModel> UpdateSong(Guid id, UpdateSongRequestModel song);
        public ResponseResult<SongViewModel> DeleteSong(Guid id);
        public ResponseResult<SongViewModel> CreateSong(CreateSongRequestModel request);
        public DynamicModelResponse.DynamicModelsResponse<SongViewModel> GetSongs(SongViewModel filter,
            PagingRequest paging, SongOrderFilter orderFilter);
    }
}
