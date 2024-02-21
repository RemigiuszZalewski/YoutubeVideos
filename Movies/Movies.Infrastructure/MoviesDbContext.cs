using Microsoft.EntityFrameworkCore;
using Movies.Domain.Entities;

namespace Movies.Infrastructure;

public class MoviesDbContext : DbContext
{
    public MoviesDbContext(DbContextOptions options) : base(options)
    {
    }
    
    public DbSet<Movie> Movies { get; set; }
}