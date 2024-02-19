namespace api_sylvainbreton.Services.Interfaces
{
    using Microsoft.Extensions.DependencyInjection;

    public static class CachingServiceExtensions
    {
        public static IServiceCollection AddCachingServices(this IServiceCollection services)
        {
            services.AddMemoryCache();
            return services;
        }
    }
}
