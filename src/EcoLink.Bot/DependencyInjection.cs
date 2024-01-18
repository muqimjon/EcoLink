using Newtonsoft.Json;
using Telegram.Bot.Polling;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using EcoLink.Bot.BotServices;
using Google.Apis.Auth.OAuth2;
using EcoLink.Infrastructure.Models;
using EcoLink.Bot.BotServices.Commons;

namespace EcoLink.Bot;

public static class DependencyInjection
{
    public static IServiceCollection AddThis(this IServiceCollection services, 
        GoogleAuthSettings googleAuthSettings,
        IConfiguration configuration)
    {
        // Get bot token
        var token = configuration.GetConnectionString("BotToken");

        // Add bot services
        services.AddSingleton(new TelegramBotClient(token!));
        services.AddSingleton<IUpdateHandler, BotUpdateHandler>();
        services.AddHostedService<BotBackgroundService>();

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
