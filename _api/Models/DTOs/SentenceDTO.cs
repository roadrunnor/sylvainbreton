using System.Text.Json.Serialization;

namespace api_sylvainbreton.Models.DTOs
{
    public class SentenceDTO
    {
        public int SentenceID { get; set; }
        public int? ArtworkID { get; set; }
        public string Author { get; set; }
        public DateTime? PublicationDate { get; set; }
        public string BookTitle { get; set; }
        public string Publisher { get; set; }
        public int SentencePage { get; set; }
        public string Content { get; set; }
        public string CountryOfPublication { get; set; }
        public string CityOfPublication { get; set; }


        // navigation props
        [JsonIgnore]
        public Artwork Artwork { get; set; }
    }
}
