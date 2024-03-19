namespace api_sylvainbreton.Services
{
    using api_sylvainbreton.Exceptions;
    using api_sylvainbreton.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public class ApplicationSetupService(IServiceProvider serviceProvider)
    {
        private readonly IServiceProvider _serviceProvider = serviceProvider;

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
                throw new InternalServerErrorException($"An error occurred during application setup: {ex.Message}");
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

        private static async Task SeedUsersAsync(UserManager<ApplicationUser> userManager)
        {
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
                if (!createUserResult.Succeeded)
                {
                    // Collect error descriptions
                    var errors = createUserResult.Errors.Select(e => e.Description).Aggregate((a, b) => a + "; " + b);
                    throw new InternalServerErrorException($"Failed to create the admin user: {errors}");
                }
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }
        }
    }
}
