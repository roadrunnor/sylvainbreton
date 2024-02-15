namespace api_sylvainbreton.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using api_sylvainbreton.Models;
    using api_sylvainbreton.Data;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.AspNetCore.Authorization;

    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly SylvainBretonDbContext _context;
        private readonly ILogger<EventsController> _logger;

        private const string Log_RequestReceived = "Request received for {ActionName}";
        private const string Log_RequestReceivedWithId = "Request for EventID {EventId} received";
        private const string Log_RequestNotFound = "Event with EventID {EventId} not found";
        private const string Log_ProcessingError = "Error processing {ActionName} request for id {Id}";
        private const string Log_RequestCreated = "Event with EventID {EventId} created successfully";
        private const string Log_RequestUpdated = "Event with EventID {EventId} updated successfully";
        private const string Log_RequestDeleted = "Event with EventID {EventId} deleted successfully";

        public EventsController(SylvainBretonDbContext context, ILogger<EventsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/Events
        [HttpGet]
        public ActionResult<IEnumerable<Event>> GetEvents()
        {
            _logger.LogInformation(Log_RequestReceived, nameof(GetEvents));
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
            _logger.LogInformation(Log_RequestReceivedWithId, id); 
            var eventEntity = _context.Events
                .Include(e => e.Place) // Inclut les données associées de Place
                .Include(e => e.EventArtworks) // Inclut les relations EventArtwork
                .FirstOrDefault(e => e.EventID == id);

            if (eventEntity == null)
            {
                _logger.LogWarning(Log_RequestNotFound, id);
                return NotFound();
            }

            return eventEntity;
        }

        // POST: api/Events
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult<Event> PostEvent(Event eventEntity)
        {
            _logger.LogInformation(Log_RequestReceived, nameof(PostEvent));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _context.Events.Add(eventEntity);
                _context.SaveChanges();
                _logger.LogInformation(Log_RequestCreated, eventEntity.EventID);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, Log_ProcessingError, nameof(PostEvent), eventEntity.EventID);
                return StatusCode(500, "An error occurred while processing your event addon request. Please try again later.");
            }

            return CreatedAtAction(nameof(GetEvent), new { id = eventEntity.EventID }, eventEntity);
        }


        // PUT: api/Events/5
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public IActionResult PutEvent(int id, Event eventEntity)
        {
            _logger.LogInformation(Log_RequestReceivedWithId, id);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != eventEntity.EventID)
            {
                return BadRequest();
            }

            try
            {
                _context.Entry(eventEntity).State = EntityState.Modified;
                _context.SaveChanges();
                _logger.LogInformation(Log_RequestUpdated, eventEntity.EventID);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!EventExists(id))
                {
                    _logger.LogWarning(Log_RequestNotFound, id);
                    return NotFound();
                }
                else
                {
                    _logger.LogError(ex, Log_ProcessingError, nameof(PutEvent), id);
                    return StatusCode(500, "An error occurred while processing your event update request. Please try again later.");
                }
            }

            return NoContent();
        }

        // DELETE: api/Events/5
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public ActionResult<Event> DeleteEvent(int id)
        {
            _logger.LogInformation(Log_RequestReceivedWithId, id);
            var eventEntity = _context.Events.Find(id);
            if (eventEntity == null)
            {
                _logger.LogWarning(Log_RequestNotFound, id);
                return NotFound();
            }

            try
            {
                _context.Events.Add(eventEntity);
                _context.SaveChanges();
                _logger.LogInformation(Log_RequestDeleted, eventEntity.EventID);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, Log_ProcessingError, nameof(PostEvent), eventEntity.EventID);
                // Customized error message for event creation errors
                return StatusCode(500, "An error occurred while processing your event deletion request. Please try again later.");
            }

            return eventEntity;
        }

        private bool EventExists(int id)
        {
            return _context.Events.Any(e => e.EventID == id);
        }
    }
}
