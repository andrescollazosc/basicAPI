using BasicApi.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BasicApi.Domain.Contracts
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetAllAsync();
        Task<IEnumerable<Book>> GetAllByAuthorIdAsync(int authorId);
        Task<Book> GetByIdAsync(int id);
        Task<Book> AddAsync(Book entity);
        Task<bool> UpdateAsync(Book entity);
        Task<bool> DeleteAsync(int id);
    }
}
