using Microsoft.AspNetCore.Mvc;
using api_sylvainbreton.Models;
using System.Collections.Generic;
using System.Linq;
using api_sylvainbreton.Data;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace api_sylvainbreton.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly SylvainBretonDbContext _context;

        // Modifiez le constructeur pour inclure IConfiguration configuration
        public ImagesController(IConfiguration configuration, SylvainBretonDbContext context)
        {
            _configuration = configuration; // Affectez le paramètre configuration à _configuration
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
        public ActionResult<Image> PostImage([FromBody] ImageDTO imageDTO) // Utilisez ImageDto si vous avez besoin de valider ou de manipuler les données avant de les insérer
        {
            // Construire le chemin de l'image en utilisant la variable d'environnement
            var imagePath = Path.Combine(_configuration["IMAGE_PATH"], imageDTO.FileName);

            var image = new Image
            {
                // Supposons que ImageDto a des propriétés comme FileName, Description, etc.
                FileRoute = imagePath,
                Description = imageDTO.Description,
                MediaType = imageDTO.MediaType,
                MediaDescription = imageDTO.MediaDescription,
                // ... autres propriétés si nécessaire
            };

            _context.Images.Add(image);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetImage), new { id = image.ImageID }, image);
        }

        // POST: api/Images/UploadWithEnvPath
        [HttpPost("UploadWithEnvPath")]
        public ActionResult<Image> PostImageWithEnvPath([FromBody] ImageDTO imageDTO)
        {
            // Construisez le chemin de l'image en utilisant la variable d'environnement
            // Assurez-vous que la variable IMAGE_PATH est définie dans votre fichier appsettings.json
            var imagePath = Path.Combine(_configuration["IMAGE_PATH"], imageDTO.FileName);

            var image = new Image
            {
                FileRoute = imagePath,
                Description = imageDTO.Description,
                MediaType = imageDTO.MediaType,
                MediaDescription = imageDTO.MediaDescription,
                // Assignez d'autres propriétés si nécessaire...
            };

            // Ajoutez l'objet image au contexte de la base de données et enregistrez les changements
            _context.Images.Add(image);
            _context.SaveChanges();

            // Retournez une réponse HTTP 201 - Created, avec l'URL de l'image nouvellement créée et l'objet image
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
