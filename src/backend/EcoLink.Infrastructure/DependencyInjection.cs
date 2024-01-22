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
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        // Add sheets service
        #region Add Sheets configure
        #region Google Auth Settings converting to json
        GoogleAuthSettings googleAuth = new();

        var properties = typeof(GoogleAuthSettings).GetProperties();
        foreach (var property in properties)
            property.SetValue(googleAuth, configuration[property.Name] ?? string.Empty);

        var googleAuthJson = JsonConvert.SerializeObject(googleAuth);
        #endregion

        services.AddSingleton(new SheetsConfigure()
        {
            SpreadsheetId = configuration.GetConnectionString("SpreadsheetId")!,
            Service = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = GoogleCredential.FromJson(json: googleAuthJson),
                ApplicationName = configuration["ApplicationName"] ?? string.Empty,
            }),
        });
        #endregion

        // Add repositories
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped(typeof(ISheetsRepository<>), typeof(SheetsRepository<>));

        return services;
    } 
}

