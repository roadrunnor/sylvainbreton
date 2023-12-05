using System.Text.Json.Serialization;

namespace api_sylvainbreton.Models
{
    public class Artwork
    {
        public int ArtworkID { get; set; }
        public string Title { get; set; }
        public DateTime? CreationDate { get; set; }
        public string ArtworkType { get; set; }
        public string Materials { get; set; }
        public string Dimensions { get; set; }
        public string Description { get; set; }
        public string Conceptual { get; set; }

        // Propriétés de navigation
        public ICollection<Image> Images { get; set; }
        [JsonIgnore]
        public ICollection<Sentence> Sentences { get; set; }
        public ICollection<EventArtwork> EventArtworks { get; set; }
    }

}
