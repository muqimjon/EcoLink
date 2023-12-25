using Microsoft.EntityFrameworkCore;
using EcoLink.Domain.Entities.Users;
using EcoLink.Domain.Entities.Investment;
using EcoLink.Domain.Entities.Representation;
using EcoLink.Domain.Entities.Entrepreneurship;
using EcoLink.Domain.Entities.ProjectManagement;

namespace EcoLink.Infrastructure.Contexts;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Investor> Investors { get; set; }
    public DbSet<Entrepreneur> Entrepreneurs { get; set; }
    public DbSet<InvestmentApp> InvestmentApps { get; set; }
    public DbSet<ProjectManager> ProjectManagers { get; set; }
    public DbSet<Representative> Representatives { get; set; }
    public DbSet<RepresentationApp> RepresentationApps { get; set; }
    public DbSet<EntrepreneurshipApp> EntrepreneurshipApps { get; set; }
    public DbSet<ProjectManagementApp> ProjectManagementApps { get; set; }
}