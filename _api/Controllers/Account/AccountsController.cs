namespace api_sylvainbreton.Controllers.Account
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using api_sylvainbreton.Models;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using api_sylvainbreton.Data;

    public class AccountsController(SylvainBretonDbContext sylvainBretonDbContext, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager) : ControllerBase
    {
        private readonly SylvainBretonDbContext _dbContext = sylvainBretonDbContext;
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly SignInManager<ApplicationUser> _signInManager = signInManager;

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                FullName = $"{model.FirstName} {model.LastName}", 
                CreatedAt = DateTime.UtcNow,
                DateOfBirth = model.DateOfBirth,
            };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                // Optionally sign the user in upon registration
                await _signInManager.SignInAsync(user, isPersistent: false);
                await _dbContext.SaveChangesAsync();
                return Ok(new { Message = "User registered successfully" });
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return BadRequest(ModelState);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, isPersistent: false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                return Ok(new { Message = "User logged in successfully" });
            }

            return BadRequest(new { Message = "Invalid login attempt" });
        }
    }
}
