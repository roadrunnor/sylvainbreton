using Microsoft.AspNetCore.Mvc;
using api_sylvainbreton.Models;
using api_sylvainbreton.Data;
using Microsoft.EntityFrameworkCore;
using api_sylvainbreton.Models.DTOs;

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

        // GET: api/Artworks
        [HttpGet]
        public ActionResult<IEnumerable<Artwork>> GetArtworks()
        {
            return _context.Artworks.ToList();
        }

        // GET: api/Artworks/5
        [HttpGet("{id}")]
        public ActionResult<Artwork> GetArtwork(int id)
        {
            var artwork = _context.Artworks.Find(id);

            if (artwork == null)
            {
                return NotFound();
            }

            return artwork;
        }

        // GET: api/Artworks
        [HttpGet("Dto")]
        public ActionResult<IEnumerable<ArtworkDTO>> GetArtworksDto()
        {
            var artworksDto = _context.Artworks
                .Select(a => new ArtworkDTO
                {
                    ArtworkID = a.ArtworkID,
                    Title = a.Title,
                    CreationDate = a.CreationDate
                    // Map any other necessary properties
                })
                .ToList();

            return Ok(artworksDto); // 'artworksDto' to match the variable name
        }


        // POST: api/Artworks
        [HttpPost]
        public ActionResult<Artwork> PostArtwork(Artwork artwork)
        {
            _context.Artworks.Add(artwork);
            _context.SaveChanges();

            return CreatedAtAction("GetArtwork", new { id = artwork.ArtworkID }, artwork);
        }

        // PUT: api/Artworks/5
        [HttpPut("{id}")]
        public IActionResult PutArtwork(int id, Artwork artwork)
        {
            if (id != artwork.ArtworkID)
            {
                return BadRequest();
            }

            _context.Entry(artwork).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArtworkExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Artworks/5
        [HttpDelete("{id}")]
        public ActionResult<Artwork> DeleteArtwork(int id)
        {
            var artwork = _context.Artworks.Find(id);
            if (artwork == null)
            {
                return NotFound();
            }

            _context.Artworks.Remove(artwork);
            _context.SaveChanges();

            return artwork;
        }

        private bool ArtworkExists(int id)
        {
            return _context.Artworks.Any(e => e.ArtworkID == id);
        }
    }
}

