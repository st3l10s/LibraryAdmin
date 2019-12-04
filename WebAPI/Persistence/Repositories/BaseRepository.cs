using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Persistence.Contexts;

namespace WebAPI.Persistence.Repositories
{
    public abstract class BaseRepository
    {
        protected readonly LibraryAdminContext _context;

        public BaseRepository(LibraryAdminContext context)
        {
            _context = context; 
        }
    }
}
