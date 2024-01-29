using System.ComponentModel.DataAnnotations;

namespace api_sylvainbreton.Models.DTOs
{
    public class ArtworkDTO
    {
        public int ArtworkID { get; set; }

        [Required, MaxLength(255)]
        public string Title { get; set; }
        public DateTime CreationDate { get; set; }

        [Required]
        public int CategoryID { get; set; }

        [MaxLength(255)]
        public string CategoryName { get; set; }

        [MaxLength(500)]
        public string Materials { get; set; }

        [MaxLength(100)]
        public string Dimensions { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }

        [MaxLength(255)]
        public string Conceptual { get; set; }

        // navigation props
        public List<ArtworkImageDTO> ArtworkImages { get; set; }

    }
}
