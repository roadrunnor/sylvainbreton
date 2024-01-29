using System.ComponentModel.DataAnnotations;

namespace api_sylvainbreton.Models
{
    public class Image
    {
        public int ImageID { get; set; }
        public int? ArtworkID { get; set; }
        public int? PerformanceID { get; set; }

        [Required, MaxLength(255)]
        public string FileName { get; set; }

        [Required, MaxLength(1000)]
        public string FilePath { get; set; }

        [Required, MaxLength(1000)]
        public string URL { get; set; }



        public Artwork Artwork { get; set; }
        public Performance Performance { get; set; }
        public ICollection<ArtworkImage> ArtworkImages { get; set; }
    }
}
