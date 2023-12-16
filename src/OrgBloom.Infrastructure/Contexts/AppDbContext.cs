using Microsoft.EntityFrameworkCore;
using OrgBloom.Domain.Entities.Representation;
using OrgBloom.Domain.Entities.Entrepreneurship;
using OrgBloom.Domain.Entities.ProjectManagement;
using OrgBloom.Domain.Entities.Investment;
using OrgBloom.Domain.Entities.Users;

namespace OrgBloom.Infrastructure.Contexts;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Investor> Investors { get; set; }
    public DbSet<Entrepreneur> Entrepreneurs { get; set; }
    public DbSet<ProjectManager> ProjectManagers { get; set; }
    public DbSet<Representative> Representatives { get; set; }
}