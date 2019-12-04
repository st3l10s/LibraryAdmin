using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Domain.Models;

namespace WebAPI.Domain.Repositories
{
    public interface IAuthorRepository
    {
        Task<IEnumerable<Author>> ListAsync();
        Task<Author> FindByIdAsync(int id);
        Task AddAsync(Author author);
        void Update(Author author);
        void Remove(Author author);
    }
}
