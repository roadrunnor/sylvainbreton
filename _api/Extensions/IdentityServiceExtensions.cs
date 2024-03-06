namespace api_sylvainbreton.Extensions
{
    using api_sylvainbreton.Data;
    using api_sylvainbreton.Models;
    using api_sylvainbreton.Services;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

    public static class IdentityServiceExtensions
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration configuration)
        {
            string jwtKey = GetConfigurationValue(configuration, "JwtConfig:JwtKey", "JWT_KEY");
            string jwtIssuer = GetConfigurationValue(configuration, "JwtConfig:JwtIssuer", "JWT_ISSUER");
            string jwtAudience = GetConfigurationValue(configuration, "JwtConfig:JwtAudience", "JWT_AUDIENCE");

            var connectionString = Environment.GetEnvironmentVariable("SYLVAINBRETON_DB_CONNECTION");
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("Connection 'SYLVAINBRETON_DB_CONNECTION' not found in environment variables.");
            }

            services.AddDbContext<SylvainBretonDbContext>(options =>
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
            );

            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                // Password settings
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;

                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings
                options.User.RequireUniqueEmail = true;
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+ ";

                // Sign-in settings
                options.SignIn.RequireConfirmedEmail = true;
                options.SignIn.RequireConfirmedAccount = true;

                // Enable 2FA - This is usually a user option rather than a global setting
                options.Tokens.AuthenticatorTokenProvider = "YourCustomTokenProvider"; // If you're implementing a custom 2FA token provider

            })
                    .AddEntityFrameworkStores<SylvainBretonDbContext>()
                    .AddDefaultUI()
                    .AddDefaultTokenProviders();

            // Register the RolesManagementService for dependency injection
            services.AddScoped<RolesManagementService>();

            return services;
        }

        private static string GetConfigurationValue(IConfiguration configuration, string paramName, string configKey)
            {
            var value = configuration[configKey];
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException(paramName, $"Configuration value for '{configKey}' is required but was not found.");

            return value;
        }
    }
}
