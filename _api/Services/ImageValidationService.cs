namespace api_sylvainbreton.Services
{
    public class ImageValidationService
    {
        public bool IsValidImage(byte[] imageBytes)
        {
            var jpeg = new byte[] { 0xFF, 0xD8, 0xFF };
            var png = new byte[] { 0x89, 0x50, 0x4E, 0x47 };
            var gif = "GIF"u8.ToArray();
            var bmp = "BM"u8.ToArray();

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