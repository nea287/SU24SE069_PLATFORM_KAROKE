using Net.payOS.Types;
using SU24SE069_PLATFORM_KAROKE_Service.RequestModels.PayOS;

namespace SU24SE069_PLATFORM_KAROKE_Service.IServices
{
    public interface IPayOSService
    {
        Task<CreatePaymentResult?> CreatePaymentLink(long orderCode, string description, List<ItemData> items);
        Task<PaymentLinkInformation?> GetPaymentLinkInformation(long orderId);
        Task<PaymentLinkInformation?> CancelPaymentLinkInformation(long orderId, string? cancellationReason = null);
        Task ProcessPayOSWebhookRequest(PayOSWebhookRequest webhookRequest);
        Task OnCancelPaymentLinkRequest(PayOSPaymentQuery paymentQuery);
    }
}
