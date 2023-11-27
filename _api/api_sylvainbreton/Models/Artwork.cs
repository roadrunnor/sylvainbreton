namespace api_sylvainbreton.Models
{
    public class Artwork
    {
        public int ArtworkID { get; set; }
        public string Title { get; set; }
        public DateTime CreationDate { get; set; }
        public string ArtworkType { get; set; }
        public string Materials { get; set; }
        public string Dimensions { get; set; }
        public string Description { get; set; }
        public string Conceptual { get; set; }

        // Relations avec d'autres tables (si nécessaire)
    }

}
