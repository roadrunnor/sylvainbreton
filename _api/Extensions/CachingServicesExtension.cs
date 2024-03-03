namespace api_sylvainbreton.Extensions
{
    public static class CachingServicesExtension
    {
        public static IServiceCollection AddCachingServices(this IServiceCollection services)
        {
            services.AddMemoryCache();
            return services;
        }
    }
}
