using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace TokenBasedAuthenticationDemo.Infrastructure;

public class DemoDbContext : IdentityDbContext
{
    public DemoDbContext(DbContextOptions options) : base(options)
    {
    }
}