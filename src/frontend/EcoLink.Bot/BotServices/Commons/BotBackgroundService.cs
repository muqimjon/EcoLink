namespace EcoLink.Bot.BotServices.Commons;

public class BotBackgroundService(TelegramBotClient client,
    ILogger<BotBackgroundService> logger,
    IUpdateHandler handler) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var bot = await client.GetMeAsync(stoppingToken);

        logger.LogInformation("Bot {BotName} started successfully", bot.Username);

        client.StartReceiving(
            updateHandler: handler.HandleUpdateAsync,
            pollingErrorHandler: handler.HandlePollingErrorAsync,
            receiverOptions: null, 
            cancellationToken: stoppingToken);
    }
}
