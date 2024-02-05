namespace api_sylvainbreton.Models
{
    using System.ComponentModel.DataAnnotations;

    public class PostCategory
    {
        [Key]
        public int CategoryId { get; set; } // Primary key

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } // Category name

        // Optional: Description for the category
        public string Description { get; set; }

        // Navigation property for related UserPosts
        public virtual ICollection<UserPost> UserPosts { get; set; }
    }
}
