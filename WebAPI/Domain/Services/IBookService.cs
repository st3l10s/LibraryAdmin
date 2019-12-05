using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Domain.Communication;
using WebAPI.Domain.Models;

namespace WebAPI.Domain.Services
{
    public interface IBookService
    {
        Task<IEnumerable<Book>> ListAsync();
        Task<BookResponse> FindByIdAsync(int id);
        Task<BookResponse> SaveAsync(Book book);
        Task<BookResponse> UpdateAsync(int id, Book book);
        Task<BookResponse> DeleteAsync(int id);
    }
}
