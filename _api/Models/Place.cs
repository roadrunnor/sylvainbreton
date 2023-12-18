namespace api_sylvainbreton.Models
{
    public class Place
    {
        public int PlaceID { get; set; }
        public string Name { get; set; }
        public string PlaceType { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }

        // Propriétés de navigation
        public ICollection<Performance> Performances { get; set; }
        public ICollection<Event> Events { get; set; }
    }

}
