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
    public class BookRepository : BaseRepository, IBookRepository
    {
        public BookRepository(LibraryAdminContext context) : base(context)
        {

        }

        public async Task AddAsync(Book book)
        {
            await _context.Books.AddAsync(book);
        }

        public async Task<Book> FindByIdAsync(int id)
        {
            return await _context.Books
                .Include(x => x.Categories)
                .Include(x => x.Author)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Book>> ListAsync()
        {
            return await _context.Books.ToListAsync();
        }

        public void Remove(Book book)
        {
            _context.Books.Remove(book);
        }

        public void Update(Book book)
        {
            _context.Books.Update(book);
        }
    }
}
