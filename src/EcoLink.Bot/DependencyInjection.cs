﻿using Newtonsoft.Json;
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
    public static IServiceCollection AddThis(this IServiceCollection services, IConfiguration configuration)
    {
        // Get bot token
        var token = configuration.GetConnectionString("BotToken");

        // Add bot services
        services.AddSingleton(new TelegramBotClient(token!));
        services.AddSingleton<IUpdateHandler, BotUpdateHandler>();
        services.AddHostedService<BotBackgroundService>();

        // Add sheets service
        #region Add Sheets configure
        #region Google Auth Settings converting to json
        GoogleAuthSettings googleAuth = new();

        var properties = typeof(GoogleAuthSettings).GetProperties();
        foreach (var property in properties)
            property.SetValue(googleAuth, 
                configuration.GetValue(property.Name, 
                    property.GetValue(googleAuth)));

        var googleAuthJson = JsonConvert.SerializeObject(googleAuth);
        #endregion

        services.AddSingleton(new SheetsConfigure()
        {
            SpreadsheetId = configuration.GetConnectionString("SpreadsheetId")!,
            Service = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = GoogleCredential.FromJson(json: googleAuthJson),
                ApplicationName = configuration.GetValue("ApplicationName", string.Empty),
            }),
        });
        #endregion

        // Add localization
        services.AddLocalization();
        return services;
    }
}