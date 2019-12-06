using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Domain.Communication;
using WebAPI.Domain.Models;
using WebAPI.Domain.Services;
using WebAPI.Resources;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        ICategoryService _categoryService;
        IMapper _mapper;

        public CategoriesController(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<CategoryDisplayResource>> GetCategories()
        {
            IEnumerable<Category> categories = await _categoryService.ListAsync();
            IEnumerable<CategoryDisplayResource> resources = _mapper.Map<IEnumerable<CategoryDisplayResource>>(categories);

            return resources;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDetailResource>> GetCategory(int id)
        {
            CategoryResponse response = await _categoryService.FindByIdAsync(id);

            if (!response.Success)
            {
                return NotFound(response.Message);
            }

            CategoryDetailResource resource = _mapper.Map<CategoryDetailResource>(response.Category);

            return Ok(resource);
        }

        [HttpPost]
        public async Task<ActionResult<CategoryDetailResource>> PostCategory(CategorySaveResource resource)
        {
            Category category = _mapper.Map<Category>(resource);
            CategoryResponse response = await _categoryService.SaveAsync(category);

            if (!response.Success)
            {
                return BadRequest(response.Message);
            }

            CategoryDetailResource savedResource = _mapper.Map<CategoryDetailResource>(response.Category);

            return CreatedAtAction(nameof(GetCategory), new { id = savedResource.Id }, savedResource);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CategoryDetailResource>> PutCategory(int id, CategorySaveResource resource)
        {
            Category category = _mapper.Map<Category>(resource);
            CategoryResponse response = await _categoryService.UpdateAsync(id, category);

            if (!response.Success)
            {
                return BadRequest(response.Message);
            }

            CategoryDetailResource updatedResource = _mapper.Map<CategoryDetailResource>(response.Category);

            return Ok(updatedResource);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<CategoryDetailResource>> DeleteCategory(int id)
        {
            CategoryResponse response = await _categoryService.DeleteAsync(id);

            if (!response.Success)
            {
                return BadRequest(response.Message);
            }

            CategoryDetailResource deletedResource = _mapper.Map<CategoryDetailResource>(response.Category);

            return Ok(deletedResource);
        }
    }
}