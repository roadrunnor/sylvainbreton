using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace api_sylvainbreton.Extensions
{
    public static class CorsExtensions
    {
        public static IServiceCollection AddCustomCorsPolicy(this IServiceCollection services, IConfiguration configuration)
        {
            // Assuming you have a configuration section for allowed origins
            var allowedOrigins = configuration.GetSection("Cors:AllowedOrigins").Get<string[]>();

            // Check if any allowed origins are configured
            if (allowedOrigins == null || allowedOrigins.Length == 0)
            {
                // Log a warning or throw an exception based on your preference
                // For example, you might want to allow a very permissive policy for development:
                allowedOrigins = ["*"]; // Use with caution, ideally only in development
            }

            services.AddCors(options =>
            {
                options.AddPolicy("CustomCorsPolicy", policyBuilder =>
                {
                    policyBuilder
                        .SetIsOriginAllowedToAllowWildcardSubdomains()
                        .WithExposedHeaders("X-My-Custom-Header")
                        .WithOrigins(allowedOrigins)
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials()
                        .WithExposedHeaders("Content-Disposition"); // Example of exposing custom headers

                    // Conditionally add AllowCredentials based on your application needs
                    // Remember, AllowCredentials cannot be used with the wildcard origin '*'
                    if (!allowedOrigins.Contains("*"))
                    {
                        policyBuilder.AllowCredentials();
                    }
                });
            });

            return services;
        }
    }
}
