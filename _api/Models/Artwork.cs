using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace api_sylvainbreton.Models
{
    public class Artwork
    {
        public int ArtworkID { get; set; }
        public string Title { get; set; }
        public DateTime CreationDate { get; set; }

        [Required]
        public int CategoryID { get; set; }

        [Required, MaxLength(255)]
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
        public Category Category { get; set; }
        public ICollection<Image> Images { get; set; }
        [JsonIgnore]
        public ICollection<Sentence> Sentences { get; set; }
        public ICollection<ArtworkImage> ArtworkImages { get; set; }
        public ICollection<EventArtwork> EventArtworks { get; set; }
    }

}
