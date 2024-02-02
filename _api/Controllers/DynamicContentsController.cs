namespace api_sylvainbreton.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using api_sylvainbreton.Models;
    using api_sylvainbreton.Data;

    [Route("api/[controller]")]
    [ApiController]
    public class DynamicContentsController(SylvainBretonDbContext context) : ControllerBase
    {
        private readonly SylvainBretonDbContext _context = context;

        // GET: api/DynamicContents
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DynamicContent>>> GetDynamicContents()
        {
            return await _context.DynamicContents.ToListAsync();
        }

        // GET: api/DynamicContents/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DynamicContent>> GetDynamicContent(int id)
        {
            var dynamicContent = await _context.DynamicContents.FindAsync(id);

            if (dynamicContent == null)
            {
                return NotFound();
            }

            return dynamicContent;
        }

        // POST: api/DynamicContents
        [HttpPost]
        public async Task<ActionResult<DynamicContent>> PostDynamicContent(DynamicContent dynamicContent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.DynamicContents.Add(dynamicContent);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDynamicContent", new { id = dynamicContent.ContentID }, dynamicContent);
        }

        // PUT: api/DynamicContents/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDynamicContent(int id, DynamicContent dynamicContent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != dynamicContent.ContentID)
            {
                return BadRequest();
            }

            _context.Entry(dynamicContent).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DynamicContentExists(id))
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

        // DELETE: api/DynamicContents/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDynamicContent(int id)
        {
            var dynamicContent = await _context.DynamicContents.FindAsync(id);
            if (dynamicContent == null)
            {
                return NotFound();
            }

            _context.DynamicContents.Remove(dynamicContent);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DynamicContentExists(int id)
        {
            return _context.DynamicContents.Any(e => e.ContentID == id);
        }
    }
}

