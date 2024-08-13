using Microsoft.AspNetCore.Mvc;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.Commons;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.ReponseModels;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.ReponseModels.Helpers;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.RequestModels.Helpers;
using SU24SE069_PLATFORM_KAROKE_Service.Filters;
using SU24SE069_PLATFORM_KAROKE_Service.Filters.Song;
using SU24SE069_PLATFORM_KAROKE_Service.ReponseModels;
using SU24SE069_PLATFORM_KAROKE_Service.RequestModels.Song;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SU24SE069_PLATFORM_KAROKE_BusinessLayer.ReponseModels.Helpers.DynamicModelResponse;

namespace SU24SE069_PLATFORM_KAROKE_Service.IServices
{
    public interface ISongService
    {
        public Task<ResponseResult<SongDTO>> GetSong(Guid id);
        public Task<ResponseResult<SongViewModel>> UpdateSong(Guid id, UpdateSongRequestModel song);
        public Task<ResponseResult<SongViewModel>> DeleteSong(Guid id);
        public Task<ResponseResult<SongViewModel>> CreateSong(CreateSongRequestModel request);
        public DynamicModelResponse.DynamicModelsResponse<SongViewModel> GetSongs(SongFilter filter, PagingRequest paging, SongOrderFilter orderFilter);
        public DynamicModelResponse.DynamicModelsResponse<SongViewModel> GetSongsForAdmin(string filter, PagingRequest paging, SongOrderFilter orderFilter);

        Task<DynamicModelsResponse<SongDTO>> GetSongsPurchaseFavorite(Guid accountId, KaraokeSongFilter filter, PagingRequest paging, SongOrderFilter orderFilter = SongOrderFilter.SongName);
    }
}
