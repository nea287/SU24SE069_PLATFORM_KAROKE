using Microsoft.Extensions.Options;
using SU24SE069_PLATFORM_KAROKE_DataAccess.Models;

namespace SU24SE069_PLATFORM_KAROKE_API.AppStarts.OptionSetup
{
    public class MoMoOptionsSetup : IConfigureOptions<MomoOptionModel>
    {
        const string SectionName = "MomoAPI";
        readonly IConfiguration configuration;

        public MoMoOptionsSetup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void Configure(MomoOptionModel options)
        {
            configuration.GetSection(SectionName).Bind(options);
        }
    }
}
