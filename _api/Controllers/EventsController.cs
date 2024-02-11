namespace api_sylvainbreton.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using api_sylvainbreton.Models;
    using api_sylvainbreton.Data;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.AspNetCore.Authorization;

    [Route("api/[controller]")]
    [ApiController]
    public class EventsController(SylvainBretonDbContext context) : ControllerBase
    {
        private readonly SylvainBretonDbContext _context = context;

        // GET: api/Events
        [HttpGet]
        public ActionResult<IEnumerable<Event>> GetEvents()
        {
            return _context.Events
                .Include(e => e.Place)
                .Include(e => e.EventArtworks)
                    .ThenInclude(ea => ea.Artwork)
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
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult<Event> PostEvent(Event eventEntity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Events.Add(eventEntity);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetEvent), new { id = eventEntity.EventID }, eventEntity);
        }

        // PUT: api/Events/5
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public IActionResult PutEvent(int id, Event eventEntity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

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
        [Authorize(Roles = "Admin")]
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
