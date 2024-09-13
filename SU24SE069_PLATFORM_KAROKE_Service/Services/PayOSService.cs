using Microsoft.Extensions.Options;
using Net.payOS;
using Net.payOS.Errors;
using Net.payOS.Types;
using Newtonsoft.Json.Linq;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.Commons;
using SU24SE069_PLATFORM_KAROKE_DataAccess.Models;
using SU24SE069_PLATFORM_KAROKE_Repository.IRepository;
using SU24SE069_PLATFORM_KAROKE_Service.Commons;
using SU24SE069_PLATFORM_KAROKE_Service.Helpers;
using SU24SE069_PLATFORM_KAROKE_Service.IServices;
using SU24SE069_PLATFORM_KAROKE_Service.RequestModels.PayOS;
using System.Security.Principal;
using System.Text;

namespace SU24SE069_PLATFORM_KAROKE_Service.Services
{
    public class PayOSService : IPayOSService
    {
        private const string PayOSPaymentSuccessCode = "00";
        private const string PayOSPaidStatus = "PAID";
        private const string PayOSPendingStatus = "PENDING";
        private const string PayOSProcessingStatus = "PROCESSING";
        private const string PayOSCancelledStatus = "CANCELLED";

        private readonly PayOSCredential payOSCredential;
        private readonly IMonetaryTransactionService _monetaryTransactionService;
        private readonly IMonetaryTransactionRepository _monetaryTransactionRepository;
        private readonly IPackageRepository _packageRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IInAppTransactionRepository _inAppTransactionRepository;
        private readonly INotificationService _notificationService;

        public PayOSService(IOptions<PayOSCredential> options,
            IMonetaryTransactionService monetaryTransactionService,
            IMonetaryTransactionRepository monetaryTransactionRepository,
            IPackageRepository packageRepository,
            IAccountRepository accountRepository,
            IInAppTransactionRepository inAppTransactionRepository,
            INotificationService notificationService)
        {
            payOSCredential = options.Value;
            _monetaryTransactionService = monetaryTransactionService;
            _monetaryTransactionRepository = monetaryTransactionRepository;
            _accountRepository = accountRepository;
            _packageRepository = packageRepository;
            _inAppTransactionRepository = inAppTransactionRepository;
            _notificationService = notificationService;
        }

        public async Task<PaymentLinkInformation?> CancelPaymentLinkInformation(long orderId, string? cancellationReason = null)
        {
            // Validate orderCode/orderId
            if (orderId < 0.0 - (Math.Pow(2.0, 53.0) - 1.0) || orderId > Math.Pow(2.0, 53.0) - 1.0)
            {
                Console.WriteLine("Order code is out of range! Failed to cancel payOS's payment link.");
                return null;
            }

            // Initialize payOS instance
            var payOS = InitializePayOS();
            if (payOS == null)
            {
                Console.WriteLine("Failed to initialize payOS instance!");
                return null;
            }

            try
            {
                if (string.IsNullOrEmpty(cancellationReason))
                {
                    return await payOS.cancelPaymentLink(orderId);
                }
                return await payOS.cancelPaymentLink(orderId, cancellationReason);
            }
            catch (PayOSError ex)
            {
                Console.WriteLine($"Error when trying to cancel payOS's created payment link:\nCode: {ex.Code}\nMessage: {ex.Message}");
                return null;
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Error when trying to deserialize JSON response of cancel payment link operation: {ex.Message}");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error when trying to cancel payOS's payment link: {ex.Message}");
                return null;
            }
        }

