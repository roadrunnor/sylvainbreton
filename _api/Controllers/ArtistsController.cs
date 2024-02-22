namespace api_sylvainbreton.Controllers
{
    using api_sylvainbreton.Data;
    using api_sylvainbreton.Models;
    using api_sylvainbreton.Models.DTOs;
    using api_sylvainbreton.Services.Interfaces;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.Extensions.Logging;
    using System;
    using static api_sylvainbreton.Exceptions.Exceptions;

    [Route("api/[controller]")]
    [ApiController]
    public class ArtistsController(SylvainBretonDbContext context, ILogger<ArtistsController> logger, ISanitizationService sanitizationService, IMemoryCache memoryCache) : ControllerBase
    {
        private readonly SylvainBretonDbContext _context = context;
        private readonly ILogger<ArtistsController> _logger = logger;
        private readonly ISanitizationService _sanitizationService = sanitizationService;
        private readonly IMemoryCache _memoryCache = memoryCache;

        // GET: api/Artists
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ArtistDTO>>> GetArtists([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            // Log the receipt of the GetArtists request with query parameters for pagination
            _logger.LogInformation("{ControllerName}: {ActionName} request received with page number {Page} and page size {PageSize}", 
                nameof(ArtistsController), nameof(GetArtists), page, pageSize);

            // Enforcing reasonable limits
            if (pageSize < 1 || pageSize > 100)
            {
                throw new BadRequestException("Invalid page size. Must be between 1 and 100");
            }

            // Validate pageSize to ensure it's within a reasonable range
            pageSize = Math.Clamp(pageSize, 1, 100);

            var totalRecords = await _context.Artists.CountAsync(); // Get the total number of records
            var totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize); // Calculate total pages



            var query = _context.Artists
                .AsNoTracking()
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(a => new ArtistDTO
                {
                    ArtistID = a.ArtistID,
                    FirstName = a.FirstName,
                    LastName = a.LastName,
                })
                .ToListAsync();

            var artists = await query;

            // Include pagination metadata in the response header or as part of a custom response object
            Response.Headers.Append("X-Pagination", Newtonsoft.Json.JsonConvert
                .SerializeObject(new { TotalRecords = totalRecords, Page = page, PageSize = pageSize, TotalPages = totalPages }));

            return Ok(artists);
        }


        // GET: api/Artists/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ArtistDTO>> GetArtist(int id)
        {
            // Received request for GetArtist
            _logger.LogInformation("{ControllerName}: {ActionName} request received for artist ID {ArtistId}", 
                nameof(ArtistsController), nameof(GetArtist), id);

            if (id <= 0)
            {
                throw new BadRequestException("Invalid Artist ID");
            }

            string cacheKey = $"artist_{id}";
            if (_memoryCache.TryGetValue(cacheKey, out ArtistDTO cachedArtist))
            {
                return cachedArtist;
            }

            var artist = await _context.Artists
                .AsNoTracking()
                .Select(a => new ArtistDTO
                {
                    ArtistID = a.ArtistID,
                    FirstName = a.FirstName,
                    LastName = a.LastName
                })
                .FirstOrDefaultAsync(a => a.ArtistID == id);

            if (artist == null)
            {
                // Artist not found in GetArtist
                _logger.LogWarning("{ControllerName}: Artist with ID {ArtistId} not found in {ActionName}", 
                    nameof(ArtistsController), id, nameof(GetArtist));
                throw new NotFoundException($"Artist with ID {id} not found");
            }

            return artist;
        }

        // POST: api/Artists
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<ArtistDTO>> PostArtist([FromBody] ArtistDTO artistDTO)
        {
            // Log that a request to create a new artist has been received
            _logger.LogInformation("{ControllerName}: {ActionName} request received for new artist creation", 
                nameof(ArtistsController), nameof(PostArtist));

            try
            {
                // Map ArtistDTO to Artist entity
                var artist = new Artist
                {
                    FirstName = artistDTO.FirstName,
                    LastName = artistDTO.LastName,
                    Bio = artistDTO.Bio
                };

                // Input Sanitization
                artist.FirstName = _sanitizationService.SanitizeInput(artist.FirstName);
                artist.LastName = _sanitizationService.SanitizeInput(artist.LastName);
                artist.Bio = _sanitizationService.SanitizeInput(artist.Bio);

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                _context.Artists.Add(artist);
                await _context.SaveChangesAsync();

                // Update the DTO with the generated ID and any other properties set by the server
                artistDTO.ArtistID = artist.ArtistID;

                // Artist created successfully
                _logger.LogInformation("{ControllerName}: Artist with ID {ArtistId} {Action} successfully", 
                    nameof(ArtistsController), artistDTO.ArtistID, "created");
                return CreatedAtAction(nameof(GetArtist), new { id = artistDTO.ArtistID }, artistDTO);
            }
            catch (DbUpdateException ex)
            {
                // Error occurred during PostArtist
                _logger.LogError("{ControllerName}: Error processing {ActionName} for artist ID {ArtistId}: {ExceptionMessage}", 
                    nameof(ArtistsController), nameof(PostArtist), artistDTO.ArtistID, ex.Message);
                throw new InternalServerErrorException("An error occurred while creating the artist. Please try again later.");
            }
        }


        // PUT: api/Artists/5
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutArtist(int id, [FromBody] ArtistDTO artistDTO)
        {
            // Log the received request with the artist ID
            _logger.LogInformation("{ControllerName}: {ActionName} request received for artist ID {ArtistId}", 
                nameof(ArtistsController), nameof(PutArtist), id);

            if (id != artistDTO.ArtistID)
            {
                throw new BadRequestException("Artist ID in the URL does not match the Artist ID in the request body.");
            }

            // Map ArtistDTO to Artist entity
            var artist = new Artist
            {
                FirstName = artistDTO.FirstName,
                LastName = artistDTO.LastName,
                Bio = artistDTO.Bio
            };

            artist.FirstName = _sanitizationService.SanitizeInput(artist.FirstName);
            artist.LastName = _sanitizationService.SanitizeInput(artist.LastName);
            artist.Bio = _sanitizationService.SanitizeInput(artist.Bio);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != artist.ArtistID)
            {
                throw new BadRequestException("ArtistID in URL does not match ArtistID in request body");
            }

            try
            {
                // 1. Check if artist with the given ID exists
                var artistToUpdate = await _context.Artists.FindAsync(id);
                if (artistToUpdate == null)
                {
                    _logger.LogWarning("{ControllerName}: Artist with ID {ArtistId} not found in {ActionName}", 
                        nameof(ArtistsController), id, nameof(PutArtist));
                    throw new NotFoundException($"Artist with ID {id} not found");
                }

                // 2. Update properties of existing artist object *selectively*
                artistToUpdate.FirstName = artist.FirstName;
                artistToUpdate.LastName = artist.LastName;
                artistToUpdate.Bio = artist.Bio;

                // 3. Additional data validation for Artist properties (e.g., FirstName, LastName)
                // Here, we validate FirstName length using MaxLength attribute on the model
                if (artistToUpdate.FirstName.Length > 100)
                {
                    ModelState.AddModelError(nameof(artistToUpdate.FirstName), "First name cannot be longer than 100 characters.");
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


                _context.Update(artistToUpdate);
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
                    throw new NotFoundException($"Artist with ID {id} not found");
                }
                else
                {
                    // Error occurred during PutArtist
                    _logger.LogError("{ControllerName}: Error processing {ActionName} for artist ID {ArtistId}: {ExceptionMessage}", 
                        nameof(ArtistsController), nameof(PutArtist), artistDTO.ArtistID, ex.Message);
                    throw new InternalServerErrorException("An error occurred while updating the artist. Please try again later.");
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
                var artistToDelete = await _context.Artists.FindAsync(id);
                if (artistToDelete == null)
                {
                    // Artist not found in DeleteArtist
                    _logger.LogWarning("{ControllerName}: Artist with ID {ArtistId} not found in {ActionName}", 
                        nameof(ArtistsController), id, nameof(DeleteArtist));
                    throw new NotFoundException($"Artist with ID {id} not found");
                }

                _context.Artists.Remove(artistToDelete);
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
                throw new InternalServerErrorException("An error occurred while deleting the artist. Please try again later.");
            }
        }


        private bool ArtistExists(int id)
        {
            return _context.Artists.Any(e => e.ArtistID == id);
        }
    }

}