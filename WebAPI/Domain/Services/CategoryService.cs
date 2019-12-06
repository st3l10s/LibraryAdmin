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
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CategoryService> _logger;

        public CategoryService(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork, ILogger<CategoryService> logger)
        {
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<CategoryResponse> DeleteAsync(int id)
        {
            Category existingCategory = await _categoryRepository.FindByIdAsync(id);

            if (existingCategory == null)
            {
                return new CategoryResponse("Category not found");
            }

            try
            {
                _categoryRepository.Remove(existingCategory);
                await _unitOfWork.CompleteAsync();
                existingCategory = await _categoryRepository.FindByIdAsync(id);

                return new CategoryResponse(existingCategory);
            }
            catch(Exception e)
            {
                _logger.LogError(e.ToString());

                return new CategoryResponse("An error ocurred while deleting the Category: " +
                    $"{e.Message} {e.InnerException?.Message}");
            }
        }

        public async Task<CategoryResponse> FindByIdAsync(int id)
        {
            Category existingCategory = await _categoryRepository.FindByIdAsync(id);
            
            if (existingCategory == null)
            {
                return new CategoryResponse("Category not found");  
            }

            return new CategoryResponse(existingCategory);
        }

        public async Task<IEnumerable<Category>> ListAsync()
        {
            IEnumerable<Category> categorys = await _categoryRepository.ListAsync();

            return categorys;
        }

        public async Task<CategoryResponse> SaveAsync(Category category)
        {
            try
            {
                await _categoryRepository.AddAsync(category);
                await _unitOfWork.CompleteAsync();
                category = await _categoryRepository.FindByIdAsync(category.Id);

                return new CategoryResponse(category);
            }
            catch(Exception e)
            {
                _logger.LogError(e.ToString());

                return new CategoryResponse("An error ocurred while saving the Category: " +
                    $"{e.Message} {e.InnerException?.Message}");
            }
        }

        public async Task<CategoryResponse> UpdateAsync(int id, Category category)
        {
            Category existingCategory = await _categoryRepository.FindByIdAsync(id);

            if (existingCategory == null)
            {
                return new CategoryResponse("Category not found");
            }

            existingCategory.Description = category.Description;
            existingCategory.Name = category.Name;

            try
            {
                _categoryRepository.Update(existingCategory);
                await _unitOfWork.CompleteAsync();
                existingCategory = await _categoryRepository.FindByIdAsync(id);

                return new CategoryResponse(existingCategory);
            }
            catch(Exception e)
            {
                _logger.LogError(e.ToString());

                return new CategoryResponse("An error ocurred while updating the Category: " +
                    $"{e.Message} {e.InnerException?.Message}");
            }
        }
    }
}
