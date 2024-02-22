using System.ComponentModel.DataAnnotations;

namespace api_sylvainbreton.Models.DTOs
{
    public class ArtworkImageDTO
    {
        public int ArtworkID { get; set; }
        public int ImageID { get; set; }

        [Required, MaxLength(255)]
        public string FileName { get; set; }

        [MaxLength(1000)]
        public string FilePath { get; set; }
        
        [MaxLength(1000)]
        public string URL { get; set; }

        [Required(ErrorMessage = "Image data is required.")]
        public string ImageData { get; set; }
    }
}
