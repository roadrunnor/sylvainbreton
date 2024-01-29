using System.ComponentModel.DataAnnotations;

namespace api_sylvainbreton.Models.DTOs
{
    public class ImageDTO
    {
        public int ImageID { get; set; }
        public int? ArtworkID { get; set; }
        public int? PerformanceID { get; set; }

        [Required, MaxLength(255)]
        public string FileName { get; set; }

        [MaxLength(1000)]
        public string FilePath { get; set; }

        [MaxLength (1000)]
        public string URL { get; set; }
    }
}