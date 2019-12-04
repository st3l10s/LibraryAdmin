using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Domain.Repositories;
using WebAPI.Persistence.Contexts;

namespace WebAPI.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly LibraryAdminContext _context;

        public UnitOfWork(LibraryAdminContext context)
        {
            _context = context;
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
