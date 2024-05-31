using SU24SE069_PLATFORM_KAROKE_BusinessLayer.IServices;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.Services;
using SU24SE069_PLATFORM_KAROKE_DAO.DAO;
using SU24SE069_PLATFORM_KAROKE_DAO.IDAO;
using SU24SE069_PLATFORM_KAROKE_Repository.IRepository;
using SU24SE069_PLATFORM_KAROKE_Repository.Repository;

namespace SU24SE069_PLATFORM_KAROKE_API.AppStarts
{
    public static class DependencyInjectionResolver
    {
        public static void ConfigDI(this IServiceCollection services)
        {
            services.AddSingleton(typeof(IBaseDAO<>), typeof(BaseDAO<>));
            services.AddScoped<ITokenService, TokenService>();

            services.AddSingleton(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IAccountService, AccountService>();

            
        }
    }
}
