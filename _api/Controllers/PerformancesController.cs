﻿using Microsoft.AspNetCore.Mvc;
using api_sylvainbreton.Models;
using System.Collections.Generic;
using System.Linq;
using api_sylvainbreton.Data;
using Microsoft.EntityFrameworkCore;

namespace api_sylvainbreton.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PerformancesController : ControllerBase
    {
        private readonly SylvainBretonDbContext _context;

        public PerformancesController(SylvainBretonDbContext context)
        {
            _context = context;
        }

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
        [HttpPost]
        public ActionResult<Performance> PostPerformance(Performance performance)
        {
            _context.Performances.Add(performance);
            _context.SaveChanges();

            return CreatedAtAction("GetPerformance", new { id = performance.PerformanceID }, performance);
        }

        // PUT: api/Performances/5
        [HttpPut("{id}")]
        public IActionResult PutPerformance(int id, Performance performance)
        {
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
