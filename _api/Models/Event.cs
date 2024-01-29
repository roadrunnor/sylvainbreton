using System.ComponentModel.DataAnnotations;

namespace api_sylvainbreton.Models
{
    public class Event
    {
        public int EventID { get; set; }

        [Required, MaxLength(255)]
        public string Title { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public int PlaceID { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }

        // Relation with Place
        public Place Place { get; set; }
        public ICollection<EventArtwork> EventArtworks { get; set; }
    }
}
