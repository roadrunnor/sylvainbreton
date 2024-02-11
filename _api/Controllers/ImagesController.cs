namespace api_sylvainbreton.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using api_sylvainbreton.Models;
    using api_sylvainbreton.Models.DTOs;
    using api_sylvainbreton.Data;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.AspNetCore.Authorization;

    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController(IConfiguration configuration, SylvainBretonDbContext context) : ControllerBase
    {
        private readonly SylvainBretonDbContext _context = context;
        private readonly IConfiguration _configuration = configuration;

        // GET: api/Images
        [HttpGet]
        public ActionResult<IEnumerable<ImageDTO>> GetImages()
        {
            var images = _context.Images
                .Include(i => i.Artwork)
                .Include(i => i.Performance)
                .Select(i => new ImageDTO
                {
                    ImageID = i.ImageID,
                    ArtworkID = i.ArtworkID,
                    PerformanceID = i.PerformanceID,
                    FileName = i.FileName,
                    FilePath = i.FilePath,
                    URL = i.URL
                })
                .ToList();

            return images;
        }
        
        // GET: api/Images/5
        [HttpGet("{id}")]
        public ActionResult<ImageDTO> GetImage(int id)
        {
            var image = _context.Images
                .Include(i => i.Artwork)
                .Include(i => i.Performance)
                .Where(i => i.ImageID == id)
                .Select(i => new ImageDTO
                {
                    ImageID = i.ImageID,
                    ArtworkID = i.ArtworkID,
                    PerformanceID = i.PerformanceID,
                    FileName = i.FileName,
                    FilePath = i.FilePath,
                    URL = i.URL
                })
                .FirstOrDefault();

            if (image == null)
            {
                return NotFound();
            }

            return image;
        }

        // POST: api/Images
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult<ImageDTO> PostImage([FromBody] ImageDTO imageDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var imagePath = Path.Combine(_configuration["IMAGE_PATH"], imageDTO.FileName);

            var image = new Image
            {
                ArtworkID = imageDTO.ArtworkID,
                PerformanceID = imageDTO.PerformanceID,
                FileName = imagePath,
                FilePath = imagePath,
                URL = imageDTO.URL
            };

            _context.Images.Add(image);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetImage), new { id = image.ImageID }, new ImageDTO
            {
                ImageID = image.ImageID,
                ArtworkID = image.ArtworkID,
                PerformanceID = image.PerformanceID,
                FileName = image.FileName,
                FilePath = image.FilePath,
                URL = image.URL
            });
        }

        // PUT: api/Images
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public IActionResult PutImage(int id, [FromBody] ImageDTO imageDTO)
        {
            if (ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != imageDTO.ImageID)
            {
                return BadRequest();
            }

            var image = _context.Images.Find(id);
            if (image == null)
            {
                return NotFound();
            }

            image.ArtworkID = imageDTO.ArtworkID;
            image.PerformanceID = imageDTO.PerformanceID;
            image.FileName = Path.Combine(_configuration["IMAGE_PATH"], imageDTO.FileName);
            image.FilePath = image.FileName;
            image.URL = imageDTO.URL;

            _context.Entry(image).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return NoContent();
        }
        
        // DELETE: api/Images/5
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public ActionResult<ImageDTO> DeleteImage(int id)
        {
            var image = _context.Images.Find(id);
            if (image == null)
            {
                return NotFound();
            }

            _context.Images.Remove(image);
            _context.SaveChanges();

            return new ImageDTO
            {
                ImageID = image.ImageID,
                ArtworkID = image.ArtworkID,
                PerformanceID = image.PerformanceID,
                FileName = image.FileName,
                FilePath = image.FilePath,
                URL = image.URL
            };
        }

        private bool ImageExists(int id)
        {
            return _context.Images.Any(i => i.ImageID == id);
        }
    }
}
