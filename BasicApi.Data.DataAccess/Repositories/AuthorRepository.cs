using BasicApi.Domain.Contracts;
using BasicApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasicApi.Data.DataAccess.Repositories
{
    public class AuthorRepository : IGenericRepository<Author>
    {
        #region Attributes
        private readonly ApplicationDbContext _context;
        #endregion

        #region Constructors
        public AuthorRepository(ApplicationDbContext context)
        {
            this._context = context;
        }
        #endregion

        #region Public methods
        public async Task<Author> AddAsync(Author entity)
        {
            entity.Active = true;
            _context.Authors.Add(entity);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return new Author();
            }

            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var authorDB = await _context.Authors.FirstOrDefaultAsync(author => author.Id == id);
            authorDB.Active = false;

            try
            {
                return await _context.SaveChangesAsync() > 0 ? true : false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IEnumerable<Author>> GetAllAsync()
        {
            var authorsDB = await _context.Authors.Where(author => author.Active).ToListAsync();

            return authorsDB;
        }

        public async Task<Author> GetByIdAsync(int id)
        {
            var authorDB = await _context.Authors.FirstOrDefaultAsync(author => author.Id == id);

            return authorDB;
        }

        public async Task<bool> UpdateAsync(Author entity)
        {
            var authorDB = await _context.Authors.FirstOrDefaultAsync(author => author.Id == entity.Id && author.Active);
            authorDB.Passport = entity.Passport;
            authorDB.FirstName = entity.FirstName;
            authorDB.LasttName = entity.LasttName;

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
