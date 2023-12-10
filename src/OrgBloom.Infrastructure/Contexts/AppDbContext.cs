using OrgBloom.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace OrgBloom.Infrastructure.Contexts;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Investor> Investors { get; set; }
    public DbSet<Entrepreneur> Entrepreneurs { get; set; }
    public DbSet<ProjectManager> ProjectManagers { get; set; }
    public DbSet<Representative> Representatives { get; set; }
}