namespace api_sylvainbreton.Models
{
    public class UserPostTag
    {
        public int PostId { get; set; }
        public virtual UserPost UserPost { get; set; }

        public int TagId { get; set; }
        public virtual PostTag PostTag { get; set; }
    }
}
