using System.Collections.Generic;
using System.Threading.Tasks;

namespace Thl.Repository.Contract.IGenericRepository
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAsync();
        Task<T> GetByIdAsync(int id);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
    }
}