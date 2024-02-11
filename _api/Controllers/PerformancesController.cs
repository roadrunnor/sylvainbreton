namespace api_sylvainbreton.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using api_sylvainbreton.Models;
    using api_sylvainbreton.Data;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.AspNetCore.Authorization;

    [Route("api/[controller]")]
    [ApiController]
    public class PerformancesController(SylvainBretonDbContext context) : ControllerBase
    {
        private readonly SylvainBretonDbContext _context = context;

        // GET: api/Performances
        [HttpGet]
        public ActionResult<IEnumerable<Performance>> GetPerformances()
        {
            return _context.Performances.ToList();
        }

        // GET: api/Performances/5
        [HttpGet("{id}")]
        public ActionResult<Performance> GetPerformance(int id)
        {
            var performance = _context.Performances.Find(id);

            if (performance == null)
            {
                return NotFound();
            }

            return performance;
        }

        // POST: api/Performances
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult<Performance> PostPerformance(Performance performance)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Performances.Add(performance);
            _context.SaveChanges();

            return CreatedAtAction("GetPerformance", new { id = performance.PerformanceID }, performance);
        }

        // PUT: api/Performances/5
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public IActionResult PutPerformance(int id, Performance performance)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != performance.PerformanceID)
            {
                return BadRequest();
            }

            _context.Entry(performance).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PerformanceExists(id))
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

        // DELETE: api/Performances/5
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public ActionResult<Performance> DeletePerformance(int id)
        {
            var performance = _context.Performances.Find(id);
            if (performance == null)
            {
                return NotFound();
            }

            _context.Performances.Remove(performance);
            _context.SaveChanges();

            return performance;
        }

        private bool PerformanceExists(int id)
        {
            return _context.Performances.Any(e => e.PerformanceID == id);
        }
    }
}
