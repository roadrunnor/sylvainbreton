namespace api_sylvainbreton.Models.DTOs
{
    public class ArtworkImageDTO
    {
        public int ArtworkID { get; set; }
        public int ImageID { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string URL { get; set; }
    }
}
