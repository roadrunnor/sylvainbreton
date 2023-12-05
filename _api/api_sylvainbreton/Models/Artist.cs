using System.ComponentModel.DataAnnotations;

namespace api_sylvainbreton.Models
{
    public class Artist
    {
        [Key]
        public int ArtistID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

}
