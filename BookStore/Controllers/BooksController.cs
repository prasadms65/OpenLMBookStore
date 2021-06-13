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
    [Route("api/Books")]
    public class BooksController : Controller
    {
        private readonly IBookRepository _iBookRepository;
        private readonly ILogger<BooksController> _logger;

        public BooksController(IBookRepository iBookRepository, ILogger<BooksController> logger)
        {
            _iBookRepository = iBookRepository;
            _logger = logger;
        }

        // GET: api/Books
        [HttpGet]
        public Task<List<BooksDto>> GetBooks()
        {
            return _iBookRepository.GetAllBooks();
        }

        // GET: api/Books/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBooks([FromRoute] int id)
        {
            var books = await _iBookRepository.GetBookById(id);
            if (books == null)
            {
                return NotFound();
            }
            return Ok(books);
        }

        // PUT: api/Books/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBooks([FromRoute] int id, [FromBody] BooksDto books)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != books.Id)
            {
                return BadRequest();
            }
            try
            {
                await _iBookRepository.UpdateBook(id, books);
            }
            catch (Exception e)
            {
                if (!_iBookRepository.BookExists(id))
                {
                    return NotFound();
                }
                else
                {
                    _logger.LogError(e, "Error in PutBooks");
                    throw;
                }
            }
            return NoContent();
        }

        // POST: api/Books
        [HttpPost]
        public async Task<IActionResult> PostBooks([FromBody] Books books)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _iBookRepository.SaveBook(books);
            return CreatedAtAction("GetBooks", new { id = books.Id }, books);
        }

        // DELETE: api/Books/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBooks([FromRoute] int id)
        {
            try
            {
                await _iBookRepository.DeleteBook(id);
                return Ok();
            }
            catch(Exception e)
            {
                _logger.LogError(e, "Error in DeleteBooks");
                return NotFound();
            }
        }
    }
}