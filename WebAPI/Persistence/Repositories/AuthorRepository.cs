using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Domain.Models;
using WebAPI.Domain.Repositories;
using WebAPI.Persistence.Contexts;

namespace WebAPI.Persistence.Repositories
{
    public class AuthorRepository : BaseRepository, IAuthorRepository
    {
        public AuthorRepository(LibraryAdminContext context) : base(context)
        {

        }

        public async Task AddAsync(Author author)
        {
            await _context.Authors.AddAsync(author);
        }

        public async Task<Author> FindByIdAsync(int id)
        {
            return await _context.Authors.FindAsync(id);
        }

        public async Task<IEnumerable<Author>> ListAsync()
        {
            return await _context.Authors.ToListAsync();
        }

        public void Remove(Author author)
        {
            _context.Authors.Remove(author);
        }

        public void Update(Author author)
        {
            _context.Authors.Update(author);
        }
    }
}
