using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Domain.Communication;
using WebAPI.Domain.Models;

namespace WebAPI.Domain.Services
{
    public interface IAuthorService
    {
        Task<IEnumerable<Author>> ListAsync();
        Task<AuthorResponse> FindByIdAsync(int id);
        Task<AuthorResponse> SaveAsync(Author author);
        Task<AuthorResponse> UpdateAsync(int id, Author author);
        Task<AuthorResponse> DeleteAsync(int id);
    }
}
