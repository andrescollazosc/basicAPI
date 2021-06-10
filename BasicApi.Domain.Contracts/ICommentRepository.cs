using BasicApi.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BasicApi.Domain.Contracts
{
    public interface ICommentRepository
    {
        Task<IEnumerable<Comment>> GetAllAsync();
        Task<IEnumerable<Comment>> GetByBookIdAsync(int bookId);
        Task<Comment> GetByIdAsync(int id);
        Task<Comment> AddAsync(Comment entity); 
        Task<bool> UpdateAsync(Comment entity);
        
    }
}