        public async Task<CreatePaymentResult?> CreatePaymentLink(long orderCode, string description, List<ItemData> items)
        {
            // Initialize payOS instance
            var payOS = InitializePayOS();
            if (payOS == null)
            {
                Console.WriteLine("Failed to initialize payOS instance!");
                return null;
            }

            // Calculate total amount
            int totalAmount = 0;
            foreach (ItemData item in items)
            {
                totalAmount += item.price;
            }

            // Create payment data
            PaymentData paymentData = new PaymentData(orderCode,
                totalAmount,
                description,
                items,
                payOSCredential.CancelUrl,
                payOSCredential.ReturnUrl);

            try
            {
                CreatePaymentResult createPayment = await payOS.createPaymentLink(paymentData);
                return createPayment;
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine($"Error when trying to create payOS's payment link: {ex.Message}");
                return null;
            }
            catch (PayOSError ex)
            {
                Console.WriteLine($"Error when trying to create payOS's payment link:\nCode: {ex.Code}\nMessage: {ex.Message}");
                return null;
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Error when trying to deserialize JSON response of payOS's create payment link: {ex.Message}");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error when trying to create payOS's payment link: {ex.Message}");
                return null;
            }
        }

        public async Task<PaymentLinkInformation?> GetPaymentLinkInformation(long orderId)
        {
            // Validate orderCode/orderId
            if (orderId < 0.0 - (Math.Pow(2.0, 53.0) - 1.0) || orderId > Math.Pow(2.0, 53.0) - 1.0)
            {
                Console.WriteLine("Order code is out of range! Failed to retrieve payOS's created payment information.");
                return null;
            }

            // Initialize payOS instance
            var payOS = InitializePayOS();
            if (payOS == null)
            {
                Console.WriteLine("Failed to initialize payOS instance!");
                return null;
            }

            try
            {
                var result = await payOS.getPaymentLinkInformation(orderId);
                return result;
            }
            catch (PayOSError ex)
            {
                Console.WriteLine($"Error when trying to retrieve payOS's created payment information:\nCode: {ex.Code}\nMessage: {ex.Message}");
                return null;
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Error when trying to deserialize JSON response of payOS's created payment information: {ex.Message}");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error when trying to retrieve payOS's created payment information: {ex.Message}");
                return null;
            }
        }

        private PayOS InitializePayOS()
        {
            return new PayOS(payOSCredential.ClientID, payOSCredential.ApiKey, payOSCredential.ChecksumKey);
        }

        public async Task ProcessPayOSWebhookRequest(PayOSWebhookRequest webhookRequest)
        {
            if (webhookRequest == null || webhookRequest.data == null)
            {
                return;
            }

            // Validate webhook result
            if (!IsWebhookDataValid(webhookRequest))
            {
                return;
            }

            // Handle failed payment result
            if (!webhookRequest.success || webhookRequest.code != PayOSPaymentSuccessCode)
            {
                await HandleFailedPayOSPayment(webhookRequest);
                return;
            }

            // Handle success payment result
            await HandleSuccessPayOSPayment(webhookRequest);
        }

        private bool IsWebhookDataValid(PayOSWebhookRequest webhookRequest)
        {
            JObject jsonObject = JObject.FromObject(webhookRequest.data);
            IEnumerable<string> sortedKeys = jsonObject.Properties().Select(p => p.Name).OrderBy(key => key);
            StringBuilder transactionStr = new StringBuilder();
            foreach (string key in sortedKeys)
            {
                string value = jsonObject[key].ToString();
                transactionStr.Append(key);
                transactionStr.Append('=');
                transactionStr.Append(value);
                if (key != sortedKeys.Last())
                {
                    transactionStr.Append('&');
                }
            }

            string signature = HashHelper.HmacSHA256(transactionStr.ToString(), payOSCredential.ChecksumKey);
            return signature.Equals(webhookRequest.signature, StringComparison.OrdinalIgnoreCase);
        }

