namespace api_sylvainbreton.Controllers
{
    using api_sylvainbreton.Data;
    using api_sylvainbreton.Models.DTOs;
    using api_sylvainbreton.Services.Interfaces;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    [Route("api/[controller]")]
    [ApiController]
    public class ArtistsController(SylvainBretonDbContext context, ILogger<ArtistsController> logger, ISanitizationService sanitizationService, IArtistService artistService) : ControllerBase
    {
        private readonly SylvainBretonDbContext _context = context;
        private readonly ILogger<ArtistsController> _logger = logger;
        private readonly ISanitizationService _sanitizationService = sanitizationService;
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
            var serviceResult = await _artistService.GetArtistByIdAsync(id);

            if (!serviceResult.Success)
            {
                return StatusCode(serviceResult.StatusCode, serviceResult.ErrorMessage);
            }

            return Ok(serviceResult.Data);
        }

        // POST: api/Artists
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<ArtistDTO>> PostArtist([FromBody] ArtistDTO artistDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var serviceResult = await _artistService.CreateArtistAsync(artistDTO);

            if (!serviceResult.Success)
            {
                return StatusCode(serviceResult.StatusCode, serviceResult.ErrorMessage);
            }

            return CreatedAtAction(nameof(GetArtist), new { id = serviceResult.Data.ArtistID }, serviceResult.Data);
        }

        // PUT: api/Artists/5
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutArtist(int id, [FromBody] ArtistDTO artistDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var serviceResult = await _artistService.UpdateArtistAsync(id, artistDTO);

            if (!serviceResult.Success)
            {
                return StatusCode(serviceResult.StatusCode, serviceResult.ErrorMessage);
            }

            return NoContent();
        }

        // DELETE: api/Artists/5
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArtist(int id)
        {
            var serviceResult = await _artistService.DeleteArtistAsync(id);

            if (!serviceResult.Success)
            {
                return StatusCode(serviceResult.StatusCode, serviceResult.ErrorMessage);
            }

            return NoContent();
        }
    }
}