using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Thl.EFCore.Context;
using Thl.Repository.Contract.IGenericRepository;

namespace Thl.Repository.GenericRepository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly MyContext _db = null;
        protected readonly DbSet<T> DbSet = null;

        public GenericRepository()
        {
            var options = new DbContextOptionsBuilder<MyContext>().Options;

            this._db = new MyContext(options);
            this.DbSet = _db.Set<T>();
        }
        public async Task<IEnumerable<T>> GetAsync()
        {
            return await DbSet.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await DbSet.FindAsync(id);
        }

        public async Task AddAsync(T entity)
        {
            DbSet.Add(entity);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            DbSet.Attach(entity);
            _db.Entry(entity).State = EntityState.Modified;
            await _db.SaveChangesAsync();
        }
    }
}