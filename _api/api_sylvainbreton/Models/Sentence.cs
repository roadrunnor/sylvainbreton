namespace api_sylvainbreton.Models
{
    public class Sentence
    {
        public int SentenceID { get; set; }
        public int ArtworkID { get; set; }
        public string Author { get; set; }
        public int? PublicationDate { get; set; }
        public string BookTitle { get; set; }
        public string Publisher { get; set; }
        public int SentencePage { get; set; }
        public string Content { get; set; }
        public string CountryOfPublication { get; set; }
        public string CityOfPublication { get; set; }

        // Relation avec Artwork
        // Propriétés de navigation
        public Artwork Artwork { get; set; }
    }



}
