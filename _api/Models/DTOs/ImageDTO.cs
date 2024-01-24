namespace api_sylvainbreton.Models.DTOs
{
    public class ImageDTO
    {
        public int ImageID { get; set; }
        public int? ArtworkID { get; set; }
        public int? PerformanceID { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string URL { get; set; }
        public string Description { get; set; }
        public string MediaType { get; set; }
        public string MediaDescription { get; set; }
    }
}