        private async Task HandleFailedPayOSPayment(PayOSWebhookRequest webhookRequest)
        {
            var monetaryTransaction = await _monetaryTransactionRepository.FindTransactionByPaymentCode(webhookRequest.data.orderCode.ToString());
            if (monetaryTransaction != null && monetaryTransaction.Status == (int)PaymentStatus.PENDING)
            {
                // Update monetary transaction's status
                monetaryTransaction.Status = (int)PaymentStatus.CANCELLED;
                await _monetaryTransactionRepository.Update(monetaryTransaction);
                await _monetaryTransactionRepository.SaveChagesAsync();

                // Create and send notification to user
                await _notificationService.CreateAndSendNotification(new RequestModels.Notification.CreateNotificationRequestModel()
                {
                    Description = $"Yêu cầu nạp gói UP đã được hủy bỏ!",
                    NotificationType = NotificationType.TRANSACTION_NOTI,
                    AccountId = monetaryTransaction.MemberId,
                });
            }
        }

        private async Task HandleSuccessPayOSPayment(PayOSWebhookRequest webhookRequest)
        {
            var monetaryTransaction = await _monetaryTransactionRepository.FindTransactionByPaymentCode(webhookRequest.data.orderCode.ToString());
            if (monetaryTransaction != null && monetaryTransaction.Status == (int)PaymentStatus.PENDING)
            {
                // Update monetary transaction's status
                monetaryTransaction.Status = (int)PaymentStatus.COMPLETE;
                await _monetaryTransactionRepository.Update(monetaryTransaction);
                await _monetaryTransactionRepository.SaveChagesAsync();

                // Fetch user information
                var account = await _accountRepository.GetByIdGuid(monetaryTransaction.MemberId);

                // Fetch UP package information
                var upPackage = await _packageRepository.GetByIdGuid(monetaryTransaction.PackageId);

                if (account == null || upPackage == null)
                {
                    // Failed to retrieve account or up package data
                    return;
                }

                // Create in-app transaction
                var inAppTransaction = new InAppTransaction()
                {
                    CreatedDate = DateTime.Now,
                    MemberId = account.AccountId,
                    SongId = null,
                    ItemId = null,
                    MonetaryTransactionId = monetaryTransaction.MonetaryTransactionId,
                    Status = (int)PaymentStatus.COMPLETE,
                    UpAmountBefore = account.UpBalance,
                    UpTotalAmount = upPackage.StarNumber,
                    TransactionType = (int)InAppTransactionType.RECHARGE_UP_BALANCE,
                };

                // Save InAppTransaction
                var addInAppTransaction = await _inAppTransactionRepository.CreateInAppTransaction(inAppTransaction);

                if (!addInAppTransaction)
                {
                    // Failed to add in-app transaction
                    return;
                }

                // Update user's UP balance
                account.UpBalance += upPackage.StarNumber;
                await _accountRepository.Update(account);
                await _accountRepository.SaveChagesAsync();

                Console.WriteLine("Finish process payOS webhook!");

                // Create and send notification to user
                await _notificationService.CreateAndSendNotification(new RequestModels.Notification.CreateNotificationRequestModel()
                {
                    Description = $"Yêu cầu nạp gói UP đã được thanh toán thành công!",
                    NotificationType = NotificationType.TRANSACTION_NOTI,
                    AccountId = account.AccountId,
                });
            }
        }

        public async Task OnCancelPaymentLinkRequest(PayOSPaymentQuery paymentQuery)
        {
            if (paymentQuery.cancel &&
                paymentQuery.code == PayOSPaymentSuccessCode &&
                paymentQuery.status == PayOSCancelledStatus)
            {
                var monetaryTransaction = await _monetaryTransactionRepository.FindTransactionByPaymentCode(paymentQuery.orderCode.ToString());
                if (monetaryTransaction != null && monetaryTransaction.Status == (int)PaymentStatus.PENDING)
                {
                    // Update monetary transaction's status
                    monetaryTransaction.Status = (int)PaymentStatus.CANCELLED;
                    await _monetaryTransactionRepository.Update(monetaryTransaction);
                    await _monetaryTransactionRepository.SaveChagesAsync();
                }
            }
        }
    }
}
