namespace api_sylvainbreton.Data
{
    using api_sylvainbreton.Models;
    using Microsoft.AspNetCore.Identity;
    using System;
    using System.Threading.Tasks;

    public static class ApplicationDbInitializer
    {
        public static async Task SeedRoles(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            // List of roles to be seeded
            string[] roleNames = ["Admin", "User", "Editor", "Viewer"];

            foreach (var roleName in roleNames)
            {
                await SeedRole(roleManager, roleName);
            }
        }

        private static async Task SeedRole(RoleManager<IdentityRole> roleManager, string roleName)
        {
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                IdentityRole role = new(roleName);
                await roleManager.CreateAsync(role);
            }
        }

        public static async Task SeedUsers(UserManager<ApplicationUser> userManager)
        {
            // Create a list of application to seed users 
            ApplicationUser adminUser = new()
            {
                UserName = "admin@sylvainbreton.com",
                Email = "admin@sylvainbreton.com",
                EmailConfirmed = true,
                // other properties
            };

            // Add the admin user if they don't exist
            if (userManager.FindByNameAsync(adminUser.UserName).Result == null)
            {
                var result = await userManager.CreateAsync(adminUser, "Password123!"); // Use a strong password in production
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }

            // Repeat for other users and roles
        }

        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            // Creating a new scope to retrieve scoped services
            using var scope = serviceProvider.CreateScope();
            var scopedServices = scope.ServiceProvider;
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            // Passing the scoped service provider to the SeedRoles method
            await SeedRoles(scopedServices);
            await SeedUsers(userManager);
        }
    }
}
