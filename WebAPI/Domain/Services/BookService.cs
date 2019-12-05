using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Domain.Communication;
using WebAPI.Domain.Models;
using WebAPI.Domain.Repositories;

namespace WebAPI.Domain.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IUnitOfWork _unitOfWork;

        public BookService(IBookRepository bookRepository, IUnitOfWork unitOfWork)
        {
            _bookRepository = bookRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<BookResponse> DeleteAsync(int id)
        {
            Book existingBook = await _bookRepository.FindByIdAsync(id);

            if (existingBook == null)
            {
                return new BookResponse("Book not found");
            }

            try
            {
                _bookRepository.Remove(existingBook);
                await _unitOfWork.CompleteAsync();
                existingBook = await _bookRepository.FindByIdAsync(id);

                return new BookResponse(existingBook);
            }
            catch(Exception e)
            {
                //TODO - Log the exception  
                return new BookResponse("An error ocurred while deleting the Book: " +
                    $"{e.Message} {e.InnerException?.Message}");
            }
        }

        public async Task<BookResponse> FindByIdAsync(int id)
        {
            Book existingBook = await _bookRepository.FindByIdAsync(id);
            
            if (existingBook == null)
            {
                return new BookResponse("Book not found");  
            }

            return new BookResponse(existingBook);
        }

        public async Task<IEnumerable<Book>> ListAsync()
        {
            IEnumerable<Book> books = await _bookRepository.ListAsync();

            return books;
        }

        public async Task<BookResponse> SaveAsync(Book book)
        {
            try
            {
                await _bookRepository.AddAsync(book);
                await _unitOfWork.CompleteAsync();
                book = await _bookRepository.FindByIdAsync(book.Id);

                return new BookResponse(book);
            }
            catch(Exception e)
            {
                //TODO - Log the exception
                return new BookResponse("An error ocurred while saving the Book: " +
                    $"{e.Message} {e.InnerException?.Message}");
            }
        }

        public async Task<BookResponse> UpdateAsync(int id, Book book)
        {
            Book existingBook = await _bookRepository.FindByIdAsync(id);

            if (existingBook == null)
            {
                return new BookResponse("Book not found");
            }

            existingBook.AuthorId = book.AuthorId;
            existingBook.ISBN = book.ISBN;
            existingBook.Name = book.Name;

            try
            {
                _bookRepository.Update(existingBook);
                await _unitOfWork.CompleteAsync();
                existingBook = await _bookRepository.FindByIdAsync(id);

                return new BookResponse(existingBook);
            }
            catch(Exception e)
            {
                //TODO - Log the exception
                return new BookResponse("An error ocurred while updating the Book: " +
                    $"{e.Message} {e.InnerException?.Message}");
            }
        }
    }
}
