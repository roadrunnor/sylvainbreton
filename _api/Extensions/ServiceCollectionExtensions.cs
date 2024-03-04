﻿namespace api_sylvainbreton.Extensions
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
            // MVC controllers to the services collection
            services.AddControllers();
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

            // Other service configurations from Program.cs
            services.AddAutoMapper(typeof(Program));
            services.AddCachingServices();
            services.AddControllersWithViews().AddCustomJsonOptions();
            services.AddCustomCorsPolicy(configuration);
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

