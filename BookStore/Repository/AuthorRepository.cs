using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BookStore.Models;
using BookStore.Dto;

namespace BookStore.Repository
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly BookStoreContext context;

        public AuthorRepository(BookStoreContext context)
        {
            this.context = context;
        }

        public async Task<List<AuthorsDto>> GetAllAuthors()
        {
            var authors = await context.Authors.ToListAsync();
            List<AuthorsDto> authorsDto = null;
            foreach (Authors author in authors)
            {
                AuthorsDto authorDto = new AuthorsDto()
                {
                    Id = author.Id,
                    Name = author.Name,
                    AdditionalInfo = author.AdditionalInfo
                };
                authorsDto.Add(authorDto);
            }
            return authorsDto;
        }

        public async Task<AuthorsDto> GetAuthorById(long authorId)
        {
            var authorDetails = await this.context.Authors.Where(a => a.Id == authorId).SingleAsync();
            AuthorsDto authorDto = new AuthorsDto()
            {
                Id = authorDetails.Id,
                Name = authorDetails.Name,
                AdditionalInfo = authorDetails.AdditionalInfo
            };
            return authorDto;
        }

        public async Task<bool> UpdateAuthor(long authorId, AuthorsDto authorDto)
        {
            var authorDetails = await this.context.Authors.Where(a => a.Id == authorId).SingleAsync();
            authorDetails.Name = authorDto.Name;
            authorDetails.AdditionalInfo = authorDto.AdditionalInfo;
            await this.context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> SaveAuthor(Authors authors)
        {
            context.Authors.Add(authors);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAuthor(int id)
        {
            var authors = await context.Authors.SingleOrDefaultAsync(m => m.Id == id);
            if (authors == null)
            {
                return false;
            }

            context.Authors.Remove(authors);
            await context.SaveChangesAsync();

            return true;
        }

        public bool AuthorExists(int id)
        {
            return context.Authors.Any(e => e.Id == id);
        }
    }
}
