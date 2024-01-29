using System.ComponentModel.DataAnnotations;

namespace api_sylvainbreton.Models
{
    public class DynamicContent
    {
        [Key]
        public int ContentID { get; set; }

        [Required, MaxLength(255)]
        public string Keyword { get; set; }

        [Required]
        public string Content { get; set; }
    }

}
