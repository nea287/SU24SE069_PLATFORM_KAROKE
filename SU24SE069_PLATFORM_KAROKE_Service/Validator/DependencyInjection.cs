using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;

namespace SU24SE069_PLATFORM_KAROKE_Service.Validator
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddFluentValidator(this IServiceCollection services)
        {
            var assembly = typeof(DependencyInjection).Assembly;
            services.AddValidatorsFromAssembly(assembly);
            services.AddFluentValidationAutoValidation();
            return services;
        }
    }
}
