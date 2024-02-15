namespace api_sylvainbreton.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using api_sylvainbreton.Models;
    using api_sylvainbreton.Data;
    using Microsoft.AspNetCore.Authorization;

    [Route("api/[controller]")]
    [ApiController]
    public class DynamicContentsController : ControllerBase
    {
        private readonly SylvainBretonDbContext _context;
        private readonly ILogger<ArtistsController> _logger;

        private const string Log_RequestReceived = "Request received for {ActionName}";
        private const string Log_RequestReceivedWithId = "{ActionName} request for id {Id} has been found";
        private const string Log_RequestNotFound = "{ActionName} request for id {Id} not found";
        private const string Log_ProcessingError = "Error processing {ActionName} request for id {Id}";
        private const string Log_RequestCreated = "Category with id {ContentID} created successfully";
        private const string Log_RequestUpdated = "Category with id {ContentID} updated successfully";
        private const string Log_RequestDeleted = "Category with id {ContentID} deleted successfully";

        public DynamicContentsController(SylvainBretonDbContext context, ILogger<ArtistsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/DynamicContents
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DynamicContent>>> GetDynamicContents()
        {
            _logger.LogInformation(Log_RequestReceived, nameof(GetDynamicContent));
            return await _context.DynamicContents.ToListAsync();
        }

        // GET: api/DynamicContents/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DynamicContent>> GetDynamicContent(int id)
        {
            _logger.LogInformation(Log_RequestReceivedWithId, nameof(GetDynamicContent), id);
            var dynamicContent = await _context.DynamicContents.FindAsync(id);

            if (dynamicContent == null)
            {
                _logger.LogInformation(Log_RequestNotFound, nameof(GetDynamicContent), id);
                return NotFound();
            }

            return dynamicContent;
        }

        // POST: api/DynamicContents
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<DynamicContent>> PostDynamicContent(DynamicContent dynamicContent)
        {
            _logger.LogInformation(Log_RequestCreated, dynamicContent.ContentID);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _context.DynamicContents.Add(dynamicContent);
                await _context.SaveChangesAsync();
                _logger.LogInformation(Log_RequestCreated, dynamicContent.ContentID);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, Log_ProcessingError, nameof(PostDynamicContent), dynamicContent.ContentID);
                return StatusCode(500, "An error occurred while processing your dynamic content addon request. Please try again later.");
            }

            return CreatedAtAction("GetDynamicContent", new { id = dynamicContent.ContentID }, dynamicContent);
        }

        // PUT: api/DynamicContents/5
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDynamicContent(int id, DynamicContent dynamicContent)
        {
            _logger.LogInformation(Log_RequestReceivedWithId, nameof(PutDynamicContent), id);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != dynamicContent.ContentID)
            {
                return BadRequest();
            }


            try
            {
                _context.Entry(dynamicContent).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                _logger.LogInformation(Log_RequestUpdated, id);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!DynamicContentExists(id))
                {
                    _logger.LogWarning(Log_RequestNotFound, nameof(PutDynamicContent), id);
                    return NotFound();
                }
                else
                {
                    _logger.LogError(ex, Log_ProcessingError, nameof(PutDynamicContent), id);
                    return StatusCode(500, "An error occurred while processing your dynamic content update request. Please try again later.");
                }
            }

            return NoContent();
        }

        // DELETE: api/DynamicContents/5
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDynamicContent(int id)
        {
            _logger.LogInformation(Log_RequestReceivedWithId, nameof(DeleteDynamicContent), id);
            var dynamicContent = await _context.DynamicContents.FindAsync(id);
            if (dynamicContent == null)
            {
                _logger.LogWarning(Log_RequestNotFound, nameof(DeleteDynamicContent), id);
                return NotFound();
            }

            try
            {
                _context.DynamicContents.Remove(dynamicContent);
                await _context.SaveChangesAsync();
                _logger.LogInformation(Log_RequestDeleted, id);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, Log_ProcessingError, nameof(DeleteDynamicContent), id);
                return StatusCode(500, "An error occurred while processing your dynamic content deletion request. Please try again later.");
            }

            return NoContent();
        }

        private bool DynamicContentExists(int id)
        {
            return _context.DynamicContents.Any(e => e.ContentID == id);
        }
    }
}

