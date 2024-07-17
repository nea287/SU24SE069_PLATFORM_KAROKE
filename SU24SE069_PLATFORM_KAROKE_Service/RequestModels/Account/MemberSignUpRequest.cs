using SU24SE069_PLATFORM_KAROKE_BusinessLayer.Commons;

namespace SU24SE069_PLATFORM_KAROKE_Service.RequestModels.Account
{
    public class MemberSignUpRequest
    {
        public string Email { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public AccountGender Gender { get; set; } = AccountGender.OTHERS;
    }
}
