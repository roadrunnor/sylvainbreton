using System.ComponentModel.DataAnnotations;

namespace api_sylvainbreton.Models
{
    public class ArtworkImage
    {
        [Required]
        public int ArtworkID { get; set; }

        [Required]
        public int ImageID { get; set; }

        [Required, MaxLength(255)]
        public string FileName { get; set; }

        [Required, MaxLength(1000)]
        public string FilePath { get; set; }

        [Required, MaxLength(1000)]
        public string URL { get; set; }
        
        
        public Artwork Artwork { get; set; }
        public Image Image { get; set; }

    }
}
