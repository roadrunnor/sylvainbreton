using System.ComponentModel.DataAnnotations;

namespace api_sylvainbreton.Models
{
    public class DynamicContent
    {
        [Key]
        public int ContentID { get; set; }
        public string Keyword { get; set; }
        public string Content { get; set; }
    }

}
