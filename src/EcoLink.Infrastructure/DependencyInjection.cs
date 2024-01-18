using Microsoft.EntityFrameworkCore;
using EcoLink.Infrastructure.Contexts;
using Microsoft.Extensions.Configuration;
using EcoLink.Infrastructure.Repositories;
using EcoLink.Application.Commons.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace EcoLink.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString));

        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped(typeof(ISheetsRepository<>), typeof(SheetsRepository<>));

        return services;
    } 
}

