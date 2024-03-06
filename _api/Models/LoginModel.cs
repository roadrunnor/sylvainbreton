namespace api_sylvainbreton.Models
{
    using api_sylvainbreton.ValidationAttributes;
    using System.ComponentModel.DataAnnotations;

    public class LoginModel
    {
        [EmailPattern(ErrorMessage = "Email is not in a valid format.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(100, ErrorMessage = "The password must be at least {2} characters long.", MinimumLength = 8)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
