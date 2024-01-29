using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace api_sylvainbreton.Models
{
    public class EventArtwork
    {
        [Required]
        public int EventID { get; set; }

        [Required]
        public int ArtworkID { get; set; }

        // Propriétés de navigation
        public Event Event { get; set; }
        public Artwork Artwork { get; set; }
    }

}
