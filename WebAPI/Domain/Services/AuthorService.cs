using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Domain.Communication;
using WebAPI.Domain.Models;
using WebAPI.Domain.Repositories;

namespace WebAPI.Domain.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AuthorService(IAuthorRepository authorRepository, IUnitOfWork unitOfWork)
        {
            _authorRepository = authorRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<AuthorResponse> DeleteAsync(int id)
        {
            Author existingAuthor = await _authorRepository.FindByIdAsync(id);

            if (existingAuthor == null)
            {
                return new AuthorResponse("Author not found");
            }

            try
            {
                _authorRepository.Remove(existingAuthor);
                await _unitOfWork.CompleteAsync();

                return new AuthorResponse(existingAuthor);
            }
            catch(Exception e)
            {
                //TODO - Log the exception  
                return new AuthorResponse("An error ocurred while deleting the Author: " +
                    $"{e.Message} {e.InnerException?.Message}");
            }
        }

        public async Task<AuthorResponse> FindByIdAsync(int id)
        {
            Author existingAuthor = await _authorRepository.FindByIdAsync(id);
            
            if (existingAuthor == null)
            {
                return new AuthorResponse("Author not found");  
            }

            return new AuthorResponse(existingAuthor);
        }

        public async Task<IEnumerable<Author>> ListAsync()
        {
            IEnumerable<Author> authors = await _authorRepository.ListAsync();

            return authors;
        }

        public async Task<AuthorResponse> SaveAsync(Author author)
        {
            try
            {
                await _authorRepository.AddAsync(author);
                await _unitOfWork.CompleteAsync();

                return new AuthorResponse(author);
            }
            catch(Exception e)
            {
                //TODO - Log the exception
                return new AuthorResponse("An error ocurred while saving the Author: " +
                    $"{e.Message} {e.InnerException?.Message}");
            }
        }

        public async Task<AuthorResponse> UpdateAsync(int id, Author author)
        {
            Author existingAuthor = await _authorRepository.FindByIdAsync(id);

            if (existingAuthor == null)
            {
                return new AuthorResponse("Author not found");
            }

            existingAuthor.BirthDay = author.BirthDay;
            existingAuthor.LastName = author.LastName;
            existingAuthor.Name = author.Name;

            try
            {
                _authorRepository.Update(existingAuthor);
                await _unitOfWork.CompleteAsync();

                return new AuthorResponse(existingAuthor);
            }
            catch(Exception e)
            {
                //TODO - Log the exception
                return new AuthorResponse("An error ocurred while updating the Author: " +
                    $"{e.Message} {e.InnerException?.Message}");
            }
        }
    }
}
