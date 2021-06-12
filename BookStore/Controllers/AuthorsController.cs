using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookStore.Models;

namespace BookStore.Controllers
{
    [Produces("application/json")]
    [Route("api/Authors")]
    public class AuthorsController : Controller
    {
        private readonly BookStoreContext _context;

        public AuthorsController(BookStoreContext context)
        {
            _context = context;
        }

        // GET: api/Authors
        [HttpGet]
        public IEnumerable<Authors> GetAuthors()
        {
            return _context.Authors;
        }

        // GET: api/Authors/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAuthors([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var authors = await _context.Authors.SingleOrDefaultAsync(m => m.Id == id);

            if (authors == null)
            {
                return NotFound();
            }

            return Ok(authors);
        }

        // PUT: api/Authors/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAuthors([FromRoute] int id, [FromBody] Authors authors)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != authors.Id)
            {
                return BadRequest();
            }

            _context.Entry(authors).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AuthorsExists(id))
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

        // POST: api/Authors
        [HttpPost]
        public async Task<IActionResult> PostAuthors([FromBody] Authors authors)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Authors.Add(authors);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAuthors", new { id = authors.Id }, authors);
        }

        // DELETE: api/Authors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthors([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var authors = await _context.Authors.SingleOrDefaultAsync(m => m.Id == id);
            if (authors == null)
            {
                return NotFound();
            }

            _context.Authors.Remove(authors);
            await _context.SaveChangesAsync();

            return Ok(authors);
        }

        private bool AuthorsExists(int id)
        {
            return _context.Authors.Any(e => e.Id == id);
        }
    }
}