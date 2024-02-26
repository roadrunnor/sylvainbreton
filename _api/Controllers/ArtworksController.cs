namespace api_sylvainbreton.Controllers
{
    using api_sylvainbreton.Data;
    using api_sylvainbreton.Exceptions;
    using api_sylvainbreton.Models.DTOs;
    using api_sylvainbreton.Services.Interfaces;
    using api_sylvainbreton.Services.Utilities;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    [Route("api/[controller]")]
    [ApiController]
    public class ArtworksController(SylvainBretonDbContext context, ILogger<ArtworksController> logger, ISanitizationService sanitizationService, ImageService imageService, ImageValidationService imageValidationService, IArtworkService artworkService) : ControllerBase
    {
        private readonly SylvainBretonDbContext _context = context;
        private readonly ILogger<ArtworksController> _logger = logger;
        private readonly ISanitizationService _sanitizationService = sanitizationService;
        private readonly ImageService _imageService = imageService;
        private readonly ImageValidationService _imageValidationService = imageValidationService;
        private readonly IArtworkService _artworkService = artworkService;

        // GET: api/Artworks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ArtworkDTO>>> GetArtworks()
        {
            var serviceResult = await _artworkService.GetAllArtworksAsync();
            if (!serviceResult.Success)
            {
                throw new BadRequestException(serviceResult.ErrorMessage);
            }

            return Ok(serviceResult.Data);
        }

        // GET: api/Artworks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ArtworkDTO>> GetArtwork(int id)
        {
            var serviceResult = await _artworkService.GetArtworkByIdAsync(id);
            if (!serviceResult.Success)
            {
                throw new NotFoundException($"Artwork with ID {id} not found.");
            }

            return Ok(serviceResult.Data);
        }

        // POST: api/Artworks
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<ArtworkDTO>> PostArtwork([FromBody] ArtworkDTO artworkDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("ArtworksController: Invalid model state for PostArtwork.");
                return BadRequest(ModelState);
            }

            var serviceResult = await _artworkService.CreateArtworkAsync(artworkDTO);
            if (!serviceResult.Success)
            {
                throw new BadRequestException(serviceResult.ErrorMessage);
            }

            return CreatedAtAction(nameof(GetArtwork), new { id = serviceResult.Data.ArtworkID }, serviceResult.Data);
        }

        // PUT: api/Artworks/5
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutArtwork(int id, [FromBody] ArtworkDTO artworkDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("ArtworksController: Invalid model state for PutArtwork with ID {ArtworkId}.", id);
                return BadRequest(ModelState);
            }

            var serviceResult = await _artworkService.UpdateArtworkAsync(id, artworkDTO);
            if (!serviceResult.Success)
            {
                _logger.LogError("ArtworksController: Error processing PutArtwork for ID {ArtworkId}: {ErrorMessage}", id, serviceResult.ErrorMessage);
                return StatusCode(serviceResult.StatusCode, serviceResult.ErrorMessage);
            }

            return NoContent();
        }

        // DELETE: api/Artworks/5
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArtwork(int id)
        {
            try
            {
                var serviceResult = await _artworkService.DeleteArtworkAsync(id);
                if (!serviceResult.Success)
                {
                    _logger.LogError("ArtworksController: Error processing DeleteArtwork for ID {ArtworkId}: {ErrorMessage}", id, serviceResult.ErrorMessage);
                    return StatusCode(serviceResult.StatusCode, serviceResult.ErrorMessage);
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError("ArtworksController: An unexpected error occurred while processing DeleteArtwork for ID {ArtworkId}: {Message}", id, ex.Message);
                return StatusCode(500, "An unexpected error occurred. Please try again later.");
            }
        }
    }
}