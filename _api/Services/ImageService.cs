namespace api_sylvainbreton.Services
{
    using api_sylvainbreton.Data;
    using api_sylvainbreton.Models;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Hosting;
    using System;
    using System.IO;

    public class ImageService(IWebHostEnvironment env, IConfiguration configuration, SylvainBretonDbContext context, ImageProcessingService imageProcessingService)
    {
        private readonly IWebHostEnvironment _env = env;
        private readonly IConfiguration _configuration = configuration;
        private readonly SylvainBretonDbContext _context = context;
        private readonly ImageProcessingService _imageProcessingService = imageProcessingService;

        public async Task<Image> SaveImageAsync(byte[] imageBytes, string originalFileName, int? artworkId = null)
        {
            // Image processing for resizing and sanitization
            imageBytes = _imageProcessingService.ProcessImage(imageBytes);

            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            var filePath = GetFilePath(fileName);
            var urlPath = GetUrlPath(fileName);

            await File.WriteAllBytesAsync(filePath, imageBytes);

            var image = new Image
            {
                ArtworkID = artworkId,
                FileName = fileName,
                FilePath = filePath,
                URL = urlPath
            };

            _context.Images.Add(image);
            await _context.SaveChangesAsync();

            return image;
        }
        private string GetFilePath(string fileName)
        {
            if (_env.IsDevelopment())
            {
                var savePath = Path.Combine(_env.WebRootPath, "assets", "images");
                Directory.CreateDirectory(savePath); // It's safe even if the directory already exists
                return Path.Combine(savePath, fileName);
            }
            else
            {
                // Logic for determining filePath in production (e.g., temp storage before cloud upload)
                throw new NotImplementedException("Production filePath logic is not implemented.");
            }
        }

        private string GetUrlPath(string fileName)
        {
            return _env.IsDevelopment()
                ? $"{_configuration["AppSettings:DevelopmentBaseImageUrl"]}{fileName}"
                : UploadToCloudStorage(fileName); // Placeholder method for cloud storage
        }

        private string UploadToCloudStorage(string fileName)
        {
            // Implement cloud storage upload logic here
            throw new NotImplementedException("Cloud storage integration is not implemented.");
        }

        public int CreateImageRecordInDatabase(string imagePath)
        {
            // Create and save an image record in the database
            // Return the ID of the created image record
            var image = new Image { FilePath = imagePath };
            _context.Images.Add(image);
            _context.SaveChanges();

            return image.ImageID;
        }
    }
}
