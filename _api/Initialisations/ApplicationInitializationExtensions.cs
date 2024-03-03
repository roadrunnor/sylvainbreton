// WebApplicationInitializationExtensions.cs
namespace api_sylvainbreton.Initialisations
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using api_sylvainbreton.Services;
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Hosting;

    public static class WebApplicationInitializationExtensions
    {
        public static async Task InitializeApplicationAsync(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var setupService = services.GetRequiredService<ApplicationSetupService>();
                    await setupService.InitializeAsync();
                    // Perform additional setup or initialization as needed
                }
                catch (Exception ex)
                {
                    // Note: Update ILogger<Program> to ILoggerFactory or ILogger<Startup> 
                    // based on where you wish to consume the logger.
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while initializing the application.");
                    // Handle initialization errors appropriately
                }
            }
        }
    }
}
