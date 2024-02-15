namespace api_sylvainbreton.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("roles")]
    public partial class Role
    {
        [Key]
        public string RoleID { get; set; }

        [Required]
        [MaxLength(256)]
        public string RoleName { get; set; }

        [Required]
        [MaxLength(256)]
        public string RoleNormalizedName { get; set; }

        public string RoleConcurrencyStamp { get; set; }
    }
}
