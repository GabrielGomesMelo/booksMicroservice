using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BooksApi.Data;
using BooksApi.Models;
using BooksApi.Models.Pagination;
using BooksApi.Repositories;
using BooksApi.ViewModels;
using System.Text.Json;
using AutoMapper;

namespace BooksApi.Controllers
{
    [ApiController]
    [Route("books")]
    public class BookControllers : ControllerBase
    {
        private readonly IBookRepository _repository;
        private readonly IMapper _mapper;

        public BookControllers(IBookRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookViewModel>>> GetBooks([FromQuery] PagingParameters pagingParameters)
        {
            try
            {
                var books = await _repository.GetAllPaginated(pagingParameters);
                var viewModelBook = _mapper.Map<List<BookViewModel>>(books);

                var metadata = new
                {
                    books.TotalCount,
                    books.PageSize,
                    books.CurrentPage,
                    books.HasNext,
                    books.HasPrevious,
                };

                Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(metadata));
                return Ok(viewModelBook);
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBook(long id)
        {
            try
            {
                var book = await _repository.Find(id);
                if (book == null)
                {
                    return  NotFound();
                }
                return book;
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        [HttpPost]
        public async Task<ActionResult<Book>> CreateBook(Book book)
        {
            try
            {
                await _repository.Create(book);
                return book;
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Book>> UpdateBook(long id, UpdateBookViewModel book)
        {
            try
            {
                if(id != book.Id) return BadRequest();

                var canUpdate = await _repository.Exists(id);
                if (!canUpdate) return NotFound();

                var bookMapped = _mapper.Map<Book>(book);
                await _repository.Update(bookMapped);

                return Ok();
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBook([FromServices] DataContext context, long id)
        {
            try
            {
                var canDelete = await _repository.Exists(id);
                if (canDelete)
                {
                    await _repository.Delete(id);
                }
                return Ok();
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }
    }
}