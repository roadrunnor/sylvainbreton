namespace api_sylvainbreton.ValidationAttributes
{
    using System.ComponentModel.DataAnnotations;
    using System.Text.RegularExpressions;

    public class Base64ImageListAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var imageList = value as List<string>;
            if (imageList != null)
            {
                foreach (var image in imageList)
                {
                    if (string.IsNullOrEmpty(image) || !IsBase64String(image))
                    {
                        return new ValidationResult("Each image must be a valid base64 string.");
                    }
                }
            }

            return ValidationResult.Success;
        }

        private bool IsBase64String(string s)
        {
            s = s.Trim();
            return (s.Length % 4 == 0) && Regex.IsMatch(s, @"^[a-zA-Z0-9\+/]*={0,3}$", RegexOptions.None);
        }
    }
}
