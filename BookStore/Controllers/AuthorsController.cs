using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BookStore.Models;
using BookStore.Repository;
using BookStore.Dto;
using Microsoft.Extensions.Logging;

namespace BookStore.Controllers
{
    [Produces("application/json")]
    [Route("api/Authors")]
    public class AuthorsController : Controller
    {
        private readonly IAuthorRepository _iAuthorRepository;
        private readonly ILogger<AuthorsController> _logger;

        public AuthorsController(IAuthorRepository iAuthorRepository, ILogger<AuthorsController> logger)
        {
            _iAuthorRepository = iAuthorRepository;
            _logger = logger;
        }

        // GET: api/Authors
        [HttpGet]
        public async Task<List<AuthorsDto>> GetAllAuthors()
        {
            return await _iAuthorRepository.GetAllAuthors();
        }

        // GET: api/Authors/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAuthors([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var authors = await _iAuthorRepository.GetAuthorById(id);

            if (authors == null)
            {
                return NotFound();
            }

            return Ok(authors);
        }

        // PUT: api/Authors/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAuthors([FromRoute] int id, [FromBody] AuthorsDto authors)
        {
            if (id != authors.Id)
            {
                return BadRequest();
            }
            try
            {
                await _iAuthorRepository.UpdateAuthor(id, authors);
            }
            catch (Exception e)
            {
                if (!_iAuthorRepository.AuthorExists(id))
                {
                    return NotFound();
                }
                else
                {
                    _logger.LogError(e, "Error in PutAuthors");
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
            await _iAuthorRepository.SaveAuthor(authors);
            return CreatedAtAction("GetAuthors", new { id = authors.Id }, authors);
        }

        // DELETE: api/Authors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthors([FromRoute] int id)
        {
            try
            {
                await _iAuthorRepository.DeleteAuthor(id);
                return Ok();
            }
            catch(Exception e)
            {
                _logger.LogError(e, "Error in PutBooks");
                return NotFound();
            }
        }
    }
}