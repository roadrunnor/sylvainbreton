using Microsoft.AspNetCore.Mvc;
using api_sylvainbreton.Models;
using System.Collections.Generic;
using System.Linq;
using api_sylvainbreton.Data;
using Microsoft.EntityFrameworkCore;
using api_sylvainbreton.Models.DTOs;

namespace api_sylvainbreton.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SentencesController : ControllerBase
    {
        private readonly SylvainBretonDbContext _context;

        public SentencesController(SylvainBretonDbContext context)
        {
            _context = context;
        }

        // GET: api/Sentences
        [HttpGet]
        public ActionResult<IEnumerable<Sentence>> GetSentences()
        {
            return _context.Sentences
                .Include(s => s.Artwork)
                .ToList();
        }

        // GET: api/Sentences/5
        [HttpGet("{id}")]
        public ActionResult<Sentence> GetSentence(int id)
        {
            var sentence = _context.Sentences
                .Include(s => s.Artwork)
                .FirstOrDefault(s => s.SentenceID == id);

            if (sentence == null)
            {
                return NotFound();
            }

            return sentence;
        }

        // GET: api/Sentences
        [HttpGet("Dto")]
        public ActionResult<IEnumerable<SentenceDTO>> GetSentencesDto()
        {
            var sentencesDto = _context.Sentences.Select(s => new SentenceDTO
            {
                SentenceID = s.SentenceID,
                Content = s.Content, // Assuming 'Content' is the correct property name
                ArtworkID = s.ArtworkID
                // Map any other necessary properties
            }).ToList();

            return Ok(sentencesDto); // This should match the variable name above
        }



        // POST: api/Sentences
        [HttpPost]
        public ActionResult<Sentence> PostSentence(Sentence sentence)
        {
            _context.Sentences.Add(sentence);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetSentence), new { id = sentence.SentenceID }, sentence);
        }

        // PUT: api/Sentences/5
        [HttpPut("{id}")]
        public IActionResult PutSentence(int id, Sentence sentence)
        {
            if (id != sentence.SentenceID)
            {
                return BadRequest();
            }

            _context.Entry(sentence).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SentenceExists(id))
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

        // DELETE: api/Sentences/5
        [HttpDelete("{id}")]
        public ActionResult<Sentence> DeleteSentence(int id)
        {
            var sentence = _context.Sentences.Find(id);
            if (sentence == null)
            {
                return NotFound();
            }

            _context.Sentences.Remove(sentence);
            _context.SaveChanges();

            return sentence;
        }

        private bool SentenceExists(int id)
        {
            return _context.Sentences.Any(s => s.SentenceID == id);
        }
    }
}
