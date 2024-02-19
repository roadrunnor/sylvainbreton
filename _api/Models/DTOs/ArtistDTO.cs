using System.ComponentModel.DataAnnotations;

namespace api_sylvainbreton.Models.DTOs
{
    public class ArtistDTO
    {
        [Key]
        public int ArtistID { get; set; }

        [Required, MaxLength(100)]
        public string FirstName { get; set; }

        [Required, MaxLength(100)]
        public string LastName { get; set; }

        [Required, MinLength(10)]
        public string Bio { get; set; }
    }
}
