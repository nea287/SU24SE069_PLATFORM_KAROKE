namespace SU24SE069_PLATFORM_KAROKE_Service.RequestModels.PayOS
{
    public class PayOSWebhookRequest
    {
        /// <summary>
        /// Mã lỗi
        /// </summary>
        public string code { get; set; } = string.Empty;
        /// <summary>
        /// Thông tin lỗi
        /// </summary>
        public string desc { get; set; } = string.Empty;
        /// <summary>
        /// Trạng thái thành công
        /// </summary>
        public bool success { get; set; }
        /// <summary>
        /// Dữ liệu
        /// </summary>
        public PayOSWebhookData data { get; set; } = null!;
        /// <summary>
        /// Chữ kí để kiểm tra thông tin
        /// </summary>
        public string signature { get; set; } = string.Empty;
    }

    public class PayOSWebhookData
    {
        /// <summary>
        /// Mã đơn hàng từ cửa hàng
        /// </summary>
        public int orderCode { get; set; }
        /// <summary>
        /// Số tiền thanh toán
        /// </summary>
        public int amount { get; set; }
        /// <summary>
        /// Mô tả thanh toán
        /// </summary>
        public string description { get; set; } = string.Empty;
        /// <summary>
        /// Số tài khoản của cửa hàng
        /// </summary>
        public string accountNumber { get; set; } = string.Empty;
        /// <summary>
        /// Mã tham chiếu giao dịch, dùng để tra soát với ngân hàng
        /// </summary>
        public string reference { get; set; } = string.Empty;
        /// <summary>
        /// Ngày giờ giao dịch thực hiện thành công
        /// </summary>
        public string transactionDateTime { get; set; } = string.Empty;
        /// <summary>
        /// Đơn vị tiền tệ
        /// </summary>
        public string currency { get; set; } = string.Empty;
        /// <summary>
        /// Mã link thanh toán
        /// </summary>
        public string paymentLinkId { get; set; } = string.Empty;
        /// <summary>
        /// Mã lỗi của giao dịch
        /// </summary>
        public string code { get; set; } = string.Empty;
        /// <summary>
        /// Thông tin lỗi của giao dịch
        /// </summary>
        public string desc { get; set; } = string.Empty;
        /// <summary>
        /// Mã ngân hàng của khách hàng dùng chuyển khoản
        /// </summary>
        public string? counterAccountBankId { get; set; }
        /// <summary>
        /// Tên ngân hàng của khách hàng dùng chuyển khoản
        /// </summary>
        public string? counterAccountBankName { get; set; }
        /// <summary>
        /// Tên tài khoản của khách hàng
        /// </summary>
        public string? counterAccountName { get; set; }
        /// <summary>
        /// Số tài khoản của khách hàng
        /// </summary>
        public string? counterAccountNumber { get; set; }
        /// <summary>
        /// Tên tài khoản ảo
        /// </summary>
        public string? virtualAccountName { get; set; }
        /// <summary>
        /// Số tài khoản ảo
        /// </summary>
        public string? virtualAccountNumber { get; set; }
    }
}
