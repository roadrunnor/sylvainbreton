using Microsoft.AspNetCore.Mvc;
using api_sylvainbreton.Models;
using System.Collections.Generic;
using System.Linq;
// Assurez-vous d'inclure tous les namespaces nécessaires

namespace api_sylvainbreton.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtworksController : ControllerBase
    {
        private readonly SylvainBretonDbContext _context; // Remplacez SylvainBretonDbContext par le contexte de votre base de données

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

