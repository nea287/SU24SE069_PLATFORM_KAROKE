using SU24SE069_PLATFORM_KAROKE_BusinessLayer.Commons;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.ReponseModels.Helpers;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.RequestModels.Helpers;
using SU24SE069_PLATFORM_KAROKE_Service.ReponseModels;
using SU24SE069_PLATFORM_KAROKE_Service.RequestModels.MoneyTransaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace SU24SE069_PLATFORM_KAROKE_Service.IServices
{
    public interface IMonetaryTransactionService
    {
        public Task<DynamicModelResponse.DynamicModelsResponse<MonetaryTransactionViewModel>> GetTransactions(MonetaryTransactionViewModel filter, PagingRequest paging, MonetaryTransactionOrderFilter orderFilter);
        public Task<DynamicModelResponse.DynamicModelsResponse<MonetaryTransactionViewModel>> GetTransactionsForAdmin(string? filter, PagingRequest paging, MonetaryTransactionOrderFilter orderFilter);
        public Task<ResponseResult<MonetaryTransactionViewModel>> CreateTransaction(MonetaryTransactionRequestModel request);
        public Task<ResponseResult<MonetaryTransactionViewModel>> UpdateStatusTransaction(Guid id, PaymentStatus status);
        public Task<ResponseResult<MonetaryTransactionViewModel>> BuyPackageTransaction(MonetaryTransactionRequestModel request);

    }
}
