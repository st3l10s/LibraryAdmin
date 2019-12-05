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
    public class BooksController : ControllerBase
    {
        IBookService _BookService;
        IMapper _mapper;

        public BooksController(IBookService BookService, IMapper mapper)
        {
            _BookService = BookService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<BookDisplayResource>> GetBooks()
        {
            IEnumerable<Book> books = await _BookService.ListAsync();
            IEnumerable<BookDisplayResource> resources = _mapper.Map<IEnumerable<BookDisplayResource>>(books);

            return resources;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BookDetailResource>> GetBook(int id)
        {
            BookResponse response = await _BookService.FindByIdAsync(id);

            if (!response.Success)
            {
                return NotFound(response.Message);
            }

            BookDetailResource resource = _mapper.Map<BookDetailResource>(response.Book);

            return Ok(resource);
        }

        [HttpPost]
        public async Task<ActionResult<BookDetailResource>> PostBook(BookSaveResource resource)
        {
            Book book = _mapper.Map<Book>(resource);
            BookResponse response = await _BookService.SaveAsync(book);

            if (!response.Success)
            {
                return BadRequest(response.Message);
            }

            BookDetailResource savedResource = _mapper.Map<BookDetailResource>(response.Book);

            return CreatedAtAction(nameof(GetBook), new { id = savedResource.Id }, savedResource);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BookDetailResource>> PutBook(int id, BookSaveResource resource)
        {
            Book book = _mapper.Map<Book>(resource);
            BookResponse response = await _BookService.UpdateAsync(id, book);

            if (!response.Success)
            {
                return BadRequest(response.Message);
            }

            BookDetailResource updatedResource = _mapper.Map<BookDetailResource>(response.Book);

            return Ok(updatedResource);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<BookDetailResource>> DeleteBook(int id)
        {
            BookResponse response = await _BookService.DeleteAsync(id);

            if (!response.Success)
            {
                return BadRequest(response.Message);
            }

            BookDetailResource deletedResource = _mapper.Map<BookDetailResource>(response.Book);

            return Ok(deletedResource);
        }
    }
}