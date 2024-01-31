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
        #region Add Sheets configure
        #region Google Auth Settings converting to json
        GoogleAuthSettings googleAuth = new();

        var properties = typeof(GoogleAuthSettings).GetProperties();
        foreach (var property in properties)
            property.SetValue(obj: googleAuth, value: configuration[property.Name] ?? string.Empty);
        #endregion

        services.AddSingleton(new SheetsConfigure()
        {
            SpreadsheetId = configuration.GetConnectionString(name: "SpreadsheetId")!,
            Service = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = GoogleCredential.FromJson(
                    json: JsonConvert.SerializeObject(googleAuth))
            }),
            Sheets = JsonConvert.DeserializeObject<Sheets>(
                value: configuration["Sheets"]!)!
        });
        #endregion

        // Add repositories
        services.AddScoped(serviceType: typeof(IRepository<>), implementationType: typeof(Repository<>));
        services.AddScoped(serviceType: typeof(ISheetsRepository<>), implementationType: typeof(SheetsRepository<>));

        return services;
    } 
}

