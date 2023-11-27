namespace api_sylvainbreton.Models
{
    public class Event
    {
        public int EventID { get; set; }
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int PlaceID { get; set; }
        public string Description { get; set; }

        // Relation avec Place
        public Place Place { get; set; }
    }


}
