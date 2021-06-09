using BasicApi.Domain.Contracts;
using BasicApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasicApi.Data.DataAccess.Repositories
{
    public class BookRepository : IBookRepository
    {
        #region Attributes
        private readonly ApplicationDbContext _context;
        #endregion

        #region Constructors
        public BookRepository(ApplicationDbContext context)
        {
            this._context = context;
        }
        #endregion

        #region Public methods
        public async Task<Book> AddAsync(Book entity) {
            entity.Active = true;
            _context.Books.Add(entity);

            try {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return new Book();
            }

            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var bookDB = await _context.Books.FirstOrDefaultAsync(book => book.Active && book.Id == id);
            bookDB.Active = false;

            try
            {
                return await _context.SaveChangesAsync() > 0 ? true : false;
            }
            catch (Exception) {
                return false;
            }
        }

        public async Task<IEnumerable<Book>> GetAllAsync()
        {
            var resultSearch = await _context.Books.Where(book => book.Active).ToListAsync();

            return resultSearch;
        }

        public async Task<IEnumerable<Book>> GetAllByAuthorIdAsync(int authorId)
        {
            var resultSearch = await _context.Books.Where(book => book.Active && book.AuthorId == authorId).ToListAsync();

            return resultSearch;
        }

        public async Task<Book> GetByIdAsync(int id)
        {
            var resultSearch = await _context.Books.FirstOrDefaultAsync(book => book.Active && book.Id == id);

            return resultSearch;
        }

        public async Task<bool> UpdateAsync(Book entity)
        {
            var bookDB = await _context.Books.FirstOrDefaultAsync(book => book.Active && book.Id == entity.Id);
            bookDB.Name = entity.Name;

            try
            {
                return await _context.SaveChangesAsync() > 0 ? true : false;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion
    }
}
