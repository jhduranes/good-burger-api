using Entities.Tables;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Configuration
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        public DbSet<Products> Products { get; set; }
    }
}
