namespace api_sylvainbreton.Extensions
{
    using api_sylvainbreton.Data;
    using api_sylvainbreton.Models;
    using api_sylvainbreton.Services;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.IdentityModel.Tokens;
    using System.Collections;
    using System.Text;

    public static class IdentityServiceExtensions
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration configuration)
        {
            string jwtKey = GetConfigurationValue(configuration, "JwtConfig:JwtKey", "JwtKey");
            string jwtIssuer = GetConfigurationValue(configuration, "JwtConfig:JwtIssuer", "JwtIssuer");
            string jwtAudience = GetConfigurationValue(configuration, "JwtConfig:JwtAudience", "JwtAudience");

            // Example debugging code to log all environment variables
            foreach (var envVar in Environment.GetEnvironmentVariables().Cast<DictionaryEntry>())
            {
                Console.WriteLine($"{envVar.Key} = {envVar.Value}");
            }

            // Adjusted DbContext configuration
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
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;

                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings
                options.User.RequireUniqueEmail = true;

                // Sign-in settings
                options.SignIn.RequireConfirmedEmail = true;
                options.SignIn.RequireConfirmedAccount = true;

                // Enable 2FA - This is usually a user option rather than a global setting
                options.Tokens.AuthenticatorTokenProvider = "YourCustomTokenProvider"; // If you're implementing a custom 2FA token provider

            })
                    .AddEntityFrameworkStores<SylvainBretonDbContext>()
                    .AddDefaultUI()
                    .AddDefaultTokenProviders();

            // JWT Authentication
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => ConfigureJwtBearerOptions(options, jwtKey, jwtIssuer, jwtAudience));

            // Register the RolesManagementService for dependency injection
            services.AddScoped<RolesManagementService>();

            return services;
        }

        private static void ConfigureJwtBearerOptions(JwtBearerOptions options, string jwtKey, string jwtIssuer, string jwtAudience)
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
                ValidateIssuer = true,
                ValidIssuer = jwtIssuer,
                ValidateAudience = true,
                ValidAudience = jwtAudience,
                RequireExpirationTime = false,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };
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
