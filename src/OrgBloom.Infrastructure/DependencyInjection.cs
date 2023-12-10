using Microsoft.EntityFrameworkCore;
using OrgBloom.Application.Interfaces;
using OrgBloom.Infrastructure.Contexts;
using Microsoft.Extensions.Configuration;
using OrgBloom.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace OrgBloom.Infrastructure;

public static class DependencyInjection
{
    public static void AddInfrastructureServices(
           this IServiceCollection services,
           IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
    }
}
