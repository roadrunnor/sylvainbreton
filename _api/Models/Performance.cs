using System.ComponentModel.DataAnnotations;

namespace api_sylvainbreton.Models
{
    public class Performance
    {
        public int PerformanceID { get; set; }

        [Required, MaxLength(255)]
        public string Title { get; set; }

        [Required]
        public DateTime PerformanceDate { get; set; }

        [MaxLength(1000)]
        public string Materials { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }

        [Required]
        public int PlaceID { get; set; }

        // Propriétés de navigation
        public Place Place { get; set; }
        public ICollection<Image> Images { get; set; }
    }
}
