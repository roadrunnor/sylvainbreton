using System.ComponentModel.DataAnnotations;

namespace api_sylvainbreton.Models
{
    public class ArtworkImage
    {
        [Required]
        public int ArtworkID { get; set; }
        [Required]
        public int ImageID { get; set; }        
        public Artwork Artwork { get; set; }
        public Image Image { get; set; }

    }
}
