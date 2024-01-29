using System.ComponentModel.DataAnnotations;

namespace api_sylvainbreton.Models
{
    public class Category
    {
        public int CategoryID { get; set; }

        [Required, MaxLength(255)]
        public string CategoryName { get; set; }

        // Collection navigation property
        public ICollection<Artwork> Artworks { get; set; }

        // Constructor to initialize the collection
        public Category()
        {
            Artworks = new HashSet<Artwork>();
        }
    }
}
