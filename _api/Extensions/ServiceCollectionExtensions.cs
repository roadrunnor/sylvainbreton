namespace api_sylvainbreton.Extensions
{
    using Microsoft.Extensions.DependencyInjection;
    using api_sylvainbreton.Services.Interfaces;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<ISanitizationService, SanitizationService>();
            return services;
        }
    }
}
