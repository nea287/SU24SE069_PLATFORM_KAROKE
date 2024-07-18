namespace SU24SE069_PLATFORM_KAROKE_Service.RequestModels.Account
{
    public class MemberAccountVerifyRequest
    {
        public string AccountEmail { get; set; } = string.Empty;
        public string VerifyCode { get; set; } = string.Empty;
    }
}
