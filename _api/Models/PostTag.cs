namespace api_sylvainbreton.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class PostTag
    {
        [Key]
        public int TagId { get; set; } // Primary key

        [Required]
        [MaxLength(50)]
        public string Name { get; set; } // Tag name

        public virtual ICollection<UserPostTag> UserPostTags { get; set; }
    }
}
