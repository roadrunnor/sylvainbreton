namespace api_sylvainbreton.Services.Implementations
{
    using api_sylvainbreton.Data;
    using api_sylvainbreton.Models;
    using api_sylvainbreton.Models.DTOs;
    using api_sylvainbreton.Services.Interfaces;
    using api_sylvainbreton.Services.Utilities;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class ArtworkService(SylvainBretonDbContext context, ISanitizationService sanitizationService, ImageValidationService imageValidationService, ImageService imageService) : IArtworkService
    {
        private readonly SylvainBretonDbContext _context = context;
        private readonly ISanitizationService _sanitizationService = sanitizationService;
        private readonly ImageValidationService _imageValidationService = imageValidationService;
        private readonly ImageService _imageService = imageService;

        public async Task<IServiceResult<IEnumerable<ArtworkDTO>>> GetAllArtworksAsync()
        {
            try
            {
                var artworks = await _context.Artworks
                    .Include(a => a.ArtworkImages)
                    .ThenInclude(ai => ai.Image)
                    .Select(a => new ArtworkDTO
                    {
                        ArtworkID = a.ArtworkID,
                        Title = a.Title,
                        CreationDate = a.CreationDate,
                        CategoryID = a.CategoryID,
                        CategoryName = a.CategoryName,
                        Materials = a.Materials,
                        Dimensions = a.Dimensions,
                        Description = a.Description,
                        Conceptual = a.Conceptual,
                        ArtworkImages = a.ArtworkImages.Select(ai => new ArtworkImageDTO
                        {
                            ArtworkID = ai.ArtworkID,
                            ImageID = ai.ImageID,
                            FileName = ai.Image.FileName,
                            FilePath = ai.Image.FilePath,
                            URL = ai.Image.URL
                        }).ToList()
                    })
                    .AsNoTracking()
                    .ToListAsync();

                return new ServiceResult<IEnumerable<ArtworkDTO>>(artworks);
            }
            catch (Exception)
            {
                return new ServiceResult<IEnumerable<ArtworkDTO>>(false, default, "An error occurred while retrieving artworks. Please try again later.", 500);
            }
        }

        public async Task<IServiceResult<ArtworkDTO>> GetArtworkByIdAsync(int id)
        {
            try
            {
                var artwork = await _context.Artworks
                    .Include(a => a.ArtworkImages)
                    .ThenInclude(ai => ai.Image)
                    .Select(a => new ArtworkDTO
                    {
                        ArtworkID = a.ArtworkID,
                        Title = a.Title,
                        CreationDate = a.CreationDate,
                        CategoryID = a.CategoryID,
                        CategoryName = a.CategoryName,
                        Materials = a.Materials,
                        Dimensions = a.Dimensions,
                        Description = a.Description,
                        Conceptual = a.Conceptual,
                        ArtworkImages = a.ArtworkImages.Select(ai => new ArtworkImageDTO
                        {
                            ArtworkID = ai.ArtworkID,
                            ImageID = ai.ImageID,
                            FileName = ai.Image.FileName,
                            FilePath = ai.Image.FilePath,
                            URL = ai.Image.URL
                        }).ToList()
                    })
                    .AsNoTracking()
                    .FirstOrDefaultAsync(a => a.ArtworkID == id);

                if (artwork == null)
                {
                    return new ServiceResult<ArtworkDTO>(false, null, "Artwork not found.", 404);
                }

                return new ServiceResult<ArtworkDTO>(artwork);
            }
            catch (Exception)
            {
                return new ServiceResult<ArtworkDTO>(false, null, "An error occurred while retrieving the artwork. Please try again later.", 500);
            }
        }

        public async Task<IServiceResult<ArtworkDTO>> CreateArtworkAsync(ArtworkDTO artworkDTO)
        {
            try
            {
                var artworkImages = new List<ArtworkImageDTO>();
                foreach (var imageData in artworkDTO.ImageData)
                {
                    var imageBytes = Convert.FromBase64String(imageData);

                    if (imageBytes.Length > 5_000_000)
                    {
                        return new ServiceResult<ArtworkDTO>("Image size exceeds the maximum allowed limit.", 400);
                    }

                    if (!_imageValidationService.IsValidImage(imageBytes))
                    {
                        return new ServiceResult<ArtworkDTO>("One or more files are not valid images.", 400);
                    }

                    var image = await _imageService.SaveImageAsync(imageBytes, "image.jpg");

                    var artworkImageDTO = new ArtworkImageDTO
                    {
                        ArtworkID = artworkDTO.ArtworkID,
                        ImageID = image.ImageID,
                        FileName = image.FileName,
                        FilePath = image.FilePath,
                        URL = image.URL
                    };
                    artworkImages.Add(artworkImageDTO);
                }

                var artwork = new Artwork
                {
                    Title = _sanitizationService.SanitizeInput(artworkDTO.Title),
                    CreationDate = artworkDTO.CreationDate,
                    CategoryID = artworkDTO.CategoryID,
                    CategoryName = _sanitizationService.SanitizeInput(artworkDTO.CategoryName),
                    Materials = _sanitizationService.SanitizeInput(artworkDTO.Materials),
                    Dimensions = _sanitizationService.SanitizeInput(artworkDTO.Dimensions),
                    Description = _sanitizationService.SanitizeInput(artworkDTO.Description),
                    Conceptual = _sanitizationService.SanitizeInput(artworkDTO.Conceptual),
                    ArtworkImages = artworkDTO.ArtworkImages.Select(ai => new ArtworkImage
                    {
                        ImageID = ai.ImageID,
                        ArtworkID = ai.ArtworkID,

                        Image = new Image
                        {
                            FileName = ai.FileName,
                            FilePath = ai.FilePath,
                            URL = ai.URL
                        }
                    }).ToList()
                };

                _context.Artworks.Add(artwork);
                await _context.SaveChangesAsync();

                var createdArtworkDTO = new ArtworkDTO
                {
                    ArtworkID = artwork.ArtworkID,
                    Title = artwork.Title,
                    CreationDate = artwork.CreationDate,
                    CategoryID = artwork.CategoryID,
                    CategoryName = artwork.CategoryName,
                    Materials = artwork.Materials,
                    Dimensions = artwork.Dimensions,
                    Description = artwork.Description,
                    Conceptual = artwork.Conceptual,
                    ArtworkImages = artwork.ArtworkImages.Select(ai => new ArtworkImageDTO
                    {
                        ArtworkID = ai.ArtworkID,
                        ImageID = ai.ImageID,
                        FileName = ai.Image.FileName,
                        FilePath = ai.Image.FilePath,
                        URL = ai.Image.URL
                    }).ToList()
                };

                return new ServiceResult<ArtworkDTO>(createdArtworkDTO);
            }
            catch (Exception)
            {
                return new ServiceResult<ArtworkDTO>(false, null, "An error occurred while creating the artwork.", 500);
            }
        }

        public async Task<IServiceResult<ArtworkDTO>> UpdateArtworkAsync(int id, ArtworkDTO artworkDTO)
        {
            try
            {
                var artwork = await _context.Artworks
                    .Include(a => a.ArtworkImages)
                    .ThenInclude(ai => ai.Image)
                    .FirstOrDefaultAsync(a => a.ArtworkID == id);

                if (artwork == null)
                {
                    return new ServiceResult<ArtworkDTO>(false, null, "Artwork not found.", 404);
                }

                // Sanitize and map DTO to entity
                artwork.Title = _sanitizationService.SanitizeInput(artworkDTO.Title);
                artwork.CreationDate = artworkDTO.CreationDate;
                artwork.CategoryID = artworkDTO.CategoryID;
                artwork.CategoryName = _sanitizationService.SanitizeInput(artworkDTO.CategoryName);
                artwork.Materials = _sanitizationService.SanitizeInput(artworkDTO.Materials);
                artwork.Dimensions = _sanitizationService.SanitizeInput(artworkDTO.Dimensions);
                artwork.Description = _sanitizationService.SanitizeInput(artworkDTO.Description);
                artwork.Conceptual = _sanitizationService.SanitizeInput(artworkDTO.Conceptual);

                // Image handling: compare, update, or add new images
                var existingImageIds = artwork.ArtworkImages.Select(ai => ai.ImageID).ToList();
                var updatedImageIds = new List<int>();

                foreach (var imageData in artworkDTO.ArtworkImages)
                {
                    var imageBytes = Convert.FromBase64String(imageData.ImageData);
                    if (!_imageValidationService.IsValidImage(imageBytes))
                    {
                        return new ServiceResult<ArtworkDTO>(false, null, "Invalid image data.", 400);
                    }

                    Image image;
                    // Check if the image already exists
                    if (imageData.ImageID != 0 && existingImageIds.Contains(imageData.ImageID))
                    {
                        // Update existing image data 
                        image = await _context.Images.FindAsync(imageData.ImageID);
                        updatedImageIds.Add(imageData.ImageID);
                    }
                    else
                    {
                        image = await _imageService.SaveImageAsync(imageBytes, imageData.FileName, artwork.ArtworkID);
                        artwork.ArtworkImages.Add(new ArtworkImage { ArtworkID = artwork.ArtworkID, ImageID = image.ImageID });
                    }
                }

                // Remove images that were not included in the updated list
                var imagesToRemove = artwork.ArtworkImages.Where(ai => !updatedImageIds.Contains(ai.ImageID)).ToList();
                foreach (var imageToRemove in imagesToRemove)
                {
                    _context.ArtworkImages.Remove(imageToRemove);
                }

                _context.Update(artwork);
                await _context.SaveChangesAsync();

                return new ServiceResult<ArtworkDTO>(true, null, "Artwork updated successfully.", 200);
            }
            catch (Exception ex)
            {
                return new ServiceResult<ArtworkDTO>(false, null, $"An error occurred: {ex.Message}", 500);
            }
        }

        public async Task<IServiceResult<ArtworkDTO>> DeleteArtworkAsync(int id)
        {
            var artwork = await _context.Artworks.Include(a => a.ArtworkImages).FirstOrDefaultAsync(a => a.ArtworkID == id);
            if (artwork == null)
            {
                return new ServiceResult<ArtworkDTO>(false, null, "Artwork not found.", 404);
            }

            try
            {
                _context.Artworks.Remove(artwork);
                await _context.SaveChangesAsync();
                return new ServiceResult<ArtworkDTO>(true, null, "Artwork deleted successfully.", 200);
            }
            catch (Exception ex)
            {
                return new ServiceResult<ArtworkDTO>(false, null, $"An error occurred while deleting the artwork: {ex.Message}", 500);
            }
        }

    }
}
