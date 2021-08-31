using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BooksApi.Data;
using BooksApi.Models;
using BooksApi.Models.Pagination;
using System;

namespace BooksApi.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly DataContext _context;
        private readonly DbSet<Book> _dbSet;

        public BookRepository(DataContext context)
        {
            _context = context;
            _dbSet = _context.Set<Book>();
        }
        public async Task Create(Book obj)
        {
            _dbSet.Add(obj);
            await _context.SaveChangesAsync();
        }

        public async Task<Book> Find(long id) => await _dbSet.FindAsync(id);

        public async Task<Book> Find(Expression<Func<Book, bool>> predicate) => await _dbSet.FirstOrDefaultAsync(predicate);

        public async Task<IEnumerable<Book>> GetAll() => await _dbSet.ToListAsync();
        public Task<PagedList<Book>> GetAllPaginated(PagingParameters pagingParameters) {
            return Task.FromResult(PagedList<Book>.GetPageList(_dbSet.AsQueryable(), pagingParameters.PageNumber, pagingParameters.PageSize));
        }

        public async Task Delete(long id)
        {
            var obj = await Find(id);
            _dbSet.Remove(obj);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Book obj)
        {
            _dbSet.Update(obj);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> Exists(long id) {
            var obj = (await Find(id));
            if(obj != null) {
                _context.Entry(obj).State = EntityState.Detached;
                return true;
            }
            return false;
        }
    }
}