namespace api_sylvainbreton.Services
{
    using api_sylvainbreton.Models;
    using Microsoft.AspNetCore.Identity;
    using System.Threading.Tasks;

    public class AuthenticationService(UserManager<ApplicationUser> userManager, IConfiguration configuration)
    {
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly IConfiguration _configuration = configuration;

        public async Task<string> AuthenticateAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null && await _userManager.CheckPasswordAsync(user, password))
            {
                // Generate JWT Token
                return await new RolesManagementService(_userManager, _configuration).GenerateJwtToken(user);
            }

            return null;
        }
    }
}
