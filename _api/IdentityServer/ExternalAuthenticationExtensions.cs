namespace api_sylvainbreton.IdentityServer
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public static class ExternalAuthenticationExtensions
    {
        public static IServiceCollection AddExternalAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication()
                .AddGoogle(options =>
                {
                    // Configuration values should ideally come from the configuration, e.g., appsettings.json or environment variables
                    options.ClientId = configuration["Authentication:Google:ClientId"];
                    options.ClientSecret = configuration["Authentication:Google:ClientSecret"];
                })
                .AddFacebook(options =>
                {
                    options.AppId = configuration["Authentication:Facebook:AppId"];
                    options.AppSecret = configuration["Authentication:Facebook:AppSecret"];
                })
                .AddMicrosoftAccount(options =>
                {
                    options.ClientId = configuration["Authentication:Microsoft:ClientId"];
                    options.ClientSecret = configuration["Authentication:Microsoft:ClientSecret"];
                });

            // You can add more external authentication services here

            return services;
        }
    }

}
