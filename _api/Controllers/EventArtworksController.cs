namespace api_sylvainbreton.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using api_sylvainbreton.Models;
    using api_sylvainbreton.Data;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.AspNetCore.Authorization;

    [Route("api/[controller]")]
    [ApiController]
    public class EventArtworksController : ControllerBase
    {
        private readonly SylvainBretonDbContext _context;
        private readonly ILogger<EventArtworksController> _logger;

        private const string Log_RequestReceived = "Request received for {ActionName}";
        private const string Log_RequestReceivedWithIds = "Request for EventID {EventId} and ArtworkID {ArtworkId} received";
        private const string Log_RequestNotFound = "EventArtwork for EventID {EventId} and ArtworkID {ArtworkId} not found";
        private const string Log_ProcessingError = "Error processing {ActionName} request for id {Id}";
        private const string Log_RequestCreated = "EventArtwork for EventID {EventId} and ArtworkID {ArtworkId} created successfully";
        private const string Log_RequestDeleted = "EventArtwork for EventID {EventId} and ArtworkID {ArtworkId} deleted successfully";

        public EventArtworksController(SylvainBretonDbContext context, ILogger<EventArtworksController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/EventArtworks
        [HttpGet]
        public ActionResult<IEnumerable<EventArtwork>> GetEventArtworks()
        {
            _logger.LogInformation(Log_RequestReceived, nameof(GetEventArtworks));
            return _context.EventArtworks
                .Include(ea => ea.Event)
                .Include(ea => ea.Artwork)
                .ToList();
        }

        // GET: api/EventArtworks/5/10
        [HttpGet("{eventId}/{artworkId}")]
        public ActionResult<EventArtwork> GetEventArtwork(int eventId, int artworkId)
        {
            _logger.LogInformation(Log_RequestReceivedWithIds, eventId, artworkId);
            var eventArtwork = _context.EventArtworks
                .Where(ea => ea.EventID == eventId && ea.ArtworkID == artworkId)
                .Include(ea => ea.Event)
                .Include(ea => ea.Artwork)
                .FirstOrDefault();

            if (eventArtwork == null)
            {
                _logger.LogWarning(Log_RequestNotFound, eventId, artworkId);
                return NotFound();
            }

            return eventArtwork;
        }

        // POST: api/EventArtworks
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult<EventArtwork> PostEventArtwork(EventArtwork eventArtwork)
        {
            _logger.LogInformation(Log_RequestReceived, nameof(PostEventArtwork));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_context.Events.Any(e => e.EventID == eventArtwork.EventID) ||
        !       _context.Artworks.Any(a => a.ArtworkID == eventArtwork.ArtworkID))
            {
                return BadRequest("The specified Event or Artwork does not exist.");
            }

            try
            {
                _context.EventArtworks.Add(eventArtwork);
                _context.SaveChanges();
                _logger.LogInformation(Log_RequestCreated, eventArtwork.EventID, eventArtwork.ArtworkID);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, Log_ProcessingError, nameof(PostEventArtwork), new { eventArtwork.EventID, eventArtwork.ArtworkID });
                return StatusCode(500, "An error occurred while processing your event addon request. Please try again later.");
            }

            return CreatedAtAction(nameof(GetEventArtwork), new { eventId = eventArtwork.EventID, artworkId = eventArtwork.ArtworkID }, eventArtwork);
        }
        
        // DELETE: api/EventArtworks/5/10
        [Authorize(Roles = "Admin")]
        [HttpDelete("{eventId}/{artworkId}")]
        public IActionResult DeleteEventArtwork(int eventId, int artworkId)
        {
            _logger.LogInformation(Log_RequestReceivedWithIds, eventId, artworkId);

            var eventArtwork = _context.EventArtworks.Find(eventId, artworkId);
            if (eventArtwork == null)
            {
                _logger.LogWarning(Log_RequestNotFound, eventId, artworkId);
                return NotFound();
            }

            try
            {
                _context.EventArtworks.Remove(eventArtwork);
                _context.SaveChanges();
                _logger.LogInformation(Log_RequestDeleted, eventId, artworkId);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, Log_ProcessingError, nameof(DeleteEventArtwork), new { eventId, artworkId });
                return StatusCode(500, "An error occurred while processing your event artwork deletion request. Please try again later.");
            }

            return NoContent();
        }

        private bool EventArtworkExists(int eventId, int artworkId)
        {
            return _context.EventArtworks.Any(ea => ea.EventID == eventId && ea.ArtworkID == artworkId);
        }
    }
}
