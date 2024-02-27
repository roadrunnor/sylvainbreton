namespace api_sylvainbreton.ValidationAttributes
{
    using System.ComponentModel.DataAnnotations;
    using System.Text.RegularExpressions;

    public class EmailPatternAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var email = value as string;
            if (string.IsNullOrEmpty(email))
            {
                return new ValidationResult("Email is required.");
            }

            // Example of a simple email pattern check, customize as needed
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            if (Regex.IsMatch(email, pattern))
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("Email is not in a valid format.");
            }
        }
    }
}
