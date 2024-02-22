namespace api_sylvainbreton.Extensions
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using api_sylvainbreton.Configurations;
    using api_sylvainbreton.Services;
    using api_sylvainbreton.Services.Interfaces;
    using api_sylvainbreton.Data;
    using Microsoft.EntityFrameworkCore;
    using api_sylvainbreton.Utilities;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Add MVC controllers to the services collection
            services.AddControllers();

            // Add DbContext with options
            services.AddDbContext<SylvainBretonDbContext>(options =>
                options.UseMySql(configuration.GetConnectionString("SylvainBretonConnection"),
                ServerVersion.AutoDetect(configuration.GetConnectionString("SylvainBretonConnection"))));

            // Scoped services
            services.AddScoped<ImageProcessingService>();
            services.AddScoped<ImageService>();
            services.AddScoped<ImageValidationService>();
            services.AddScoped<ISanitizationService, SanitizationService>();

            // Incorporate other service configurations from Program.cs
            // Note: Ensure that you include method implementations for these if they're not already implemented
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

