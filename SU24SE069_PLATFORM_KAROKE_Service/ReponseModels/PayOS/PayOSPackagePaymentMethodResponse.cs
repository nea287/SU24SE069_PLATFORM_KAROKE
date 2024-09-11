using SU24SE069_PLATFORM_KAROKE_DataAccess.Models;

namespace SU24SE069_PLATFORM_KAROKE_Service.ReponseModels.PayOS
{
    public class PayOSPackagePaymentMethodResponse
    {
        public string checkoutUrl { get; set; } = string.Empty;
        public string qrCode { get; set; } = string.Empty;

        // Package & transaction detail
        public string PackageId { get; set; } = string.Empty;
        public string PackageName { get; set; } = string.Empty;
        public int UpAmount { get; set; }
        public decimal MoneyAmount { get; set; }
        public string AccountId { get; set; } = string.Empty;
        public string MonetaryTransactionId { get; set; } = string.Empty;
        public DateTime CreatedDate {  get; set; } = DateTime.Now;

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
                CreatedDate = transaction.CreatedDate;
            }
        }
    }
}
