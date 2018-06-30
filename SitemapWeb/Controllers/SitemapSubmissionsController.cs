using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SitemapWeb.Data;
using SitemapWeb.Entities;

namespace SitemapWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SitemapSubmissionsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SitemapSubmissionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/SitemapSubmissions
        [HttpGet]
        public IEnumerable<SitemapSubmission> GetSitemapSubmissions()
        {
            return _context.SitemapSubmissions;
        }

        // GET: api/SitemapSubmissions/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSitemapSubmission([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var sitemapSubmission = await _context.SitemapSubmissions.FindAsync(id);

            if (sitemapSubmission == null)
            {
                return NotFound();
            }

            return Ok(sitemapSubmission);
        }

        // PUT: api/SitemapSubmissions/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSitemapSubmission([FromRoute] int id, [FromBody] SitemapSubmission sitemapSubmission)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != sitemapSubmission.Id)
            {
                return BadRequest();
            }

            _context.Entry(sitemapSubmission).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SitemapSubmissionExists(id))
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

        // POST: api/SitemapSubmissions
        [HttpPost]
        public async Task<IActionResult> PostSitemapSubmission([FromBody] SitemapSubmission sitemapSubmission)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.SitemapSubmissions.Add(sitemapSubmission);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSitemapSubmission", new { id = sitemapSubmission.Id }, sitemapSubmission);
        }

        // DELETE: api/SitemapSubmissions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSitemapSubmission([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var sitemapSubmission = await _context.SitemapSubmissions.FindAsync(id);
            if (sitemapSubmission == null)
            {
                return NotFound();
            }

            _context.SitemapSubmissions.Remove(sitemapSubmission);
            await _context.SaveChangesAsync();

            return Ok(sitemapSubmission);
        }

        private bool SitemapSubmissionExists(int id)
        {
            return _context.SitemapSubmissions.Any(e => e.Id == id);
        }
    }
}