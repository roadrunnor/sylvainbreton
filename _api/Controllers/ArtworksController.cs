namespace api_sylvainbreton.Controllers
{
    using api_sylvainbreton.Data;
    using api_sylvainbreton.Models;
    using api_sylvainbreton.Models.DTOs;
    using api_sylvainbreton.Services;
    using api_sylvainbreton.Services.Interfaces;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using static api_sylvainbreton.Exceptions.Exceptions;

    [Route("api/[controller]")]
    [ApiController]
    public class ArtworksController : ControllerBase
    {
        private readonly SylvainBretonDbContext _context;
        private readonly ILogger<ArtworksController> _logger;
        private readonly ISanitizationService _sanitizationService;
        private readonly ImageService _imageService;
        private readonly ImageValidationService _imageValidationService;
        private readonly IArtworkService _artworkService;

        public ArtworksController(SylvainBretonDbContext context, ILogger<ArtworksController> logger, ISanitizationService sanitizationService, ImageService imageService, ImageValidationService imageValidationService, IArtworkService artworkService)
        {
            _context = context;
            _logger = logger;
            _sanitizationService = sanitizationService;
            _imageService = imageService;
            _imageValidationService = imageValidationService;
            _artworkService = artworkService;
        }

        // GET: api/Artworks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ArtworkDTO>>> GetArtworks()
        {
            _logger.LogInformation("{ControllerName}: {ActionName} request received for all artworks",
                nameof(ArtworksController), nameof(GetArtworks));

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

                return Ok(artworks);
            }
            catch (Exception ex)
            {
                _logger.LogError("{ControllerName}: Error processing {ActionName}: {ExceptionMessage}",
                    nameof(ArtworksController), nameof(GetArtworks), ex.Message);
                throw new InternalServerErrorException("An error occurred while retrieving artworks. Please try again later.");
            }
        }

        // GET: api/Artworks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ArtworkDTO>> GetArtwork(int id)
        {
            _logger.LogInformation("{ControllerName}: {ActionName} request received for artwork ID {ArtworkId}",
                nameof(ArtworksController), nameof(GetArtwork), id);

            if (id <= 0)
            {
                throw new BadRequestException("Invalid Artwork ID");
            }

            try
            {
                var artwork = await _context.Artworks
                    .Include(a => a.ArtworkImages)
                        .ThenInclude(ai => ai.Image)
                    .AsNoTracking()
                    .Select(a => new ArtworkDTO { /* Mapping properties */ })
                    .FirstOrDefaultAsync(a => a.ArtworkID == id);

                if (artwork == null)
                {
                    _logger.LogWarning("{ControllerName}: Artwork with ID {ArtworkId} not found in {ActionName}",
                        nameof(ArtworksController), id, nameof(GetArtwork));
                    throw new NotFoundException($"Artwork with ID {id} not found");
                }

                return Ok(artwork);
            }
            catch (Exception ex)
            {
                _logger.LogError("{ControllerName}: Error processing {ActionName} for artwork ID {ArtworkId}: {ExceptionMessage}",
                    nameof(ArtworksController), nameof(GetArtwork), id, ex.Message);
                throw new InternalServerErrorException($"An error occurred while retrieving the artwork with ID {id}. Please try again later.");
            }
        }

        // POST: api/Artworks
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<ArtworkDTO>> PostArtwork([FromBody] ArtworkDTO artworkDTO)
        {
            _logger.LogInformation("{ControllerName}: {ActionName} request received for new artwork creation",
                nameof(ArtworksController), nameof(PostArtwork));

            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
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
                    ArtworkImages = new List<ArtworkImage>()
                };

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                _context.Artworks.Add(artwork);
                await _context.SaveChangesAsync();

                var artworkImages = new List<ArtworkImageDTO>();
                foreach (var imageData in artworkDTO.ImageData)
                {
                    var imageBytes = Convert.FromBase64String(imageData);

                    // Limit image size (e.g., 5MB)
                    if (imageBytes.Length > 5_000_000)
                    {
                        throw new BadRequestException("Image size exceeds the maximum allowed limit.");
                    }

                    // Verify image content (MIME type or magic bytes) - Placeholder
                    if (!_imageValidationService.IsValidImage(imageBytes))
                    {
                        throw new BadRequestException("One or more files are not valid images.");
                    }

                    var image = await _imageService.SaveImageAsync(imageBytes, "image.jpg", artwork.ArtworkID);

                    var artworkImageDTO = new ArtworkImageDTO
                    {
                        ArtworkID = artwork.ArtworkID,
                        ImageID = image.ImageID,
                        FileName = image.FileName,
                        FilePath = image.FilePath,
                        URL = image.URL
                    };
                    artworkImages.Add(artworkImageDTO);

                    _context.ArtworkImages.Add(new ArtworkImage { ArtworkID = artwork.ArtworkID, ImageID = image.ImageID });
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                var savedArtworkDTO = new ArtworkDTO
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
                    ArtworkImages = artworkImages
                };

                _logger.LogInformation("{ControllerName}: Artwork with ID {ArtworkId} {Action} successfully",
                    nameof(ArtworksController), artwork.ArtworkID, "created");
                return CreatedAtAction(nameof(GetArtwork), new { id = artwork.ArtworkID }, savedArtworkDTO);
            }
            catch (DbUpdateException ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError("{ControllerName}: Error processing {ActionName} for new artwork: {ExceptionMessage}",
                    nameof(ArtworksController), nameof(PostArtwork), ex.Message);
                throw new InternalServerErrorException("An error occurred while creating the artwork. Please try again later.");
            }
        }

        // PUT: api/Artworks/5
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutArtwork(int id, [FromBody] ArtworkDTO artworkDto)
        {
            _logger.LogInformation("{ControllerName}: {ActionName} request received for artwork ID {ArtworkId}",
                nameof(ArtworksController), nameof(PutArtwork), id);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != artworkDto.ArtworkID)
            {
                throw new BadRequestException("ArtworkID in URL does not match ArtworkID in request body");
            }

            try
            {
                var existingArtwork = await _context.Artworks.FindAsync(id);
                if (existingArtwork == null)
                {
                    _logger.LogWarning("{ControllerName}: Artwork with ID {ArtworkId} not found in {ActionName}",
                        nameof(ArtworksController), id, nameof(PutArtwork));
                    throw new NotFoundException($"Artist with ID {id} not found");
                }

                artworkDto.Title = _sanitizationService.SanitizeInput(artworkDto.Title);
                artworkDto.CategoryName = _sanitizationService.SanitizeInput(artworkDto.CategoryName);
                artworkDto.Materials = _sanitizationService.SanitizeInput(artworkDto.Materials);
                artworkDto.Dimensions = _sanitizationService.SanitizeInput(artworkDto.Dimensions);
                artworkDto.Description = _sanitizationService.SanitizeInput(artworkDto.Description);
                artworkDto.Conceptual = _sanitizationService.SanitizeInput(artworkDto.Conceptual);

                existingArtwork.Title = artworkDto.Title;
                existingArtwork.CreationDate = artworkDto.CreationDate;
                existingArtwork.CategoryID = artworkDto.CategoryID;
                existingArtwork.CategoryName = artworkDto.CategoryName;
                existingArtwork.Materials = artworkDto.Materials;
                existingArtwork.Dimensions = artworkDto.Dimensions;
                existingArtwork.Description = artworkDto.Description;
                existingArtwork.Conceptual = artworkDto.Conceptual;

                if (existingArtwork.Title.Length > 255)
                {
                    ModelState.AddModelError(nameof(existingArtwork.Title), "Title cannot be longer than 255 characters.");
                    return BadRequest(ModelState);
                }

                _context.Update(existingArtwork);
                await _context.SaveChangesAsync();

                _logger.LogInformation("{ControllerName}: Artwork with ID {ArtworkId} {Action} successfully",
                    nameof(ArtworksController), existingArtwork.ArtworkID, "updated");
                return NoContent();
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError("{ControllerName}: Error processing {ActionName} for artwork ID {ArtworkId}: {ExceptionMessage}",
                    nameof(ArtworksController), nameof(PutArtwork), artworkDto.ArtworkID, ex.Message);
                throw new InternalServerErrorException("An error occurred while updating the artwork. Please try again later.");
            }
        }

        // DELETE: api/Artworks/5
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArtwork(int id)
        {
            _logger.LogInformation("{ControllerName}: Delete request received for artwork ID {ArtworkId}",
                nameof(ArtworksController), id);

            try
            {
                var artwork = await _context.Artworks.FindAsync(id);
                if (artwork == null)
                {
                    _logger.LogWarning("{ControllerName}: Artwork with ID {ArtworkId} not found",
                        nameof(ArtworksController), id);
                    return NotFound();
                }

                _context.Artworks.Remove(artwork);
                await _context.SaveChangesAsync();

                _logger.LogInformation("{ControllerName}: Artwork with ID {ArtworkId} deleted successfully",
                    nameof(ArtworksController), id);
                return NoContent();
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError("{ControllerName}: Error processing {ActionName} for artwork ID {ArtworkId}: {ExceptionMessage}",
                    nameof(ArtworksController), nameof(DeleteArtwork), id, ex.Message);
                throw new InternalServerErrorException("An error occurred while deleting the artwork. Please try again later.");
            }
        }

        private bool ArtworkExists(int id)
        {
            return _context.Artworks.Any(e => e.ArtworkID == id);
        }
    }
}