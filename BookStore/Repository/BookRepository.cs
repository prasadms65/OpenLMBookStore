using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BookStore.Models;
using BookStore.Dto;

namespace BookStore.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly BookStoreContext context;

        public BookRepository(BookStoreContext context)
        {
            this.context = context;
        }

        public async Task<List<BooksDto>> GetAllBooks()
        {
            var books = await context.Books.ToListAsync();
            List<BooksDto> booksDto = null;
            foreach (Books book in books)
            {
                BooksDto bookDto = new BooksDto()
                {
                    Id = book.Id,
                    Title = book.Title,
                    PublishDate = book.PublishDate,
                    Price = book.Price
                };
                booksDto.Add(bookDto);
            }
            return booksDto;
        }

        public async Task<BooksDto> GetBookById(long bookId)
        {
            var bookDetails = await this.context.Books.Where(b => b.Id == bookId).SingleAsync();
            BooksDto bookDto = new BooksDto()
            {
                Id = bookDetails.Id,
                Title = bookDetails.Title,
                PublishDate = bookDetails.PublishDate,
                Price = bookDetails.Price
            };
            return bookDto;
        }

        public async Task<bool> UpdateBook(long bookId, BooksDto bookDto)
        {
            var bookDetails = await this.context.Books.Where(b => b.Id == bookId).SingleAsync();
            bookDetails.Title = bookDto.Title;
            bookDetails.AuthorId = bookDto.AuthorId;
            bookDetails.PublishDate = bookDto.PublishDate;
            bookDetails.Price = bookDto.Price;

            await this.context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> SaveBook(Books book)
        {
            context.Books.Add(book);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteBook(int id)
        {
            var book = await context.Books.SingleOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return false;
            }
            context.Books.Remove(book);
            await context.SaveChangesAsync();

            return true;
        }

        public bool BookExists(int id)
        {
            return context.Books.Any(e => e.Id == id);
        }
    }
}
