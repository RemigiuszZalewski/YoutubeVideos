using Microsoft.EntityFrameworkCore;
using StoredProcedures.Domain.Entities;

namespace StoredProcedures.Infrastructure;

public class DemoDbContext : DbContext
{
    public DemoDbContext(DbContextOptions options) : base(options)
    {
    }
    
    public DbSet<Author> Authors { get; set; }
    public DbSet<Book> Books { get; set; }
}