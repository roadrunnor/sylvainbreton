using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace api_sylvainbreton.Models.DTOs
{
    public class SentenceDTO
    {
        public int SentenceID { get; set; }
        public int? ArtworkID { get; set; }

        [Required]
        public string Author { get; set; }
        public DateTime PublicationDate { get; set; }


        [Required, MaxLength(255)]
        public string BookTitle { get; set; }

        [Required, MaxLength(255)]
        public string Publisher { get; set; }

        [Required]
        public int SentencePage { get; set; }

        [Required]
        public string Content { get; set; }

        [MaxLength(255)]
        public string CountryOfPublication { get; set; }

        [MinLength(255)]
        public string CityOfPublication { get; set; }

        // navigation props
        [JsonIgnore]
        public Artwork Artwork { get; set; }
    }
}
