﻿namespace api_sylvainbreton.Controllers
{
    using api_sylvainbreton.Data;
    using api_sylvainbreton.Models;
    using api_sylvainbreton.Models.DTOs;
    using api_sylvainbreton.Services.Interfaces;
    using api_sylvainbreton.Services.Utilities;
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
            _logger.LogInformation("ArtworksController: GetArtworks request received for all artworks");

            var serviceResult = await _artworkService.GetAllArtworksAsync();

            if (!serviceResult.Success)
            {
                _logger.LogError("ArtworksController: Error processing GetArtworks: {ErrorMessage}", serviceResult.ErrorMessage);
                return StatusCode(serviceResult.StatusCode, serviceResult.ErrorMessage);
            }

            return Ok(serviceResult.Data);
        }

        // GET: api/Artworks/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ArtworkDTO>> GetArtwork(int id)
        {
            _logger.LogInformation("ArtworksController: GetArtwork request received for artwork ID {ArtworkId}", id);

            var serviceResult = await _artworkService.GetArtworkByIdAsync(id);

            if (!serviceResult.Success)
            {
                _logger.LogError("ArtworksController: Error processing GetArtwork: {ErrorMessage}", serviceResult.ErrorMessage);
                return StatusCode(serviceResult.StatusCode, serviceResult.ErrorMessage);
            }

            return Ok(serviceResult.Data);
        }

        // POST: api/Artworks
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<ArtworkDTO>> PostArtwork([FromBody] ArtworkDTO artworkDTO)
        {
            _logger.LogInformation("ArtworksController: PostArtwork request received for new artwork creation");

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var serviceResult = await _artworkService.CreateArtworkAsync(artworkDTO);

            if (!serviceResult.Success)
            {
                _logger.LogError("ArtworksController: Error processing PostArtwork: {ErrorMessage}", serviceResult.ErrorMessage);
                return StatusCode(serviceResult.StatusCode, serviceResult.ErrorMessage);
            }

            _logger.LogInformation("ArtworksController: Artwork with ID {ArtworkId} created successfully", serviceResult.Data.ArtworkID);
            return CreatedAtAction(nameof(GetArtwork), new { id = serviceResult.Data.ArtworkID }, serviceResult.Data);
        }

        // PUT: api/Artworks/5
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutArtwork(int id, [FromBody] ArtworkDTO artworkDto)
        {
            _logger.LogInformation("ArtworksController: GetArtwork request received for artwork ID {ArtworkId}", id);

            var serviceResult = await _artworkService.UpdateArtworkAsync(id, artworkDto);

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
            _logger.LogInformation("ArtworksController: Delete request received for artwork ID {ArtworkId}", id);

            var serviceResult = await _artworkService.DeleteArtworkAsync(id);

            if (!serviceResult.Success)
            {
                _logger.LogError("ArtworksController: Error processing DeleteArtwork for ID {ArtworkId}: {ErrorMessage}", id, serviceResult.ErrorMessage);
                return StatusCode(serviceResult.StatusCode, serviceResult.ErrorMessage);
            }

            return NoContent();
        }
    }
}