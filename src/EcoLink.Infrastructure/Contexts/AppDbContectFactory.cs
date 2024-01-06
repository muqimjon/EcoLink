using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace EcoLink.Infrastructure.Contexts;

public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {/*
        if (args == null || args.Length == 0)
            throw new InvalidOperationException("Connection string is missing. Please provide a valid connection string.");*/

        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionsBuilder.UseNpgsql("host=localhost:5432; password=root;uid=postgres;database=EcoLink;");
        return new AppDbContext(optionsBuilder.Options);
    }
}
