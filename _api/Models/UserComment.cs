namespace api_sylvainbreton.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class UserComment
    {
        [Key]
        public int CommentId { get; set; } // Primary key

        [Required]
        public string UserId { get; set; } // Foreign key for ApplicationUser

        [Required]
        public int PostId { get; set; } // Foreign key for UserPost

        [Required]
        public string Content { get; set; } // Comment content

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // Comment creation date

        [ForeignKey("UserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }

        [ForeignKey("PostId")]
        public virtual UserPost UserPost { get; set; }
    }
}
