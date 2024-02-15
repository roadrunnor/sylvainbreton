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
    public class ImagesController : ControllerBase
    {
        private readonly SylvainBretonDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly ILogger<ImagesController> _logger;

        private const string Log_RequestReceived = "Request received for {ActionName}";
        private const string Log_RequestReceivedWithId = "Request for ImageID {ImageId} received";
        private const string Log_RequestNotFound = "Image with ImageID {ImageId} not found";
        private const string Log_ProcessingError = "Error processing {ActionName} request for id {Id}";
        private const string Log_RequestCreated = "Image with ImageID {ImageId} created successfully";
        private const string Log_RequestUpdated = "Image with ImageID {ImageId} updated successfully";
        private const string Log_RequestDeleted = "Image with ImageID {ImageId} deleted successfully";


        public ImagesController(IConfiguration configuration, SylvainBretonDbContext context, ILogger<ImagesController> logger)
        {
            _context = context;
            _configuration = configuration;
            _logger = logger;
        }

        // GET: api/Images
        [HttpGet]
        public ActionResult<IEnumerable<ImageDTO>> GetImages()
        {
            _logger.LogInformation(Log_RequestReceived, nameof(GetImages));

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
            _logger.LogInformation(Log_RequestReceivedWithId, id);

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
                _logger.LogWarning(Log_RequestNotFound, id);
                return NotFound();
            }

            return image;
        }

        // POST: api/Images
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult<ImageDTO> PostImage([FromBody] ImageDTO imageDTO)
        {
            _logger.LogInformation(Log_RequestReceived, nameof(PostImage));
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

            try
            {
                _context.Images.Add(image);
                _context.SaveChanges();
                _logger.LogInformation(Log_RequestCreated, image.ImageID);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, Log_ProcessingError, nameof(PostImage), image.ImageID);
                return StatusCode(500, "An error occurred while processing your image addon request. Please try again later.");
            }

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
            _logger.LogInformation(Log_RequestReceivedWithId, id);

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
                _logger.LogWarning(Log_RequestNotFound, id);
                return NotFound();
            }

            image.ArtworkID = imageDTO.ArtworkID;
            image.PerformanceID = imageDTO.PerformanceID;
            image.FileName = Path.Combine(_configuration["IMAGE_PATH"], imageDTO.FileName);
            image.FilePath = image.FileName;
            image.URL = imageDTO.URL;

            try
            {
                _context.Entry(image).State = EntityState.Modified;
                _context.SaveChanges();
                _logger.LogInformation(Log_RequestUpdated, image.ImageID);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogError(ex, Log_ProcessingError, nameof(PutImage), id);
                return StatusCode(500, "An error occurred while processing your image update request. Please try again later.");
            }

            return NoContent();
        }
        
        // DELETE: api/Images/5
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public ActionResult<ImageDTO> DeleteImage(int id)
        {
            _logger.LogInformation(Log_RequestReceivedWithId, id);

            var image = _context.Images.Find(id);
            if (image == null)
            {
                _logger.LogWarning(Log_RequestNotFound, id);
                return NotFound();
            }

            try
            {
                _context.Images.Remove(image);
                _context.SaveChanges();
                _logger.LogInformation(Log_RequestDeleted, image.ImageID);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, Log_ProcessingError, nameof(DeleteImage), image.ImageID);
                return StatusCode(500, "An error occurred while processing your image deletion request. Please try again later.");
            }

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
