using Microsoft.AspNetCore.Http;
using SU24SE069_PLATFORM_KAROKE_Service.ReponseModels;
using SU24SE069_PLATFORM_KAROKE_Service.ReponseModels.Momo;
using SU24SE069_PLATFORM_KAROKE_Service.RequestModels.MoMo;
using SU24SE069_PLATFORM_KAROKE_Service.RequestModels.MoneyTransaction;

namespace SU24SE069_PLATFORM_KAROKE_Service.IServices
{
    public interface IMomoService
    {
        public Task<MoMoCreatePaymentResponse?> CreatePaymentAsync(MonetaryTransactionRequestModel transactionRequest);
        public Task ProcessMoMoIpnRequest(MoMoIpnRequest ipnRequest);
    }
}
