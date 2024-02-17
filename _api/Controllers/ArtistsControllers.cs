namespace api_sylvainbreton.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using api_sylvainbreton.Models;
    using api_sylvainbreton.Data;
    using api_sylvainbreton.Services.Interfaces;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.Extensions.Logging;
    using Microsoft.Data.SqlClient;

    [Route("api/[controller]")]
    [ApiController]
    public class ArtistsController : ControllerBase
    {
        private readonly SylvainBretonDbContext _context;
        private readonly ILogger<ArtistsController> _logger; 
        private readonly ISanitizationService _sanitizationService;

        private const string Log_RequestReceived = "Request receive for {ActionName}";
        private const string Log_RequestReceivedWithId = "{ActionName} request for id {Id} has been found";
        private const string Log_RequestNotFound = "{ActionName} request for id {Id} not found";
        private const string Log_ProcessingError = "Error processing {ActionName} request for id {Id}";
        private const string Log_RequestCreated = "Artist with id {ArtistId} created successfully";
        private const string Log_RequestUpdated = "Artist with id {ArtistId} updated successfully";
        private const string Log_RequestDeleted = "Artist with id {ArtistId} deleted successfully";

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
            _logger.LogInformation(Log_RequestReceived, nameof(GetArtists));

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
            _logger.LogInformation(Log_RequestReceivedWithId, nameof(GetArtist), id);

            if (id <= 0)
            {
                return BadRequest("Invalid Artist ID");
            }

            var artist = await _context.Artists
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.ArtistID == id);
            if (artist == null)
            {
                _logger.LogInformation(Log_RequestNotFound, nameof(GetArtist), id);
                return NotFound();
            }

            return artist;
        }

        // POST: api/Artists
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<Artist>> PostArtist([FromBody] Artist artist)
        {
            _logger.LogInformation(Log_RequestReceived, nameof(PostArtist));

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

                _logger.LogInformation(Log_RequestCreated, artist.ArtistID);
                return CreatedAtAction(nameof(GetArtist), new { id = artist.ArtistID }, artist);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, Log_ProcessingError, nameof(PostArtist), artist.ArtistID);
                return StatusCode(500, "An error occurred while creating the artist. Please try again later.");
            }
        }


        // PUT: api/Artists/5
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutArtist(int id, [FromBody] Artist artist)
        {
            _logger.LogInformation(Log_RequestReceivedWithId, nameof(PutArtist), id);

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
                    _logger.LogWarning(Log_RequestNotFound, nameof(PutArtist), id);
                    return NotFound();
                }

                // 2. Update properties of existing artist object *selectively*
                existingArtist.FirstName = artist.FirstName; 

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

                _logger.LogInformation(Log_RequestUpdated, id);
                return NoContent();
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, Log_ProcessingError, nameof(PutArtist), id);
                return StatusCode(500, "An error occurred while updating the artist. Please try again later.");
            }
        }

        // DELETE: api/Artists/5
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArtist(int id)
        {
            _logger.LogInformation(Log_RequestReceivedWithId, nameof(DeleteArtist), id);

            try
            {
                var artist = await _context.Artists.FindAsync(id);
                if (artist == null)
                {
                    _logger.LogWarning(Log_RequestNotFound, nameof(DeleteArtist), id);
                    return NotFound();
                }

                _context.Artists.Remove(artist);
                await _context.SaveChangesAsync();

                _logger.LogInformation(Log_RequestDeleted, id);
                return NoContent();
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, Log_ProcessingError, nameof(DeleteArtist), id);
                return StatusCode(500, "An error occurred while deleting the artist. Please try again later.");
            }
        }


        private bool ArtistExists(int id)
        {
            return _context.Artists.Any(e => e.ArtistID == id);
        }
    }

}