namespace api_sylvainbreton.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using api_sylvainbreton.Models;
    using api_sylvainbreton.Models.DTOs;
    using api_sylvainbreton.Data;
    using Microsoft.EntityFrameworkCore;

    [Route("api/[controller]")]
    [ApiController]
    public class SentencesController(SylvainBretonDbContext context) : ControllerBase
    {
        private readonly SylvainBretonDbContext _context = context;

        [HttpGet]
        public ActionResult<IEnumerable<SentenceDTO>> GetSentences()
        {
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

        [HttpGet("{id}")]
        public ActionResult<SentenceDTO> GetSentence(int id)
        {
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
                return NotFound();
            }

            return sentence;
        }

        [HttpPost]
        public ActionResult<SentenceDTO> PostSentence([FromBody] SentenceDTO sentenceDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var sentence = new Sentence
            {
                Content = sentenceDto.Content,
                ArtworkID = sentenceDto.ArtworkID
            };

            _context.Sentences.Add(sentence);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetSentence), new { id = sentence.SentenceID }, new SentenceDTO
            {
                SentenceID = sentence.SentenceID,
                Content = sentence.Content,
                ArtworkID = sentence.ArtworkID
            });
        }

        [HttpPut("{id}")]
        public IActionResult PutSentence(int id, [FromBody] SentenceDTO sentenceDto)
        {
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
                return NotFound();
            }

            sentence.Content = sentenceDto.Content;
            sentence.ArtworkID = sentenceDto.ArtworkID;

            _context.Entry(sentence).State = EntityState.Modified;
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult<SentenceDTO> DeleteSentence(int id)
        {
            var sentence = _context.Sentences.Find(id);
            if (sentence == null)
            {
                return NotFound();
            }

            _context.Sentences.Remove(sentence);
            _context.SaveChanges();

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