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
    public class EventsController : ControllerBase
    {
        private readonly SylvainBretonDbContext _context;

        public EventsController(SylvainBretonDbContext context)
        {
            _context = context;
        }

        // GET: api/Events
        [HttpGet]
        public ActionResult<IEnumerable<Event>> GetEvents()
        {
            return _context.Events
                .Include(e => e.Place) // Inclut les données associées de Place
                .Include(e => e.EventArtworks) // Inclut les relations EventArtwork
                .ToList();
        }

        // GET: api/Events/5
        [HttpGet("{id}")]
        public ActionResult<Event> GetEvent(int id)
        {
            var eventEntity = _context.Events
                .Include(e => e.Place) // Inclut les données associées de Place
                .Include(e => e.EventArtworks) // Inclut les relations EventArtwork
                .FirstOrDefault(e => e.EventID == id);

            if (eventEntity == null)
            {
                return NotFound();
            }

            return eventEntity;
        }

        // POST: api/Events
        [HttpPost]
        public ActionResult<Event> PostEvent(Event eventEntity)
        {
            _context.Events.Add(eventEntity);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetEvent), new { id = eventEntity.EventID }, eventEntity);
        }

        // PUT: api/Events/5
        [HttpPut("{id}")]
        public IActionResult PutEvent(int id, Event eventEntity)
        {
            if (id != eventEntity.EventID)
            {
                return BadRequest();
            }

            _context.Entry(eventEntity).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventExists(id))
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

        // DELETE: api/Events/5
        [HttpDelete("{id}")]
        public ActionResult<Event> DeleteEvent(int id)
        {
            var eventEntity = _context.Events.Find(id);
            if (eventEntity == null)
            {
                return NotFound();
            }

            _context.Events.Remove(eventEntity);
            _context.SaveChanges();

            return eventEntity;
        }

        private bool EventExists(int id)
        {
            return _context.Events.Any(e => e.EventID == id);
        }
    }
}
