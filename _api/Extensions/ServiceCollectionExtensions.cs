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
            // Single call to AddControllersWithViews if your application uses both MVC views and API controllers.
            // If only API controllers are used, consider replacing it with AddControllers().
            services.AddControllersWithViews().AddCustomJsonOptions();


            // MVC controllers to the services collection
            services.AddScoped<IArtistService, ArtistService>();
            services.AddScoped<IArtistRepository, ArtistRepository>();
            services.AddScoped<IArtworkService, ArtworkService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ImageProcessingService>();
            services.AddScoped<ImageService>();
            services.AddScoped<ImageValidationService>();
            services.AddScoped<ISanitizationService, SanitizationService>();
            services.AddScoped<RolesManagementService>();
            services.AddScoped<ApplicationSetupService>();
            services.AddScoped<AuthenticationService>();

            // Additional service configurations
            services.AddAutoMapper(typeof(Program));
            services.AddCachingServices();
            services.AddCustomCorsPolicy(configuration);
            services.AddDatabaseConfiguration(configuration);
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddIdentityServices(configuration);
            services.AddIdentityServerWithCertificate();
            services.AddExternalAuthentication(configuration);
            services.AddJwtAuthentication();

            // Security configurations.
            services.ConfigureSecurityServices(configuration);

            return services;
        }

        public static IServiceCollection ConfigureSecurityServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Security services configurations. 
            services.AddCustomDataProtection(configuration);
            services.AddCertificateValidation(configuration);

            return services;
        }

        public static async Task InitializeApplicationDataAsync(this IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var setupService = scope.ServiceProvider.GetRequiredService<ApplicationSetupService>();
            await setupService.InitializeAsync();
        }
    }
}