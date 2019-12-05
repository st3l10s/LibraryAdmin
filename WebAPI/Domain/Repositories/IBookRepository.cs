using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Domain.Models;

namespace WebAPI.Domain.Repositories
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> ListAsync();
        Task<Book> FindByIdAsync(int id);
        Task AddAsync(Book book);
        void Update(Book book);
        void Remove(Book book);
    }
}
