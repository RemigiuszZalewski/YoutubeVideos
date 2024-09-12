using Bogus;
using BulkOperations.API.Models;
using Microsoft.EntityFrameworkCore;

namespace BulkOperations.API;

public class BulkOperationsDbContext : DbContext
{
    public BulkOperationsDbContext(DbContextOptions options) : base(options)
    {
    }
    
    public DbSet<Product> Products { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>()
            .Property(p => p.Price)
            .HasColumnType("decimal(18, 2)");
    }
}