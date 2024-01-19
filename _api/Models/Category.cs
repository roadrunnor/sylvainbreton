namespace api_sylvainbreton.Models
{
    public class Category
    {
        public int CategoryID { get; set; }
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
