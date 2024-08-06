namespace SU24SE069_PLATFORM_KAROKE_DataAccess.Models
{
    public class MomoOptionModel
    {
        //public string MomoApiUrl { get; set; }
        //public string SecretKey { get; set; }
        //public string AccessKey { get; set; }
        //public string ReturnUrl { get; set; }
        //public string NotifyUrl { get; set; }
        //public string PartnerCode { get; set; }
        //public string RequestType { get; set; }
        public string PartnerCode { get; set; } = string.Empty;
        public string ReturnUrl { get; set; } = string.Empty;
        public string IpnUrl { get; set; } = string.Empty;
        public string SecretKey { get; set; } = string.Empty;
        public string AccessKey { get; set; } = string.Empty;
        public string PaymentUrl { get; set; } = string.Empty;
        public string RequestType { get; set; } = string.Empty;
    }
}
