namespace api_sylvainbreton.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using api_sylvainbreton.Models;
    using api_sylvainbreton.Data;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.AspNetCore.Authorization;

    [Route("api/[controller]")]
    [ApiController]
    public class PerformancesController : ControllerBase
    {
        private readonly SylvainBretonDbContext _context;
        private readonly ILogger<PerformancesController> _logger;

        private const string Log_RequestReceived = "Request received for {ActionName}";
        private const string Log_RequestReceivedWithId = "Request for PerformanceID {PerformanceId} received";
        private const string Log_RequestNotFound = "Performance with PerformanceID {PerformanceId} not found";
        private const string Log_ProcessingError = "Error processing {ActionName} request for id {Id}";
        private const string Log_RequestCreated = "Performance with PerformanceID {PerformanceId} created successfully";
        private const string Log_RequestUpdated = "Performance with PerformanceID {PerformanceId} updated successfully";
        private const string Log_RequestDeleted = "Performance with PerformanceID {PerformanceId} deleted successfully";
        public PerformancesController(SylvainBretonDbContext context, ILogger<PerformancesController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/Performances
        [HttpGet]
        public ActionResult<IEnumerable<Performance>> GetPerformances()
        {
            _logger.LogInformation(Log_RequestReceived, nameof(GetPerformances));
            return _context.Performances.ToList();
        }

        // GET: api/Performances/5
        [HttpGet("{id}")]
        public ActionResult<Performance> GetPerformance(int id)
        {
            _logger.LogInformation(Log_RequestReceivedWithId, id);
            var performance = _context.Performances.Find(id);

            if (performance == null)
            {
                _logger.LogWarning(Log_RequestNotFound, id);
                return NotFound();
            }

            return performance;
        }

        // POST: api/Performances
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult<Performance> PostPerformance(Performance performance)
        {
            _logger.LogInformation(Log_RequestCreated, performance.PerformanceID);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _context.Performances.Add(performance);
                _context.SaveChanges();
                _logger.LogInformation(Log_RequestCreated, performance.PerformanceID);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, Log_ProcessingError, nameof(PostPerformance), performance.PerformanceID);
                return StatusCode(500, "An error occurred while processing your performance addon request. Please try again later.");
            }

            return CreatedAtAction("GetPerformance", new { id = performance.PerformanceID }, performance);
        }

        // PUT: api/Performances/5
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public IActionResult PutPerformance(int id, Performance performance)
        {
            _logger.LogInformation(Log_RequestReceivedWithId, id);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != performance.PerformanceID)
            {
                return BadRequest();
            }

            try
            {
                _context.Entry(performance).State = EntityState.Modified;
                _logger.LogInformation(Log_RequestUpdated, performance.PerformanceID);
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!PerformanceExists(id))
                {
                    _logger.LogWarning(Log_RequestNotFound, id);
                    return NotFound();
                }
                else
                {
                    _logger.LogError(ex, Log_ProcessingError, nameof(PutPerformance), id);
                    return StatusCode(500, "An error occurred while processing your performance update request. Please try again later.");
                }
            }

            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public ActionResult<Performance> DeletePerformance(int id)
        {
            _logger.LogInformation(Log_RequestReceivedWithId, id);
            var performance = _context.Performances.Find(id);
            if (performance == null)
            {
                _logger.LogWarning(Log_RequestNotFound, id);
                return NotFound();
            }

            try
            {
                _context.Performances.Remove(performance);
                _context.SaveChanges();
                _logger.LogInformation(Log_RequestDeleted, performance.PerformanceID);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, Log_ProcessingError, nameof(DeletePerformance), id);
                return StatusCode(500, "An error occurred while processing your performance deletion request. Please try again later.");
            }

            return performance;
        }


        private bool PerformanceExists(int id)
        {
            return _context.Performances.Any(e => e.PerformanceID == id);
        }
    }
}
