using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Thl.EFCore.Models;
using Thl.Repository.Contract.IRepository;
using Thl.Repository.GenericRepository;

namespace Thl.Repository.Repository
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        // Using Default Page Size as 50 to avoid custom query param input mega number as page size
        private const int PAGE_SIZE = 50;

        public Task<Product> GetProductByIdAsync(int id)
        {
            return this.GetByIdAsync(id);
        }

        public Task<IEnumerable<Product>> GetProductsByNameAsync(int page, string name)
        {
            var products = this._db.Products
                .Where(p => p.Name.ToLower() == name.ToLower())
                .Skip((page - 1) * PAGE_SIZE)
                .Take(PAGE_SIZE)
                .ToList();

            return (Task<IEnumerable<Product>>)products.AsEnumerable();
        }

        public Task AddProductAsync(Product product)
        {
            return this.AddAsync(product);
        }

        public Task UpdateProductAsync(Product product)
        {
            return this.UpdateAsync(product);
        }
    }
}