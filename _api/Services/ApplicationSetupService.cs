namespace api_sylvainbreton.Services
{
    using api_sylvainbreton.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Threading.Tasks;

    public class ApplicationSetupService(IServiceProvider serviceProvider, ILogger<ApplicationSetupService> logger)
    {
        private readonly IServiceProvider _serviceProvider = serviceProvider;
        private readonly ILogger<ApplicationSetupService> _logger = logger;

        public async Task InitializeAsync()
        {
            try
            {
                using var scope = _serviceProvider.CreateScope();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                await SeedRolesAsync(roleManager);
                await SeedUsersAsync(userManager);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during application setup.");
            }
        }

        private static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            string[] roleNames = ["Admin", "User", "Editor", "Viewer"];
            foreach (var roleName in roleNames)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }
        }

        private async Task SeedUsersAsync(UserManager<ApplicationUser> userManager)
        {
            // Read from environment variables
            var adminUserName = Environment.GetEnvironmentVariable("ADMIN_USER_NAME");
            var adminEmail = Environment.GetEnvironmentVariable("ADMIN_USER_EMAIL");
            var adminPassword = Environment.GetEnvironmentVariable("ADMIN_USER_PASSWORD");

            if (await userManager.FindByNameAsync(adminUserName) == null)
            {
                var adminUser = new ApplicationUser
                {
                    UserName = adminUserName,
                    Email = adminEmail,
                    EmailConfirmed = true
                };

                var createUserResult = await userManager.CreateAsync(adminUser, adminPassword);
                if (createUserResult.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
                else
                {
                    var errors = createUserResult.Errors.Select(e => e.Description).ToArray();
                    _logger.LogError("Failed to create the admin user: {Errors}", string.Join(", ", errors));
                }
            }
        }
    }
}
