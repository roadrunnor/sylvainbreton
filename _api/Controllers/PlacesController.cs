namespace api_sylvainbreton.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using api_sylvainbreton.Models;
    using api_sylvainbreton.Data;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.AspNetCore.Authorization;

    [Route("api/[controller]")]
    [ApiController]
    public class PlacesController : ControllerBase
    {
        private readonly SylvainBretonDbContext _context;
        private readonly ILogger<PlacesController> _logger;

        private const string Log_RequestReceived = "Request received for {ActionName}";
        private const string Log_RequestReceivedWithId = "Request for PlaceID {PlaceId} received";
        private const string Log_RequestNotFound = "Place with PlaceID {PlaceId} not found";
        private const string Log_RequestCreated = "Place with PlaceID {PlaceId} created successfully";
        private const string Log_RequestUpdated = "Place with PlaceID {PlaceId} updated successfully";
        private const string Log_RequestDeleted = "Place with PlaceID {PlaceId} deleted successfully";
        private const string Log_ProcessingError = "Error processing {ActionName} request for PlaceID {PlaceId}";

        public PlacesController(SylvainBretonDbContext context, ILogger<PlacesController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/Places
        [HttpGet]
        public ActionResult<IEnumerable<Place>> GetPlaces()
        {
            _logger.LogInformation(Log_RequestReceived, nameof(GetPlaces));

            return _context.Places
                .Include(p => p.Performances)
                .Include(p => p.Events)
                .ToList();
        }

        // GET: api/Places/5
        [HttpGet("{id}")]
        public ActionResult<Place> GetPlace(int id)
        {
            _logger.LogInformation(Log_RequestReceivedWithId, id);

            var place = _context.Places.Find(id);

            if (place == null)
            {
                _logger.LogWarning(Log_RequestNotFound, id);
                return NotFound();
            }

            return place;
        }

        // POST: api/Places
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult<Place> PostPlace(Place place)
        {
            _logger.LogInformation(Log_RequestReceived, nameof(PostPlace));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _context.Places.Add(place);
                _context.SaveChanges();
                _logger.LogInformation(Log_RequestCreated, place.PlaceID);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, Log_ProcessingError, nameof(PostPlace), place.PlaceID);
                return StatusCode(500, "An error occurred while processing your place addon request. Please try again later.");
            }

            return CreatedAtAction("GetPlace", new { id = place.PlaceID }, place);
        }

        // PUT: api/Places/5
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public IActionResult PutPlace(int id, Place place)
        {
            _logger.LogInformation(Log_RequestReceivedWithId, id);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != place.PlaceID)
            {
                return BadRequest();
            }

            try
            {
                _context.Entry(place).State = EntityState.Modified;
                _context.SaveChanges();
                _logger.LogInformation(Log_RequestUpdated, place.PlaceID);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!PlaceExists(id))
                {
                    _logger.LogWarning(Log_RequestNotFound, id);
                    return NotFound();
                }
                else
                {
                    _logger.LogError(ex, Log_ProcessingError, nameof(PutPlace), place.PlaceID);
                    return StatusCode(500, "An error occurred while processing your place update request. Please try again later.");
                }
            }

            return NoContent();
        }

        // DELETE: api/Places/5
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public ActionResult<Place> DeletePlace(int id)
        {
            _logger.LogInformation(Log_RequestReceivedWithId, id);
            var place = _context.Places.Find(id);
            if (place == null)
            {
                _logger.LogWarning(Log_RequestNotFound, id);
                return NotFound();
            }

            try
            {
                _context.Places.Remove(place);
                _context.SaveChanges();
                _logger.LogInformation(Log_RequestDeleted, place.PlaceID);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, Log_ProcessingError, nameof(DeletePlace), place.PlaceID);
                return StatusCode(500, "An error occurred while processing your place deletion request. Please try again later.");
            }

            return place;
        }

        private bool PlaceExists(int id)
        {
            return _context.Places.Any(e => e.PlaceID == id);
        }
    }
}
