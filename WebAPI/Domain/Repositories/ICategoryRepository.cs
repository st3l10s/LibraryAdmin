using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Domain.Models;

namespace WebAPI.Domain.Repositories
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> ListAsync();
        Task<Category> FindByIdAsync(int id);
        Task AddAsync(Category category);
        void Update(Category category);
        void Remove(Category category);
    }
}
