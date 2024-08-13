using SU24SE069_PLATFORM_KAROKE_BusinessLayer.Commons;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.ReponseModels.Helpers;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.RequestModels.Helpers;
using SU24SE069_PLATFORM_KAROKE_Service.ReponseModels;
using SU24SE069_PLATFORM_KAROKE_Service.RequestModels.Singer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Service.IServices
{
    public interface ISingerService
    {
        public Task<ResponseResult<SingerViewModel>> GetSinger(Guid id);
        public Task<DynamicModelResponse.DynamicModelsResponse<SingerViewModel>> GetSingers(SingerViewModel filter, PagingRequest paging, SingerOrderFilter orderFilter);
        public Task<DynamicModelResponse.DynamicModelsResponse<SingerViewModel>> GetSingersForAdmin(string? filter, PagingRequest paging, SingerOrderFilter orderFilter);
        public Task<ResponseResult<SingerViewModel>> CreateSinger(SingerRequestModel request);
        public Task<ResponseResult<SingerViewModel>> DeleteSinger(Guid id);
        public Task<ResponseResult<SingerViewModel>> UpdateSinger(Guid id, SingerRequestModel request);
    }
}
