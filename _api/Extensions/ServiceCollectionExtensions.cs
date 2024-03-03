namespace api_sylvainbreton.Extensions
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using api_sylvainbreton.Configurations;
    using api_sylvainbreton.Services.Interfaces;
    using api_sylvainbreton.Services.Implementations;
    using api_sylvainbreton.Services.Utilities;
    using api_sylvainbreton.Services.Repositories;
    using api_sylvainbreton.Services;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Add MVC controllers to the services collection
            services.AddControllers();

            // Scoped services
            services.AddScoped<IArtistService, ArtistService>();
            services.AddScoped<IArtistService, ArtistService>();
            services.AddScoped<IArtistRepository, ArtistRepository>();
            services.AddScoped<IArtworkService, ArtworkService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ImageProcessingService>();
            services.AddScoped<ImageService>();
            services.AddScoped<ImageValidationService>();
            services.AddScoped<ISanitizationService, SanitizationService>();
            services.AddScoped<RolesManagementService>();

            // Incorporate other service configurations from Program.cs
            // Note: Ensure that you include method implementations for these if they're not already implemented
            services.AddAutoMapper(typeof(Program));
            services.AddCachingServices();
            services.AddControllersWithViews().AddCustomJsonOptions();
            services.AddCustomCorsPolicy();
            services.AddCustomDataProtection();
            services.AddDatabaseConfiguration(configuration);
            services.AddEndpointsApiExplorer();
            services.AddExternalAuthentication(configuration);
            services.AddIdentityServerWithCertificate();
            services.AddIdentityServices(configuration);
            services.AddSwaggerGen();

            return services;
        }
    }
}

