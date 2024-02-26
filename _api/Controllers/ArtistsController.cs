namespace api_sylvainbreton.Controllers
{
    using api_sylvainbreton.Data;
    using api_sylvainbreton.Exceptions;
    using api_sylvainbreton.Models.DTOs;
    using api_sylvainbreton.Services.Interfaces;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.Extensions.Logging;

    [Route("api/[controller]")]
    [ApiController]
    public class ArtistsController(SylvainBretonDbContext context, ILogger<ArtistsController> logger, ISanitizationService sanitizationService, IMemoryCache memoryCache, IArtistService artistService) : ControllerBase
    {
        private readonly SylvainBretonDbContext _context = context;
        private readonly ILogger<ArtistsController> _logger = logger;
        private readonly ISanitizationService _sanitizationService = sanitizationService;
        private readonly IMemoryCache _memoryCache = memoryCache;
        private readonly IArtistService _artistService = artistService;

        // GET: api/Artists
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ArtistDTO>>> GetArtists([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var serviceResult = await _artistService.GetAllArtistsAsync(page, pageSize);
            if (!serviceResult.Success)
            {
                return StatusCode(serviceResult.StatusCode, serviceResult.ErrorMessage);
            }

            Response.Headers.Append("X-Pagination", Newtonsoft.Json.JsonConvert.SerializeObject(serviceResult.Pagination));
            return Ok(serviceResult.Data);
        }

        // GET: api/Artists/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetArtist(int id)
        {
            if (id <= 0)
            {
                _logger.LogWarning("ArtistsController: Invalid Artist ID provided.");
                return BadRequest("Invalid Artist ID.");
            }

            string cacheKey = $"artist_{id}";
            if (_memoryCache.TryGetValue(cacheKey, out ArtistDTO cachedArtist))
            {
                _logger.LogInformation("ArtistsController: Returning cached result for artist ID {Id}.", id);
                return Ok(cachedArtist);
            }

            try
            {
                var result = await _artistService.GetArtistByIdAsync(id);
                if (!result.Success)
                {
                    _logger.LogError("ArtistsController: Error retrieving artist with ID {Id}: {ErrorMessage}", id, result.ErrorMessage);
                    return StatusCode(result.StatusCode, result.ErrorMessage);
                }

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(30)); // Cache duration
                _memoryCache.Set(cacheKey, result.Data, cacheEntryOptions);

                return Ok(result.Data);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InternalServerErrorException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // POST: api/Artists
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<ArtistDTO>> PostArtist([FromBody] ArtistDTO artistDTO)
        {
            var result = await _artistService.CreateArtistAsync(artistDTO);
            if (!result.Success)
            {
                return StatusCode(result.StatusCode, result.ErrorMessage);
            }

            return CreatedAtAction(nameof(GetArtist), new { id = result.Data.ArtistID }, result.Data);
        }

        // PUT: api/Artists/5
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutArtist(int id, [FromBody] ArtistDTO artistDTO)
        {
            if (!ModelState.IsValid || artistDTO.FirstName.Length > 100 || artistDTO.LastName.Length > 100 || artistDTO.Bio.Length < 10)
            {
                return BadRequest("Validation error: First name and Last name cannot be longer than 100 characters, and Bio must be at least 10 characters long.");
            }

            var result = await _artistService.UpdateArtistAsync(id, artistDTO);
            if (!result.Success)
            {
                return StatusCode(result.StatusCode, result.ErrorMessage);
            }

            return NoContent();
        }

        // DELETE: api/Artists/5
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArtist(int id)
        {
            var result = await _artistService.DeleteArtistAsync(id);
            if (!result.Success)
            {
                return StatusCode(result.StatusCode, result.ErrorMessage);
            }

            return NoContent();
        }
    }
}