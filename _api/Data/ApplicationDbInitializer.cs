namespace api_sylvainbreton.Data
{
    using Microsoft.AspNetCore.Identity;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public static class ApplicationDbInitializer
    {
        public static async Task SeedRoles(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            // List of roles to be seeded
            string[] roleNames = { "Admin", "User", "Editor", "Viewer" };

            foreach (var roleName in roleNames)
            {
                await SeedRole(roleManager, roleName);
            }
        }

        private static async Task SeedRole(RoleManager<IdentityRole> roleManager, string roleName)
        {
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                IdentityRole role = new IdentityRole(roleName);
                await roleManager.CreateAsync(role);
            }
        }
    }
}
