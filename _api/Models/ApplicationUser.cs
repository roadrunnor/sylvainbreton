namespace api_sylvainbreton.Models
{
    using Microsoft.AspNetCore.Identity;
    using System;
    using System.Collections.Generic;

    public class ApplicationUser : IdentityUser
    {
        // Existing properties
        public string ProfilePictureUrl { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string FullName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsActive { get; set; } = true; // To enable or disable user accounts
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // User creation date

        public virtual ICollection<UserPost> UserPosts { get; set; }
    }
}
