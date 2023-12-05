namespace api_sylvainbreton.Models.DTOs
{
    public class SentenceDTO
    {
        public int SentenceID { get; set; }
        public string Content { get; set; }
        public int ArtworkID { get; set; } // Si vous avez besoin de faire référence à l'artwork
        // Autres propriétés publiques que vous souhaitez exposer
    }
}
