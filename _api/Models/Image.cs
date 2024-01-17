namespace api_sylvainbreton.Models
{
    public class Image
    {
        public int ImageID { get; set; }
        public int? ArtworkID { get; set; }
        public int? PerformanceID { get; set; }
        public string FileName { get; set; }
        public string Description { get; set; }
        public string MediaType { get; set; }
        public string MediaDescription { get; set; }

        // Relations avec Artwork et Performance
        // Propriétés de navigation
        public Artwork Artwork { get; set; }
        public Performance Performance { get; set; }
    }


}
