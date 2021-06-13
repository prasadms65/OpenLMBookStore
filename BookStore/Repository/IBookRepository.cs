using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookStore.Dto;
using BookStore.Models;

namespace BookStore.Repository
{
    public interface IBookRepository
    {
        Task<List<BooksDto>> GetAllBooks();
        Task<BooksDto> GetBookById(long bookId);
        Task<bool> UpdateBook(long bookId, BooksDto bookDto);
        Task<bool> SaveBook(Books book);
        Task<bool> DeleteBook(int id);
        bool BookExists(int id);
    }
}
