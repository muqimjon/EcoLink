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
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")), ServiceLifetime.Scoped);

        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));


/*        string googleAuthJson = configuration["GoogleAuth"]!;

        var configur = new SheetsConfigure()
        {
            SpreadsheetId = configuration["SpreadsheetId"]!,
            Service = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = GoogleCredential.FromJson(googleAuthJson),
                ApplicationName = configuration["ApplicationName"],
            }),
        };
        services.AddSingleton(configur);*/

/*

        services.AddScoped(typeof(ISheetsRepository<>), typeof(SheetsRepository<>));*/

        return services;
    }
}

