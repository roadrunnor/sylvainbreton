namespace api_sylvainbreton.Configurations
{
    using api_sylvainbreton.Data;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using api_sylvainbreton.Models;

    public static class DatabaseConfig
    {
        public static IServiceCollection AddDatabaseConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = Environment.GetEnvironmentVariable("SYLVAINBRETON_DB_CONNECTION")
                                   ?? configuration["DatabaseSettings:ConnectionString"];
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new InvalidOperationException("Database connection string not found in environment variables.");
            }

            services.AddDbContext<SylvainBretonDbContext>(options =>
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString),
                    mySqlOptions => mySqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 5,
                        maxRetryDelay: TimeSpan.FromSeconds(30),
                        errorNumbersToAdd: null).CommandTimeout(60))
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking) // Improve performance by disabling tracking for retrieval operations
            );

            return services;
        }
    }

}
