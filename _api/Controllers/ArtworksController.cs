using Microsoft.AspNetCore.Mvc;
using api_sylvainbreton.Models;
using api_sylvainbreton.Models.DTOs;
using api_sylvainbreton.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_sylvainbreton.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtworksController : ControllerBase
    {
        private readonly SylvainBretonDbContext _context;

        public ArtworksController(SylvainBretonDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ArtworkDTO>> GetArtworks()
        {
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

        [HttpGet("{id}")]
        public ActionResult<ArtworkDTO> GetArtwork(int id)
        {
            var artwork = _context.Artworks
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
                .FirstOrDefault();

            if (artwork == null)
            {
                return NotFound();
            }

            return artwork;
        }

        [HttpPost]
        public async Task<ActionResult<ArtworkDTO>> PostArtwork([FromBody] ArtworkDTO artworkDto)
        {
            var artwork = new Artwork
            {
                Title = artworkDto.Title,
                CreationDate = artworkDto.CreationDate,
                CategoryID = artworkDto.CategoryID,
                CategoryName = artworkDto.CategoryName,
                Materials = artworkDto.Materials,
                Dimensions = artworkDto.Dimensions,
                Description = artworkDto.Description,
                Conceptual = artworkDto.Conceptual
            };

            _context.Artworks.Add(artwork);
            await _context.SaveChangesAsync();

            foreach (var artworkImageDto in artworkDto.ArtworkImages)
            {
                var image = new Image
                {
                    FileName = artworkImageDto.FileName,
                    FilePath = artworkImageDto.FilePath,
                    URL = artworkImageDto.URL
                };

                _context.Images.Add(image);
                await _context.SaveChangesAsync();

                var artworkImage = new ArtworkImage
                {
                    ArtworkID = artwork.ArtworkID,
                    ImageID = image.ImageID
                };

                _context.Set<ArtworkImage>().Add(artworkImage);
            }

            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetArtwork), new { id = artwork.ArtworkID }, new ArtworkDTO
            {
                ArtworkID = artwork.ArtworkID,
                Title = artwork.Title,
                CreationDate = artwork.CreationDate,
                CategoryID = artwork.CategoryID,
                CategoryName = artwork.CategoryName,
                Materials = artwork.Materials,
                Dimensions = artwork.Dimensions,
                Description = artwork.Description,
                Conceptual = artwork.Conceptual,
                ArtworkImages = artwork.ArtworkImages.Select(ai => new ArtworkImageDTO
                {
                    ArtworkID = ai.ArtworkID,
                    ImageID = ai.ImageID,
                    FileName = ai.Image.FileName,
                    FilePath = ai.Image.FilePath,
                    URL = ai.Image.URL
                }).ToList()
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutArtwork(int id, [FromBody] ArtworkDTO artworkDto)
        {
            if (id != artworkDto.ArtworkID)
            {
                return BadRequest();
            }

            var artwork = await _context.Artworks.FindAsync(id);
            if (artwork == null)
            {
                return NotFound();
            }

            artwork.Title = artworkDto.Title;
            artwork.CreationDate = artworkDto.CreationDate;
            artwork.CategoryID = artworkDto.CategoryID;
            artwork.CategoryName = artworkDto.CategoryName;
            artwork.Materials = artworkDto.Materials;
            artwork.Dimensions = artworkDto.Dimensions;
            artwork.Description = artworkDto.Description;
            artwork.Conceptual = artworkDto.Conceptual;

            _context.Entry(artwork).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ArtworkDTO>> DeleteArtwork(int id)
        {
            var artwork = await _context.Artworks.FindAsync(id);
            if (artwork == null)
            {
                return NotFound();
            }

            _context.Artworks.Remove(artwork);
            await _context.SaveChangesAsync();

            return new ArtworkDTO
            {
                ArtworkID = artwork.ArtworkID,
                Title = artwork.Title,
                CreationDate = artwork.CreationDate,
                CategoryID = artwork.CategoryID,
                CategoryName = artwork.CategoryName,
                Materials = artwork.Materials,
                Dimensions = artwork.Dimensions,
                Description = artwork.Description,
                Conceptual = artwork.Conceptual,
                ArtworkImages = artwork.ArtworkImages.Select(ai => new ArtworkImageDTO
                {
                    ArtworkID = ai.ArtworkID,
                    ImageID = ai.ImageID,
                    FileName = ai.Image.FileName,
                    FilePath = ai.Image.FilePath,
                    URL = ai.Image.URL
                }).ToList()
            };
        }

        private bool ArtworkExists(int id)
        {
            return _context.Artworks.Any(e => e.ArtworkID == id);
        }
    }
}