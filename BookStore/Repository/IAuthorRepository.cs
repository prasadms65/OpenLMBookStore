using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookStore.Dto;
using BookStore.Models;

namespace BookStore.Repository
{
    public interface IAuthorRepository
    {
        Task<List<AuthorsDto>> GetAllAuthors();
        Task<AuthorsDto> GetAuthorById(long authorId);
        Task<bool> UpdateAuthor(long authorId, AuthorsDto authorDto);
        Task<bool> SaveAuthor(Authors authors);
        Task<bool> DeleteAuthor(int id);
        bool AuthorExists(int id);
    }
}
