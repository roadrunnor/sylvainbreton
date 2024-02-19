namespace api_sylvainbreton.Controllers
{
    using api_sylvainbreton.Data;
    using api_sylvainbreton.Models;
    using api_sylvainbreton.Models.DTOs;
    using api_sylvainbreton.Services.Interfaces;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    [Route("api/[controller]")]
    [ApiController]
    public class ArtworksController : ControllerBase
    {
        private readonly SylvainBretonDbContext _context;
        private readonly ILogger<ArtworksController> _logger;
        private readonly ISanitizationService _sanitizationService;

        public ArtworksController(SylvainBretonDbContext context, ILogger<ArtworksController> logger, ISanitizationService sanitizationService)
        {
            _context = context;
            _logger = logger;
            _sanitizationService = sanitizationService;
        }

        // GET: api/Artworks
        [HttpGet]
        public ActionResult<IEnumerable<ArtworkDTO>> GetArtworks()
        {
            // Log the receipt of the GetArtworks request
            _logger.LogInformation("{ControllerName}: {ActionName} request received for all artworks",
                nameof(ArtistsController), nameof(GetArtwork));

            var artworks = _context.Artworks
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
                .ToList();

            return artworks;
        }

        // GET: api/Artworks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Artwork>> GetArtwork(int id)
        {
            _logger.LogInformation("{ControllerName}: {ActionName} request received for artwork ID {ArtworkId}",
                nameof(ArtworksController), nameof(GetArtwork), id);

            if (id <= 0)
            {
                return BadRequest("Invalid Artwork ID");
            }

            var artwork = await _context.Artworks
                .Include(a => a.ArtworkImages)
                    .ThenInclude(ai => ai.Image)
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.ArtworkID == id);
            if (artwork == null)
            {
                _logger.LogWarning("{ControllerName}: Artwork with ID {ArtworkId} not found in {ActionName}",
                    nameof(ArtworksController), id, nameof(GetArtwork));
                return NotFound();
            }

            return artwork;
        }

        // POST: api/Artworks
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<Artwork>> PostArtwork([FromBody] ArtworkDTO artworkDto)
        {
            _logger.LogInformation("{ControllerName}: {ActionName} request received for new artwork creation",
                nameof(ArtworksController), nameof(PostArtwork));

            artworkDto.Title = _sanitizationService.SanitizeInput(artworkDto.Title);
            artworkDto.CategoryName = _sanitizationService.SanitizeInput(artworkDto.CategoryName);
            artworkDto.Materials = _sanitizationService.SanitizeInput(artworkDto.Materials);
            artworkDto.Dimensions = _sanitizationService.SanitizeInput(artworkDto.Dimensions);
            artworkDto.Description = _sanitizationService.SanitizeInput(artworkDto.Description);
            artworkDto.Conceptual = _sanitizationService.SanitizeInput(artworkDto.Conceptual);

            var artwork = new Artwork
            {
                Title = artworkDto.Title,
                CreationDate = artworkDto.CreationDate,
                CategoryID = artworkDto.CategoryID,
                CategoryName = artworkDto.CategoryName,
                Materials = artworkDto.Materials,
                Dimensions = artworkDto.Dimensions,
                Description = artworkDto.Description,
                Conceptual = artworkDto.Conceptual,
            };

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _context.Artworks.Add(artwork);
                await _context.SaveChangesAsync();

                _logger.LogInformation("{ControllerName}: Artwork with ID {ArtworkId} {Action} successfully",
                    nameof(ArtworksController), artwork.ArtworkID, "created");
                return CreatedAtAction(nameof(GetArtwork), new { id = artwork.ArtworkID }, artwork);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError("{ControllerName}: Error processing {ActionName} for artwork ID {ArtworkId}: {ExceptionMessage}",
                    nameof(ArtworksController), nameof(PostArtwork), artwork.ArtworkID, ex.Message);
                return StatusCode(500, "An error occurred while creating the artwork. Please try again later.");
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
                return BadRequest("ArtworkID in URL does not match ArtworkID in request body");
            }

            try
            {
                var existingArtwork = await _context.Artworks.FindAsync(id);
                if (existingArtwork == null)
                {
                    _logger.LogWarning("{ControllerName}: Artwork with ID {ArtworkId} not found in {ActionName}",
                        nameof(ArtworksController), id, nameof(PutArtwork));
                    return NotFound();
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
                return StatusCode(500, "An error occurred while updating the artwork. Please try again later.");
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
                return StatusCode(500, "An error occurred while deleting the artwork. Please try again later.");
            }
        }

        private bool ArtworkExists(int id)
        {
            return _context.Artworks.Any(e => e.ArtworkID == id);
        }
    }
}