using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BooksApi.Models;
using BooksApi.Models.Pagination;

namespace BooksApi.Repositories
{
    public interface IBookRepository
    {
        Task Create(Book obj);
        Task<IEnumerable<Book>> GetAll();
        Task<PagedList<Book>> GetAllPaginated(PagingParameters pagingParameters);
        Task<Book> Find (Expression<Func<Book, bool>> predicate);
        Task<Book> Find(long id);
        Task Delete(long id);
        Task Update(Book obj);
        Task<bool> Exists(long id);
    }
}