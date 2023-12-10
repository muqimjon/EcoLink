using Microsoft.EntityFrameworkCore;
using OrgBloom.Domain.Entities;

namespace OrgBloom.Infrastructure.Contexts;


public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Investor> Investors { get; set; }
}
