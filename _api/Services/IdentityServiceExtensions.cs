using api_sylvainbreton.Data;
using api_sylvainbreton.Models;
using Microsoft.AspNetCore.Identity;

namespace api_sylvainbreton.Services
{
    public static class IdentityServiceExtensions
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services)
        {
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

            return services;
        }
    }
}
