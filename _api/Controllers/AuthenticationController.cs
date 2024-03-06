namespace api_sylvainbreton.Controllers
{
    using api_sylvainbreton.Models;
    using api_sylvainbreton.Services;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using System.Threading.Tasks;

    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController(AuthenticationService authenticationService, ILogger<AuthenticationController> logger) : ControllerBase
    {
        private readonly AuthenticationService _authenticationService = authenticationService;
        private readonly ILogger<AuthenticationController> _logger = logger;

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Login attempt with invalid model state.");
                return BadRequest(ModelState);
            }

            _logger.LogInformation("Login attempt for {Email}", loginModel.Email);

            var token = await _authenticationService.AuthenticateAsync(loginModel.Email, loginModel.Password);

            if (token != null)
            {
                _logger.LogInformation("Login successful for {Email}", loginModel.Email); 
                return Ok(new { Token = token });
            }

            _logger.LogWarning("Login failed for {Email}", loginModel.Email); 
            return Unauthorized("Invalid email or password.");
        }

    }
}
