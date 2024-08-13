using Microsoft.Extensions.Options;
using SU24SE069_PLATFORM_KAROKE_Service.Commons;

namespace SU24SE069_PLATFORM_KAROKE_API.AppStarts.OptionSetup
{
    public class FirebaseCredentialOptionSetup : IConfigureOptions<FirebaseCredential>
    {
        const string SectionName = "FirebaseCredential";
        readonly IConfiguration configuration;

        public FirebaseCredentialOptionSetup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public void Configure(FirebaseCredential options)
        {
            configuration.GetSection(SectionName).Bind(options);
        }
    }
}
