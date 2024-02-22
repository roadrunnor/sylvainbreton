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
    public class SentencesController(SylvainBretonDbContext context, ILogger<SentencesController> logger) : ControllerBase
    {
        private readonly SylvainBretonDbContext _context = context;
        private readonly ILogger<SentencesController> _logger = logger;

        private const string Log_RequestReceived = "Request received for {ActionName}";
        private const string Log_RequestReceivedWithId = "Request for SentenceID {SentenceId} received";
        private const string Log_RequestNotFound = "Sentence with SentenceID {SentenceId} not found";
        private const string Log_RequestCreated = "Sentence with SentenceID {SentenceId} created successfully";
        private const string Log_RequestUpdated = "Sentence with SentenceID {SentenceId} updated successfully";
        private const string Log_RequestDeleted = "Sentence with SentenceID {SentenceId} deleted successfully";
        private const string Log_ProcessingError = "Error processing {ActionName} request for SentenceID {SentenceId}";

        // GET: api/Sentences
        [HttpGet]
        public ActionResult<IEnumerable<SentenceDTO>> GetSentences()
        {
            _logger.LogInformation(Log_RequestReceived, nameof(GetSentences));

            var sentences = _context.Sentences
                .Select(s => new SentenceDTO
                {
                    SentenceID = s.SentenceID,
                    ArtworkID = s.ArtworkID,
                    Author = s.Author,
                    PublicationDate = s.PublicationDate,
                    BookTitle = s.BookTitle,
                    Publisher = s.Publisher,
                    SentencePage = s.SentencePage,
                    Content = s.Content,
                    CountryOfPublication = s.CountryOfPublication,
                    CityOfPublication = s.CityOfPublication,

                })
                .ToList();

            return sentences;
        }

        // GET: api/Sentences/5
        [HttpGet("{id}")]
        public ActionResult<SentenceDTO> GetSentence(int id)
        {
            _logger.LogInformation(Log_RequestReceivedWithId, id);

            var sentence = _context.Sentences
                .Where(s => s.SentenceID == id)
                .Select(s => new SentenceDTO
                {
                    SentenceID = s.SentenceID,
                    Content = s.Content,
                    ArtworkID = s.ArtworkID
                })
                .FirstOrDefault();

            if (sentence == null)
            {
                _logger.LogWarning(Log_RequestNotFound, id);
                return NotFound();
            }

            return sentence;
        }

        // POST: api/Sentences
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult<SentenceDTO> PostSentence([FromBody] SentenceDTO sentenceDto)
        {
            _logger.LogInformation(Log_RequestReceived, nameof(PostSentence));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var sentence = new Sentence
            {
                Content = sentenceDto.Content,
                ArtworkID = sentenceDto.ArtworkID
            };


            try
            {
                _context.Sentences.Add(sentence);
                _context.SaveChanges();
                _logger.LogInformation(Log_RequestCreated, sentence.SentenceID);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, Log_ProcessingError, nameof(PostSentence), sentence.SentenceID);
                return StatusCode(500, "An error occurred while processing your sentence addon request. Please try again later.");
            }

            return CreatedAtAction(nameof(GetSentence), new { id = sentence.SentenceID }, new SentenceDTO
            {
                SentenceID = sentence.SentenceID,
                Content = sentence.Content,
                ArtworkID = sentence.ArtworkID
            });
        }

        // PUT: api/Sentences/5
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public IActionResult PutSentence(int id, [FromBody] SentenceDTO sentenceDto)
        {
            _logger.LogInformation(Log_RequestReceivedWithId, id);

            if (ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != sentenceDto.SentenceID)
            {
                return BadRequest();
            }

            var sentence = _context.Sentences.Find(id);
            if (sentence == null)
            {
                _logger.LogWarning(Log_RequestNotFound, id);
                return NotFound();
            }

            sentence.Content = sentenceDto.Content;
            sentence.ArtworkID = sentenceDto.ArtworkID;

            try
            {
                _context.Entry(sentence).State = EntityState.Modified;
                _context.SaveChanges();
                _logger.LogInformation(Log_RequestUpdated, sentence.SentenceID);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogError(ex, Log_ProcessingError, nameof(PutSentence), sentence.SentenceID);
                return StatusCode(500, "An error occurred while processing your sentence update request. Please try again later.");
            }

            return NoContent();
        }

        // DELETE: api/Sentences/5
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public ActionResult<SentenceDTO> DeleteSentence(int id)
        {
            _logger.LogInformation(Log_RequestReceivedWithId, id);

            var sentence = _context.Sentences.Find(id);
            if (sentence == null)
            {
                _logger.LogWarning(Log_RequestNotFound, id);
                return NotFound();
            }

            try
            {
                _context.Sentences.Remove(sentence);
                _context.SaveChanges();
                _logger.LogInformation(Log_RequestDeleted, sentence.SentenceID);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, Log_ProcessingError, nameof(DeleteSentence), sentence.SentenceID);
                return StatusCode(500, "An error occurred while processing your sentence deletion request. Please try again later.");
            }

            return new SentenceDTO
            {
                SentenceID = sentence.SentenceID,
                Content = sentence.Content,
                ArtworkID = sentence.ArtworkID
            };
        }

        private bool SentenceExists(int id)
        {
            return _context.Sentences.Any(s => s.SentenceID == id);
        }
    }
}