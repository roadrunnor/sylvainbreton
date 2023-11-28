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
    public class ImagesController : ControllerBase
    {
        private readonly SylvainBretonDbContext _context;

        public ImagesController(SylvainBretonDbContext context)
        {
            _context = context;
        }

        // GET: api/Images
        [HttpGet]
        public ActionResult<IEnumerable<Image>> GetImages()
        {
            return _context.Images
                .Include(i => i.Artwork)
                .Include(i => i.Performance)
                .ToList();
        }

        // GET: api/Images/5
        [HttpGet("{id}")]
        public ActionResult<Image> GetImage(int id)
        {
            var image = _context.Images
                .Include(i => i.Artwork)
                .Include(i => i.Performance)
                .FirstOrDefault(i => i.ImageID == id);

            if (image == null)
            {
                return NotFound();
            }

            return image;
        }

        // POST: api/Images
        [HttpPost]
        public ActionResult<Image> PostImage(Image image)
        {
            _context.Images.Add(image);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetImage), new { id = image.ImageID }, image);
        }

        // PUT: api/Images/5
        [HttpPut("{id}")]
        public IActionResult PutImage(int id, Image image)
        {
            if (id != image.ImageID)
            {
                return BadRequest();
            }

            _context.Entry(image).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ImageExists(id))
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

        // DELETE: api/Images/5
        [HttpDelete("{id}")]
        public ActionResult<Image> DeleteImage(int id)
        {
            var image = _context.Images.Find(id);
            if (image == null)
            {
                return NotFound();
            }

            _context.Images.Remove(image);
            _context.SaveChanges();

            return image;
        }

        private bool ImageExists(int id)
        {
            return _context.Images.Any(i => i.ImageID == id);
        }
    }
}
