namespace api_sylvainbreton.Models
{
    public class ArtworkImage
    {
        public int ArtworkID { get; set; }
        public int ImageID { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string URL { get; set; }
        public Artwork Artwork { get; set; }
        public Image Image { get; set; }

    }
}
