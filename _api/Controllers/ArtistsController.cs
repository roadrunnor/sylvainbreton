namespace api_sylvainbreton.Controllers
{
    using api_sylvainbreton.Data;
    using api_sylvainbreton.Models;
    using api_sylvainbreton.Services.Interfaces;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;
    using System;

    [Route("api/[controller]")]
    [ApiController]
    public class ArtistsController : ControllerBase
    {
        private readonly SylvainBretonDbContext _context;
        private readonly ILogger<ArtistsController> _logger;
        private readonly ISanitizationService _sanitizationService;

        public ArtistsController(SylvainBretonDbContext context, ILogger<ArtistsController> logger, ISanitizationService sanitizationService)
        {
            _context = context;
            _logger = logger;
            _sanitizationService = sanitizationService;
        }

        // GET: api/Artists
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Artist>>> GetArtists([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            // Log the receipt of the GetArtists request with query parameters for pagination
            _logger.LogInformation("{ControllerName}: {ActionName} request received with page number {Page} and page size {PageSize}", 
                nameof(ArtistsController), nameof(GetArtists), page, pageSize);

            // Validate pageSize to ensure it's within a reasonable range
            pageSize = Math.Clamp(pageSize, 1, 100);

            var totalRecords = await _context.Artists.CountAsync(); // Get the total number of records
            var totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize); // Calculate total pages

            var query = _context.Artists
                .AsNoTracking()
                .Skip((page - 1) * pageSize)
                .Take(pageSize);

            var artists = await query.ToListAsync();

            // Include pagination metadata in the response header or as part of a custom response object
            Response.Headers.Append("X-Pagination", Newtonsoft.Json.JsonConvert
                .SerializeObject(new { TotalRecords = totalRecords, Page = page, PageSize = pageSize, TotalPages = totalPages }));

            return artists;
        }


        // GET: api/Artists/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Artist>> GetArtist(int id)
        {
            // Received request for GetArtist
            _logger.LogInformation("{ControllerName}: {ActionName} request received for artist ID {ArtistId}", 
                nameof(ArtistsController), nameof(GetArtist), id);

            if (id <= 0)
            {
                return BadRequest("Invalid Artist ID");
            }

            var artist = await _context.Artists
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.ArtistID == id);

            if (artist == null)
            {
                // Artist not found in GetArtist
                _logger.LogWarning("{ControllerName}: Artist with ID {ArtistId} not found in {ActionName}", 
                    nameof(ArtistsController), id, nameof(GetArtist));
                return NotFound();
            }

            return artist;
        }

        // POST: api/Artists
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<Artist>> PostArtist([FromBody] Artist artist)
        {
            // Log that a request to create a new artist has been received
            _logger.LogInformation("{ControllerName}: {ActionName} request received for new artist creation", 
                nameof(ArtistsController), nameof(PostArtist));

            // Input Sanitization
            artist.FirstName = _sanitizationService.SanitizeInput(artist.FirstName);
            artist.LastName = _sanitizationService.SanitizeInput(artist.LastName);
            artist.Bio = _sanitizationService.SanitizeInput(artist.Bio);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _context.Artists.Add(artist);
                await _context.SaveChangesAsync();

                // Artist created successfully
                _logger.LogInformation("{ControllerName}: Artist with ID {ArtistId} {Action} successfully", 
                    nameof(ArtistsController), artist.ArtistID, "created");
                return CreatedAtAction(nameof(GetArtist), new { id = artist.ArtistID }, artist);
            }
            catch (DbUpdateException ex)
            {
                // Error occurred during PostArtist
                _logger.LogError("{ControllerName}: Error processing {ActionName} for artist ID {ArtistId}: {ExceptionMessage}", 
                    nameof(ArtistsController), nameof(PostArtist), artist.ArtistID, ex.Message);
                return StatusCode(500, "An error occurred while creating the artist. Please try again later.");
            }
        }


        // PUT: api/Artists/5
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutArtist(int id, [FromBody] Artist artist)
        {
            // Log the received request with the artist ID
            _logger.LogInformation("{ControllerName}: {ActionName} request received for artist ID {ArtistId}", 
                nameof(ArtistsController), nameof(PutArtist), id);

            artist.FirstName = _sanitizationService.SanitizeInput(artist.FirstName);
            artist.LastName = _sanitizationService.SanitizeInput(artist.LastName);
            artist.Bio = _sanitizationService.SanitizeInput(artist.Bio);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != artist.ArtistID)
            {
                return BadRequest("ArtworkID in URL does not match ArtworkID in request body");
            }

            try
            {
                // 1. Check if artist with the given ID exists
                var existingArtist = await _context.Artists.FindAsync(id);
                if (existingArtist == null)
                {
                    _logger.LogWarning("{ControllerName}: Artist with ID {ArtistId} not found in {ActionName}", 
                        nameof(ArtistsController), id, nameof(PutArtist));
                    return NotFound();
                }

                // 2. Update properties of existing artist object *selectively*
                existingArtist.FirstName = artist.FirstName;
                existingArtist.LastName = artist.LastName;
                existingArtist.Bio = artist.Bio;

                // 3. Additional data validation for Artist properties (e.g., FirstName, LastName)
                // Here, we validate FirstName length using MaxLength attribute on the model
                if (existingArtist.FirstName.Length > 100)
                {
                    ModelState.AddModelError(nameof(existingArtist.FirstName), "First name cannot be longer than 100 characters.");
                    return BadRequest(ModelState);
                }

                if (artist.LastName.Length > 100)
                {
                    ModelState.AddModelError(nameof(artist.LastName), "Last name cannot be longer than 100 characters.");
                }

                if (artist.Bio.Length < 10)
                {
                    ModelState.AddModelError(nameof(artist.Bio), "Bio must be at least 10 characters long.");
                }


                _context.Update(existingArtist);
                await _context.SaveChangesAsync();

                // Artist updated successfully
                _logger.LogInformation("{ControllerName}: Artist with ID {ArtistId} {Action} successfully", 
                    nameof(ArtistsController), artist.ArtistID, "updated");
                return NoContent();
            }
            catch (DbUpdateException ex)
            {
                if (!ArtistExists(id))
                {
                    // Artist not found in PutArtist
                    _logger.LogWarning("{ControllerName}: Artist with ID {ArtistId} not found in {ActionName}", 
                        nameof(ArtistsController), id, nameof(PutArtist));
                    return NotFound();
                }
                else
                {
                    // Error occurred during PutArtist
                    _logger.LogError("{ControllerName}: Error processing {ActionName} for artist ID {ArtistId}: {ExceptionMessage}", 
                        nameof(ArtistsController), nameof(PutArtist), artist.ArtistID, ex.Message);
                    return StatusCode(500, "An error occurred while updating the artist. Please try again later.");
                }
            }
        }

        // DELETE: api/Artists/5
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArtist(int id)
        {
            _logger.LogWarning("{ControllerName}: Artist with ID {ArtistId} not found in {ActionName}", 
                nameof(ArtistsController), id, nameof(DeleteArtist));

            try
            {
                var artist = await _context.Artists.FindAsync(id);
                if (artist == null)
                {
                    // Artist not found in DeleteArtist
                    _logger.LogWarning("{ControllerName}: Artist with ID {ArtistId} not found in {ActionName}", 
                        nameof(ArtistsController), id, nameof(DeleteArtist));
                    return NotFound();
                }

                _context.Artists.Remove(artist);
                await _context.SaveChangesAsync();

                // Artist deleted successfully
                _logger.LogInformation("{ControllerName}: Artist with ID {ArtistId} {Action} successfully", 
                    nameof(ArtistsController), id, "deleted");
                return NoContent();
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError("{ControllerName}: Error processing {ActionName} for artist ID {ArtistId}: {ExceptionMessage}", 
                    nameof(ArtistsController), nameof(DeleteArtist), id, ex.Message);
                return StatusCode(500, "An error occurred while deleting the artist. Please try again later.");
            }
        }


        private bool ArtistExists(int id)
        {
            return _context.Artists.Any(e => e.ArtistID == id);
        }
    }

}