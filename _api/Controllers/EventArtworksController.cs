using Microsoft.AspNetCore.Mvc;
using api_sylvainbreton.Models;
using System.Collections.Generic;
using System.Linq;
using api_sylvainbreton.Data;
using Microsoft.EntityFrameworkCore;

namespace api_sylvainbreton.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventArtworksController : ControllerBase
    {
        private readonly SylvainBretonDbContext _context;

        public EventArtworksController(SylvainBretonDbContext context)
        {
            _context = context;
        }

        // GET: api/EventArtworks
        [HttpGet]
        public ActionResult<IEnumerable<EventArtwork>> GetEventArtworks()
        {
            return _context.EventArtworks
                .Include(ea => ea.Event)
                .Include(ea => ea.Artwork)
                .ToList();
        }

        // GET: api/EventArtworks/5/10
        [HttpGet("{eventId}/{artworkId}")]
        public ActionResult<EventArtwork> GetEventArtwork(int eventId, int artworkId)
        {
            var eventArtwork = _context.EventArtworks
                .Where(ea => ea.EventID == eventId && ea.ArtworkID == artworkId)
                .Include(ea => ea.Event)
                .Include(ea => ea.Artwork)
                .FirstOrDefault();

            if (eventArtwork == null)
            {
                return NotFound();
            }

            return eventArtwork;
        }

        // POST: api/EventArtworks
        [HttpPost]
        public ActionResult<EventArtwork> PostEventArtwork(EventArtwork eventArtwork)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_context.Events.Any(e => e.EventID == eventArtwork.EventID) ||
        !       _context.Artworks.Any(a => a.ArtworkID == eventArtwork.ArtworkID))
            {
                return BadRequest("The specified Event or Artwork does not exist.");
            }

            _context.EventArtworks.Add(eventArtwork);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetEventArtwork), new { eventId = eventArtwork.EventID, artworkId = eventArtwork.ArtworkID }, eventArtwork);
        }

        // DELETE: api/EventArtworks/5/10
        [HttpDelete("{eventId}/{artworkId}")]
        public IActionResult DeleteEventArtwork(int eventId, int artworkId)
        {
            var eventArtwork = _context.EventArtworks.Find(eventId, artworkId);
            if (eventArtwork == null)
            {
                return NotFound();
            }

            _context.EventArtworks.Remove(eventArtwork);
            _context.SaveChanges();

            return NoContent();
        }

        private bool EventArtworkExists(int eventId, int artworkId)
        {
            return _context.EventArtworks.Any(ea => ea.EventID == eventId && ea.ArtworkID == artworkId);
        }
    }
}
