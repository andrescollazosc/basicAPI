using BasicApi.Domain.Contracts;
using BasicApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasicApi.Data.DataAccess.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        #region Attributes
        private readonly ApplicationDbContext _context;

        #endregion

        #region Constructors
        public CommentRepository(ApplicationDbContext context)
        {
            this._context = context;
        }
        #endregion

        #region Public methods
        public async Task<Comment> AddAsync(Comment entity)
        {
            entity.Active = true;
            _context.Comments.Add(entity);

            try {
                await _context.SaveChangesAsync();
            }
            catch (Exception) {
                return new Comment();
            }

            return entity;
        }

        public async Task<IEnumerable<Comment>> GetByBookIdAsync(int bookId)
        {
            var result = await _context.Comments.Where(comment => comment.Active && comment.BookId == bookId).ToListAsync();

            return result;
        }

        public async Task<IEnumerable<Comment>> GetAllAsync()
        {
            var result = await _context.Comments.Where(comment => comment.Active).ToListAsync();

            return result;
        }

        public async Task<Comment> GetByIdAsync(int id)
        {
            var result = await _context.Comments.FirstOrDefaultAsync(comment => comment.Active && comment.Id == id);

            return result;
        }
        #endregion
    }
}
