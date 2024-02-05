namespace api_sylvainbreton.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class UserPost
    {
        [Key]
        public int PostId { get; set; } // Primary key

        [Required]
        public string UserId { get; set; } // Foreign key referencing ApplicationUser

        [Required]
        [MaxLength(500)]
        public string Title { get; set; } // Post title

        [Required]
        public string Content { get; set; } // Post content

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // Post creation date

        // Properties for post status (e.g., Published, Draft)
        public string Status { get; set; }

        // Navigation property back to the ApplicationUser
        [ForeignKey("UserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }

        // Posts can be categorized or tagged, below are navigation properties
        public virtual ICollection<PostCategory> Categories { get; set; }
        public virtual ICollection<PostTag> Tags { get; set; }

        // For comments or replies on posts
        public virtual ICollection<UserComment> Comments { get; set; }
        public virtual ICollection<UserPostTag> UserPostTags { get; set; }
    }
}
