using Net.payOS.Types;
using SU24SE069_PLATFORM_KAROKE_DataAccess.Models;

namespace SU24SE069_PLATFORM_KAROKE_Service.ReponseModels.PayOS
{
    public class PayOSPackagePaymentResponse
    {
        // PayOS payment link detail
        public string bin { get; set; } = string.Empty;
        public string accountNumber { get; set; } = string.Empty;
        public int amount { get; set; }
        public string description { get; set; } = string.Empty;
        public long orderCode { get; set; }
        public string currency { get; set; } = string.Empty;
        public string paymentLinkId { get; set; } = string.Empty;
        public string status { get; set; } = string.Empty;
        public string checkoutUrl { get; set; } = string.Empty;
        public string qrCode { get; set; } = string.Empty;

        // Package & transaction detail
        public string PackageId { get; set; } = string.Empty;
        public string PackageName { get; set; } = string.Empty;
        public int UpAmount { get; set; }
        public decimal MoneyAmount { get; set; }
        public string AccountId { get; set; } = string.Empty;
        public string MonetaryTransactionId { get; set; } = string.Empty;

        public void MapPayOSPaymentLink(CreatePaymentResult createPaymentResult)
        {
            if (createPaymentResult != null)
            {
                bin = createPaymentResult.bin;
                accountNumber = createPaymentResult.accountNumber;
                amount = createPaymentResult.amount;
                description = createPaymentResult.description;
                orderCode = createPaymentResult.orderCode;
                currency = createPaymentResult.currency;
                paymentLinkId = createPaymentResult.paymentLinkId;
                status = createPaymentResult.status;
                checkoutUrl = createPaymentResult.checkoutUrl;
                qrCode = createPaymentResult.qrCode;
            }
        }

        public void MapTransactionData(MonetaryTransactionViewModel transaction, int upAmount)
        {
            if (transaction != null)
            {
                PackageId = transaction.PackageId.ToString();
                PackageName = transaction.PackageName;
                UpAmount = upAmount;
                MoneyAmount = (decimal)transaction.MoneyAmount;
                AccountId = transaction.MemberId.ToString();
                MonetaryTransactionId = transaction.MonetaryTransactionId.ToString();
            }
        }

        public void MapTransactionEntityData(MonetaryTransaction transaction, Package upPackage)
        {
            if (transaction != null && upPackage != null)
            {
                PackageId = transaction.PackageId.ToString();
                PackageName = upPackage.PackageName;
                UpAmount = upPackage.StarNumber;
                MoneyAmount = transaction.MoneyAmount;
                AccountId = transaction.MemberId.ToString();
                MonetaryTransactionId = transaction.MonetaryTransactionId.ToString();
            }
        }
    }
}
