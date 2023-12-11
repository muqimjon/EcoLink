using Microsoft.EntityFrameworkCore;
using OrgBloom.Infrastructure.Contexts;
using Microsoft.Extensions.Configuration;
using OrgBloom.Infrastructure.Repositories;
using OrgBloom.Application.Commons.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace OrgBloom.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(
           this IServiceCollection services,
           IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        services.AddSingleton(typeof(IRepository<>), typeof(Repository<>));

        return services;
    }
}
