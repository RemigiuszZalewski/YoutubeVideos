using ConcurrencyControl.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ConcurrencyControl.Persistence;

public class DemoDbContext : DbContext
{
    public DemoDbContext(DbContextOptions options) : base(options)
    {
    }
    
    public DbSet<BankAccount> BankAccounts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BankAccount>()
            .Property(v => v.RowVersion)
            .IsRowVersion();

        modelBuilder.Entity<BankAccount>()
            .Property(p => p.Balance)
            .HasColumnType("decimal(18,2)");
    }
}