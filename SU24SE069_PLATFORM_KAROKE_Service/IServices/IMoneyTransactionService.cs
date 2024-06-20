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
    public interface IMoneyTransactionService
    {
        public Task<DynamicModelResponse.DynamicModelsResponse<MoneyTransactionViewModel>> GetTransactions(MoneyTransactionViewModel filter, PagingRequest paging, MoneyTransactionOrderFilter orderFilter);
        public Task<ResponseResult<MoneyTransactionViewModel>> CreateTransaction(MoneyTransactionRequestModel request);
        public Task<ResponseResult<MoneyTransactionViewModel>> UpdateStatusTransaction(Guid id, PaymentStatus status);

    }
}
