namespace api_sylvainbreton.Services.Utilities
{
    using SixLabors.ImageSharp;
    using SixLabors.ImageSharp.Processing;
    using System.IO;

    public class ImageProcessingService
    {
        public byte[] ProcessImage(byte[] imageBytes)
        {
            using var image = Image.Load(imageBytes);
            // Resize the image to a maximum acceptable size while maintaining aspect ratio
            image.Mutate(x => x.Resize(new ResizeOptions
            {
                Mode = ResizeMode.Max,
                Size = new Size(800, 600) // Example max size, adjust as needed
            }));

            using var ms = new MemoryStream();
            // Save the image back to a MemoryStream, and then to a byte array
            image.SaveAsJpeg(ms); // Here, assuming saving as JPEG; adjust as needed based on your requirements
            return ms.ToArray();
        }
    }
}
