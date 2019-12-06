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
    public class CategoryRepository : BaseRepository, ICategoryRepository
    {
        public CategoryRepository(LibraryAdminContext context) : base(context)
        {

        }

        public async Task AddAsync(Category category)
        {
            await _context.Categories.AddAsync(category);
        }

        public async Task<Category> FindByIdAsync(int id)
        {
            return await _context.Categories
                .Include(x => x.Books)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Category>> ListAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public void Remove(Category category)
        {
            _context.Categories.Remove(category);
        }

        public void Update(Category category)
        {
            _context.Categories.Update(category);
        }
    }
}
