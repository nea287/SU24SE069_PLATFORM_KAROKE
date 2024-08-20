using Microsoft.Extensions.Options;
using SU24SE069_PLATFORM_KAROKE_Service.Commons;

namespace SU24SE069_PLATFORM_KAROKE_API.AppStarts.OptionSetup
{
    public class PayOSCredentialOptionSetup : IConfigureOptions<PayOSCredential>
    {
        const string SectionName = "PayOSCredential";
        readonly IConfiguration configuration;

        public PayOSCredentialOptionSetup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void Configure(PayOSCredential options)
        {
            configuration.GetSection(SectionName).Bind(options);
        }
    }
}
