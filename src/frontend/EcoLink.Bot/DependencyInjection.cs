using Telegram.Bot.Polling;
using EcoLink.Bot.BotServices;
using EcoLink.Bot.BotServices.Commons;

namespace EcoLink.Bot;

public static class DependencyInjection
{
    public static IServiceCollection AddThis(this IServiceCollection services, IConfiguration configuration)
    {
        // Add bot services
        services.AddSingleton(new TelegramBotClient(token: 
            configuration.GetConnectionString("BotToken")!));
        services.AddSingleton<IUpdateHandler, BotUpdateHandler>();
        services.AddHostedService<BotBackgroundService>();

        // Add localization
        services.AddLocalization();
        return services;
    }
}