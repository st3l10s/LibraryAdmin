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
    public class AuthorsController : ControllerBase
    {
        IAuthorService _authorService;
        IMapper _mapper;

        public AuthorsController(IAuthorService authorService, IMapper mapper)
        {
            _authorService = authorService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<AuthorDisplayResource>> GetAuthors()
        {
            IEnumerable<Author> authors = await _authorService.ListAsync();
            IEnumerable<AuthorDisplayResource> resources = _mapper.Map<IEnumerable<AuthorDisplayResource>>(authors);

            return resources;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorDetailResource>> GetAuthor(int id)
        {
            AuthorResponse response = await _authorService.FindByIdAsync(id);

            if (!response.Success)
            {
                return NotFound(response.Message);
            }

            AuthorDetailResource resource = _mapper.Map<AuthorDetailResource>(response.Author);

            return Ok(resource);
        }

        [HttpPost]
        public async Task<ActionResult<AuthorDetailResource>> PostAuthor(AuthorSaveResource resource)
        {
            Author author = _mapper.Map<Author>(resource);
            AuthorResponse response = await _authorService.SaveAsync(author);

            if (!response.Success)
            {
                return BadRequest(response.Message);
            }

            AuthorDetailResource savedResource = _mapper.Map<AuthorDetailResource>(response.Author);

            return CreatedAtAction(nameof(GetAuthor), new { id = savedResource.Id }, savedResource);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<AuthorDetailResource>> PutAuthor(int id, AuthorSaveResource resource)
        {
            Author author = _mapper.Map<Author>(resource);
            AuthorResponse response = await _authorService.UpdateAsync(id, author);

            if (!response.Success)
            {
                return BadRequest(response.Message);
            }

            AuthorDetailResource updatedResource = _mapper.Map<AuthorDetailResource>(response.Author);

            return Ok(updatedResource);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<AuthorDetailResource>> DeleteAuthor(int id)
        {
            AuthorResponse response = await _authorService.DeleteAsync(id);

            if (!response.Success)
            {
                return BadRequest(response.Message);
            }

            AuthorDetailResource deletedResource = _mapper.Map<AuthorDetailResource>(response.Author);

            return Ok(deletedResource);
        }
    }
}