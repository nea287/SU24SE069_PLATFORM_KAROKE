using SU24SE069_PLATFORM_KAROKE_BusinessLayer.Commons;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.ReponseModels.Helpers;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.RequestModels.Helpers;
using SU24SE069_PLATFORM_KAROKE_Service.ReponseModels;
using SU24SE069_PLATFORM_KAROKE_Service.RequestModels.InAppTransaction;
using SU24SE069_PLATFORM_KAROKE_Service.RequestModels.PurchasedSong;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Service.IServices
{
    public interface IInAppTransactionService
    {
        public Task<ResponseResult<InAppTransactionViewModel>> GetTransaction(Guid id);
        public Task<ResponseResult<InAppTransactionViewModel>> CreateInAppTransaction(CrreateInAppTransactionRequestModel request);
        public Task<ResponseResult<InAppTransactionViewModel>> UpdateInAppTransaction(UpdateInAppTransactionRequestModel request, Guid id);
        public Task<DynamicModelResponse.DynamicModelsResponse<InAppTransactionViewModel>> GetTransactions(InAppTransactionViewModel filter, PagingRequest paging, InAppTransactionOrderFilter orderFilter);

        public Task<ResponseResult<InAppTransactionViewModel>> PurchaseSong(PurchasedSongRequestModel request);


    }
}
