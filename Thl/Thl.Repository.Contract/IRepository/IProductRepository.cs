using System.Collections.Generic;
using System.Threading.Tasks;
using Thl.EFCore.Models;
using Thl.Repository.Contract.IGenericRepository;

namespace Thl.Repository.Contract.IRepository
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<Product> GetProductByIdAsync(int id);
        Task<IEnumerable<Product>> GetProductsByNameAsync(int page, string name);
        Task AddProductAsync(Product product);
        Task UpdateProductAsync(Product product);
    }
}