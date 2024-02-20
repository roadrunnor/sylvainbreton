namespace api_sylvainbreton.Extensions
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using api_sylvainbreton.Configurations;
    using api_sylvainbreton.Services;
    using api_sylvainbreton.Services.Interfaces;
    // Ensure all necessary namespaces are included for your service registrations

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Existing service registration
            services.AddScoped<ISanitizationService, SanitizationService>();

            // Incorporate other service configurations from Program.cs
            services.AddCachingServices();
            services.AddControllersWithViews().AddCustomJsonOptions();
            services.AddCustomCorsPolicy();
            services.AddCustomDataProtection();
            services.AddDatabaseConfiguration(configuration);
            services.AddEndpointsApiExplorer();
            services.AddExternalAuthentication(configuration);
            services.AddIdentityServerWithCertificate();
            services.AddIdentityServices();
            services.AddSwaggerGen();

            // Any additional services your application needs
            return services;
        }
    }
}

