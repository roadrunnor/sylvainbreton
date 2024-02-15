namespace api_sylvainbreton.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using api_sylvainbreton.Models;
    using api_sylvainbreton.Models.DTOs;
    using api_sylvainbreton.Data;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.Extensions.Logging;

    [Route("api/[controller]")]
    [ApiController]
    public class ArtworksController : ControllerBase
    {
        private readonly SylvainBretonDbContext _context;
        private readonly ILogger<ArtworksController> _logger;

        private const string Log_RequestReceived = "Request received for {ActionName}";
        private const string Log_RequestReceivedWithId = "{ActionName} request for id {Id} received";
        private const string Log_RequestNotFound = "{ActionName} request for id {Id} not found";
        private const string Log_ProcessingError = "Error processing {ActionName} request for id {Id}";
        private const string Log_RequestCreated = "Artwork with id {ArtworkId} created successfully";
        private const string Log_RequestUpdated = "Artwork with id {ArtworkId} updated successfully";
        private const string Log_RequestDeleted = "Artwork with id {ArtworkId} deleted successfully";

        public ArtworksController(SylvainBretonDbContext context, ILogger<ArtworksController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/Artworks
        [HttpGet]
        public ActionResult<IEnumerable<ArtworkDTO>> GetArtworks()
        {
            _logger.LogInformation(Log_RequestReceived, nameof(GetArtworks));
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
        public async Task<ActionResult<ArtworkDTO>> GetArtwork(int id)
        {
            _logger.LogInformation(Log_RequestReceivedWithId, nameof(GetArtwork), id);
            var artwork = await _context.Artworks
                .Include(a => a.ArtworkImages)
                    .ThenInclude(ai => ai.Image)
                .Where(a => a.ArtworkID == id)
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
                .FirstOrDefaultAsync();

            if (artwork == null)
            {
                _logger.LogInformation(Log_RequestNotFound, nameof(GetArtwork), id);
                return NotFound();
            }

            return artwork;
        }

        // POST: api/Artworks
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<ArtworkDTO>> PostArtwork([FromBody] ArtworkDTO artworkDto)
        {
            _logger.LogInformation(Log_RequestReceived, nameof(PostArtwork));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                // Create and validate Artwork entity
                var artwork = new Artwork
                {
                    Title = artworkDto.Title,
                    CreationDate = artworkDto.CreationDate,
                    CategoryID = artworkDto.CategoryID,
                    // Validate CategoryID exists (add logic here)
                    CategoryName = artworkDto.CategoryName,
                    Materials = artworkDto.Materials,
                    Dimensions = artworkDto.Dimensions,
                    Description = artworkDto.Description,
                    Conceptual = artworkDto.Conceptual
                };

                // Add Artwork to context and save
                _context.Artworks.Add(artwork);
                await _context.SaveChangesAsync();

                // Process and save ArtworkImages
                var artworkImages = new List<ArtworkImage>();
                foreach (var artworkImageDto in artworkDto.ArtworkImages)
                {
                    // Validate image data (add logic here)

                    var image = new Image
                    {
                        FileName = artworkImageDto.FileName,
                        FilePath = artworkImageDto.FilePath,
                        URL = artworkImageDto.URL
                    };

                    _context.Images.Add(image);
                    await _context.SaveChangesAsync();

                    artworkImages.Add(new ArtworkImage
                    {
                        ArtworkID = artwork.ArtworkID,
                        ImageID = image.ImageID
                    });
                }

                // Add ArtworkImages to context and save
                _context.Set<ArtworkImage>().AddRange(artworkImages);
                await _context.SaveChangesAsync();

                // Prepare and return response
                _logger.LogInformation(Log_RequestCreated, artwork.ArtworkID);
                return CreatedAtAction(nameof(GetArtwork), new { id = artwork.ArtworkID }, new ArtworkDTO
                {
                    ArtworkID = artwork.ArtworkID,
                    Title = artwork.Title,
                    CreationDate = artwork.CreationDate,
                    CategoryID = artwork.CategoryID,
                    CategoryName = artwork.CategoryName,
                    Materials = artwork.Materials,
                    Dimensions = artworkDto.Dimensions,
                    Description = artwork.Description,
                    Conceptual = artwork.Conceptual,
                    ArtworkImages = artworkImages.Select(ai => new ArtworkImageDTO
                    {
                        ArtworkID = ai.ArtworkID,
                        ImageID = ai.ImageID,
                        FileName = ai.Image.FileName,
                        FilePath = ai.Image.FilePath,
                        URL = ai.Image.URL
                    }).ToList()
                });
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, Log_ProcessingError, nameof(PostArtwork), artworkDto.ArtworkID);
                return StatusCode(500, "An error occurred while creating the artwork. Please try again later.");
            }
        }


        // PUT: api/Artworks/{id}
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutArtwork(int id, [FromBody] ArtworkDTO artworkDto)
        {
            _logger.LogInformation(Log_RequestReceivedWithId, nameof(PutArtwork), id);

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
                var artwork = await _context.Artworks.FindAsync(id);
                if (artwork == null)
                {
                    _logger.LogWarning(Log_RequestNotFound, nameof(PutArtwork), id);
                    return NotFound();
                }

                // Update artwork entity
                artwork.Title = artworkDto.Title;
                artwork.CreationDate = artworkDto.CreationDate;
                artwork.CategoryID = artworkDto.CategoryID;
                artwork.CategoryName = artworkDto.CategoryName;
                artwork.Materials = artworkDto.Materials;
                artwork.Dimensions = artworkDto.Dimensions;
                artwork.Description = artworkDto.Description;
                artwork.Conceptual = artworkDto.Conceptual;

                _context.Update(artwork);
                await _context.SaveChangesAsync();

                _logger.LogInformation(Log_RequestUpdated, artworkDto.ArtworkID);
                return NoContent();
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, Log_ProcessingError, nameof(PutArtwork), artworkDto.ArtworkID);
                return StatusCode(500, "An error occurred while updating the artwork. Please try again later.");
            }
        }


        // DELETE: api/Artworks/{id}
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArtwork(int id)
        {
            _logger.LogInformation(Log_RequestReceivedWithId, nameof(DeleteArtwork), id);

            try
            {
                var artwork = await _context.Artworks
                    .Include(a => a.ArtworkImages)
                    .ThenInclude(ai => ai.Image)
                    .FirstOrDefaultAsync(a => a.ArtworkID == id);

                if (artwork == null)
                {
                    _logger.LogWarning(Log_RequestNotFound, nameof(DeleteArtwork), id);
                    return NotFound();
                }

                _context.Artworks.Remove(artwork);
                await _context.SaveChangesAsync();

                _logger.LogInformation(Log_RequestDeleted, artwork.ArtworkID);
                return NoContent();
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, Log_ProcessingError, nameof(DeleteArtwork), id);
                return StatusCode(500, "An error occurred while deleting the artwork. Please try again later.");
            }
        }


        private bool ArtworkExists(int id)
        {
            return _context.Artworks.Any(e => e.ArtworkID == id);
        }
    }
}