namespace api_sylvainbreton.Models.DTOs
{
    public class ArtworkDTO
    {
        public int ArtworkID { get; set; } 
        public string Title { get; set; }
        public DateTime? CreationDate { get; set; }
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string Materials { get; set; }
        public string Dimensions { get; set; }
        public string Description { get; set; }
        public string Conceptual { get; set; }

        // navigation props
        public List<ArtworkImageDTO> ArtworkImages { get; set; }

    }
}
