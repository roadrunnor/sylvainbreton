namespace api_sylvainbreton.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using api_sylvainbreton.Data;
    using api_sylvainbreton.Models;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.Extensions.Logging;

    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly SylvainBretonDbContext _context;
        private readonly ILogger<CategoriesController> _logger;

        private const string Log_RequestReceived = "Request received for {ActionName}";
        private const string Log_RequestReceivedWithId = "{ActionName} request for id {Id} has been found";
        private const string Log_RequestNotFound = "{ActionName} request for id {Id} not found";
        private const string Log_ProcessingError = "Error processing {ActionName} request for id {Id}";
        private const string Log_RequestCreated = "Category with id {CategoryId} created successfully";
        private const string Log_RequestUpdated = "Category with id {CategoryId} updated successfully";
        private const string Log_RequestDeleted = "Category with id {CategoryId} deleted successfully";

        public CategoriesController(SylvainBretonDbContext context, ILogger<CategoriesController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/Categories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            _logger.LogInformation(Log_RequestReceived, nameof(GetCategories));
            return await _context.Categories.ToListAsync();
        }

        // GET: api/Categories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategory(int id)
        {
            _logger.LogInformation(Log_RequestReceivedWithId, nameof(GetCategory), id);
            var category = await _context.Categories.FindAsync(id);

            if (category == null)
            {
                _logger.LogWarning(Log_RequestNotFound, nameof(GetCategory), id);
                return NotFound();
            }

            return category;
        }

        // POST: api/Categories
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<Category>> PostCategory(Category category)
        {
            _logger.LogInformation(Log_RequestReceived, nameof(PostCategory));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _context.Categories.Add(category);
                await _context.SaveChangesAsync();

                _logger.LogInformation(Log_RequestCreated, category.CategoryID);
                return CreatedAtAction(nameof(GetCategory), new { id = category.CategoryID }, category);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, Log_ProcessingError, nameof(PostCategory), category.CategoryID);
                return StatusCode(500, "An error occurred while creating your category request. Please try again later.");
            }
        }

        // PUT: api/Categories/5
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory(int id, Category category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != category.CategoryID)
            {
                return BadRequest();
            }

            _context.Entry(category).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                _logger.LogInformation(Log_RequestUpdated, category.CategoryID);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!CategoryExists(id))
                {
                    _logger.LogWarning(Log_RequestNotFound, nameof(PutCategory), id);
                    return NotFound();
                }
                else
                {
                    _logger.LogError(ex, Log_ProcessingError, nameof(PutCategory), id);
                    return StatusCode(500, "An error occurred while processing your category update request. Please try again later.");
                }
            }

            return NoContent();
        }

        // DELETE: api/Categories/{id}
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            _logger.LogInformation(Log_RequestReceivedWithId, nameof(DeleteCategory), id);

            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                _logger.LogWarning(Log_RequestNotFound, nameof(DeleteCategory), id);
                return NotFound();
            }

            try
            {
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
                _logger.LogInformation(Log_RequestDeleted, category.CategoryID);
                return NoContent();
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, Log_ProcessingError, nameof(DeleteCategory), id);
                return StatusCode(500, "An error occurred while deleting the category. Please try again later.");
            }
        }

        private bool CategoryExists(int id)
        {
            return _context.Categories.Any(e => e.CategoryID == id);
        }
    }
}
