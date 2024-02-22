namespace api_sylvainbreton.Services
{
    public class ImageValidationService
    {
        public bool IsValidImage(byte[] imageBytes)
        {
            var jpeg = new byte[] { 0xFF, 0xD8, 0xFF };
            var png = new byte[] { 0x89, 0x50, 0x4E, 0x47 };
            var gif = new byte[] { 0x47, 0x49, 0x46 };
            var bmp = new byte[] { 0x42, 0x4D };

            if (imageBytes.Take(3).SequenceEqual(jpeg) ||
                imageBytes.Take(4).SequenceEqual(png) ||
                imageBytes.Take(3).SequenceEqual(gif) ||
                imageBytes.Take(2).SequenceEqual(bmp))
            {
                return true;
            }

            return false;
        }
    }
}