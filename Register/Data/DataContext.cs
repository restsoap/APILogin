using Microsoft.EntityFrameworkCore;
using Register.Models;

namespace Register.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Usuario> Users { get; set; }

    }
}
