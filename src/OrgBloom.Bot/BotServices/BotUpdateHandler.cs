using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;
using Microsoft.Extensions.Localization;

namespace OrgBloom.Bot.BotServices;

public partial class BotUpdateHandler(ILogger<BotUpdateHandler> logger, IStringLocalizer localizer) : IUpdateHandler
{
    public Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
    {
        logger.LogInformation("HandlePollingError: {ErrorText}", exception.Message);

        return Task.CompletedTask;
    }

    public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        var handler = update.Type switch
        {
            UpdateType.Message => HandleMessageAsync(botClient, update.Message, cancellationToken),
            UpdateType.EditedMessage => HandleEditedMessageAsync(botClient, update.EditedMessage, cancellationToken),
            _ => HandleUnknownUpdateAsync(botClient, update, cancellationToken)
        };

        try
        {
            await handler;
        }
        catch(Exception ex)
        {
            await HandlePollingErrorAsync(botClient, ex, cancellationToken);
        }
    }

    private Task HandleUnknownUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        logger.LogInformation("Unknown update type: {UpdateType}", update.Type);
        return Task.CompletedTask;
    }
}
