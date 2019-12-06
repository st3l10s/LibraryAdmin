using Microsoft.Extensions.Logging;
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
        private readonly IAuthorRepository _authorRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<BookService> _logger;

        public BookService(IBookRepository bookRepository, IAuthorRepository authorRepository, ICategoryRepository categoryRepository,
            IUnitOfWork unitOfWork, ILogger<BookService> logger)
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
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
                _logger.LogError(e.ToString());

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
            Author author = await _authorRepository.FindByIdAsync(book.AuthorId);

            if (author == null)
            {
                return new BookResponse($"Author with Id: {book.AuthorId} does not exist");
            }

            foreach (CategoryBook categoryBook in book.Categories)
            {
                Category category = await _categoryRepository.FindByIdAsync(categoryBook.CategoryId);
                if (category == null)
                {
                    return new BookResponse($"Category with Id: {categoryBook.CategoryId} does not exist");
                }
            }

            try
            {
                await _bookRepository.AddAsync(book);
                await _unitOfWork.CompleteAsync();
                book = await _bookRepository.FindByIdAsync(book.Id);

                return new BookResponse(book);
            }
            catch(Exception e)
            {
                _logger.LogError(e.ToString());

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

            Author author = await _authorRepository.FindByIdAsync(book.AuthorId);

            if (author == null)
            {
                return new BookResponse($"Author with Id: {book.AuthorId} does not exist");
            }

            foreach (CategoryBook categoryBook in book.Categories)
            {
                Category category = await _categoryRepository.FindByIdAsync(categoryBook.CategoryId);
                if (category == null)
                {
                    return new BookResponse($"Category with Id: {categoryBook.CategoryId} does not exist");
                }
            }

            existingBook.AuthorId = book.AuthorId;
            existingBook.ISBN = book.ISBN;
            existingBook.Name = book.Name;
            existingBook.Categories = book.Categories;

            try
            {
                _bookRepository.Update(existingBook);
                await _unitOfWork.CompleteAsync();
                existingBook = await _bookRepository.FindByIdAsync(id);

                return new BookResponse(existingBook);
            }
            catch(Exception e)
            {
                _logger.LogError(e.ToString());

                return new BookResponse("An error ocurred while updating the Book: " +
                    $"{e.Message} {e.InnerException?.Message}");
            }
        }
    }
}
