using Microsoft.EntityFrameworkCore;
using Thl.EFCore.Models;

namespace Thl.EFCore.Context
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
    }
}