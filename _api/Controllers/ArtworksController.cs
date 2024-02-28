namespace api_sylvainbreton.Controllers
{
    using api_sylvainbreton.Models.DTOs;
    using api_sylvainbreton.Services.Interfaces;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    [Route("api/[controller]")]
    [ApiController]
    public class ArtworksController(IArtworkService artworkService) : ControllerBase
    {
        private readonly IArtworkService _artworkService = artworkService;

        // GET: api/Artworks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ArtworkDTO>>> GetArtworks()
        {
            var serviceResult = await _artworkService.GetAllArtworksAsync();

            if (!serviceResult.Success)
            {
                return StatusCode(serviceResult.StatusCode, serviceResult.ErrorMessage);
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
                return StatusCode(serviceResult.StatusCode, serviceResult.ErrorMessage);
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
                return BadRequest(ModelState);
            }

            var serviceResult = await _artworkService.CreateArtworkAsync(artworkDTO);

            if (!serviceResult.Success)
            {
                return StatusCode(serviceResult.StatusCode, serviceResult.ErrorMessage);
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
                return BadRequest(ModelState);
            }

            var serviceResult = await _artworkService.UpdateArtworkAsync(id, artworkDTO);

            if (!serviceResult.Success)
            {
                return StatusCode(serviceResult.StatusCode, serviceResult.ErrorMessage);
            }

            return NoContent();
        }

        // DELETE: api/Artworks/5
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArtwork(int id)
        {
            var serviceResult = await _artworkService.DeleteArtworkAsync(id);

            if (!serviceResult.Success)
            {
                return StatusCode(serviceResult.StatusCode, serviceResult.ErrorMessage);
            }

            return NoContent();
        }
    }
}