namespace api_sylvainbreton.Models
{
    public class EventArtwork
    {
        public int EventID { get; set; }
        public int ArtworkID { get; set; }

        // Relations avec Event et Artwork
        public Event Event { get; set; }
        public Artwork Artwork { get; set; }
    }

}


}
