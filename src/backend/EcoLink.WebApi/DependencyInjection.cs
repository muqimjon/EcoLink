using Newtonsoft.Json;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Auth.OAuth2;
using EcoLink.Infrastructure.Models;

namespace EcoLink.WebApi;

public static class DependencyInjection
{
    public static IServiceCollection AddThis(this IServiceCollection services, 
        GoogleAuthSettings googleAuthSettings,
        IConfiguration configuration)
    {
        var g = JsonConvert.SerializeObject(googleAuthSettings);

        // Add Sheets configure
        services.AddSingleton(new SheetsConfigure()
        {
            SpreadsheetId = configuration.GetConnectionString("SpreadsheetId")!,
            Service = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = GoogleCredential.FromJson(json: g),
                ApplicationName = configuration.GetValue("ApplicationName", string.Empty),
            }),
        });

        // Add localization
        services.AddLocalization();
        return services;
    }
}
