namespace api_sylvainbreton.Models
{
    using Microsoft.AspNetCore.Identity;

    public class ApplicationUser : IdentityUser
    {
        // An example of a custom property
        public string ProfilePictureUrl { get; set; }
        public DateTime DateOfBirth { get; set; }
        // Add any additional properties you need
    }
}
