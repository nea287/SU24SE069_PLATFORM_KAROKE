using SU24SE069_PLATFORM_KAROKE_BusinessLayer.Commons;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.ReponseModels.Helpers;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.RequestModels.Helpers;
using SU24SE069_PLATFORM_KAROKE_Service.ReponseModels;
using SU24SE069_PLATFORM_KAROKE_Service.RequestModels.Recording;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Service.IServices
{
    public interface IRecordingService
    {
        public Task<ResponseResult<RecordingViewModel>> CreateRecording(CreateRecordingRequestModel request);
        public Task<ResponseResult<RecordingViewModel>> UpdateRecording(Guid id, UpdateRecording1RequestModel request);
        public Task<DynamicModelResponse.DynamicModelsResponse<RecordingViewModel>> GetRecordings(RecordingViewModel filter, PagingRequest paging, RecordingOrderFilter orderFilter);
        public Task<ResponseResult<RecordingViewModel>> Delete(Guid id);
  
    }
}
