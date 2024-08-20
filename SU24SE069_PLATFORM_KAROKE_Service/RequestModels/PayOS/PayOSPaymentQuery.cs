namespace SU24SE069_PLATFORM_KAROKE_Service.RequestModels.PayOS
{
    public class PayOSPaymentQuery
    {
        /// <summary>
        /// Mã lỗi
        /// </summary>
        public string code { get; set; } = string.Empty;
        /// <summary>
        /// Payment Link Id
        /// </summary>
        public string id { get; set; } = string.Empty;
        /// <summary>
        /// Trạng thái hủy
        /// </summary>
        public bool cancel { get; set; } = false;
        /// <summary>
        /// Trạng thái thanh toán
        /// </summary>
        public string status { get; set; } = string.Empty;
        /// <summary>
        /// Mã đơn hàng
        /// </summary>
        public int orderCode { get; set; }
    }
}
