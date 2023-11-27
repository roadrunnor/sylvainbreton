namespace api_sylvainbreton.Models
{
    public class Performance
    {
        public int PerformanceID { get; set; }
        public string Title { get; set; }
        public DateTime PerformanceDate { get; set; }
        public string Materials { get; set; }
        public string Description { get; set; }
        public int PlaceID { get; set; }

        // Relation avec Place
        public Place Place { get; set; }
    }

}
