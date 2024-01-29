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
    public class PlacesController : ControllerBase
    {
        private readonly SylvainBretonDbContext _context;

        public PlacesController(SylvainBretonDbContext context)
        {
            _context = context;
        }

        // GET: api/Places
        [HttpGet]
        public ActionResult<IEnumerable<Place>> GetPlaces()
        {
            return _context.Places
                .Include(p => p.Performances)
                .Include(p => p.Events)
                .ToList();
        }

        // GET: api/Places/5
        [HttpGet("{id}")]
        public ActionResult<Place> GetPlace(int id)
        {
            var place = _context.Places.Find(id);

            if (place == null)
            {
                return NotFound();
            }

            return place;
        }

        // POST: api/Places
        [HttpPost]
        public ActionResult<Place> PostPlace(Place place)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Places.Add(place);
            _context.SaveChanges();

            return CreatedAtAction("GetPlace", new { id = place.PlaceID }, place);
        }

        // PUT: api/Places/5
        [HttpPut("{id}")]
        public IActionResult PutPlace(int id, Place place)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != place.PlaceID)
            {
                return BadRequest();
            }

            _context.Entry(place).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlaceExists(id))
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

        // DELETE: api/Places/5
        [HttpDelete("{id}")]
        public ActionResult<Place> DeletePlace(int id)
        {
            var place = _context.Places.Find(id);
            if (place == null)
            {
                return NotFound();
            }

            _context.Places.Remove(place);
            _context.SaveChanges();

            return place;
        }

        private bool PlaceExists(int id)
        {
            return _context.Places.Any(e => e.PlaceID == id);
        }
    }
}
