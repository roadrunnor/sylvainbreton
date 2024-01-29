using System.ComponentModel.DataAnnotations;

namespace api_sylvainbreton.Models
{
    public class Place
    {
        public int PlaceID { get; set; }

        [Required, MaxLength(255)]
        public string Name { get; set; }

        [Required, MaxLength(50)]
        public string PlaceType { get; set; }

        [Required, MaxLength(255)]
        public string Address { get; set; }

        [Required, MaxLength(255)]
        public string Country { get; set; }

        // Propriétés de navigation
        public ICollection<Performance> Performances { get; set; }
        public ICollection<Event> Events { get; set; }
    }

}
