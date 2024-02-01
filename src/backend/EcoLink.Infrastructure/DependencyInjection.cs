using Newtonsoft.Json;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Auth.OAuth2;
using Microsoft.EntityFrameworkCore;
using EcoLink.Infrastructure.Models;
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
        // Add database
        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString(name: "DefaultConnection")));

        // Add sheets service
        services.AddSingleton(implementationInstance: new SheetsConfigure()
        {
            SpreadsheetId = configuration.GetConnectionString(name: "SpreadsheetId")!,
            Service = new SheetsService(initializer: new BaseClientService.Initializer()
            {
                HttpClientInitializer = GoogleCredential.FromJson(
                    json: JsonConvert.SerializeObject(
                        value: configuration.GetSection(
                            key: nameof(GoogleAuthSettings)).
                            Get<GoogleAuthSettings>())),
            }),
            Sheets = configuration.GetSection(key: nameof(Sheets)).Get<Sheets>()!
        });

        // Add repositories
        services.AddScoped(serviceType: typeof(IRepository<>), implementationType: typeof(Repository<>));
        services.AddScoped(serviceType: typeof(ISheetsRepository<>), implementationType: typeof(SheetsRepository<>));

        return services;
    } 
}

