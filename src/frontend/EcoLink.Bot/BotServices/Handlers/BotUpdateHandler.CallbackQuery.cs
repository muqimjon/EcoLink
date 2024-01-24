namespace EcoLink.Bot.BotServices;

public partial class BotUpdateHandler
{
    private async Task HandleCallbackQuery(ITelegramBotClient botClient, CallbackQuery? callbackQuery, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(callbackQuery);

        var handler = user.State switch
        {
            State.WaitingForSelectLanguage => HandleSelectedLanguageAsync(botClient, callbackQuery, cancellationToken),
            State.WaitingForEnterSector => HandleSectorAsync(botClient, callbackQuery, cancellationToken),
            State.WaitingForSubmitApplication => HandleSubmittionApplicationAsync(botClient, callbackQuery, cancellationToken),
            _ => HandleUnknownCallbackQueryAsync(botClient, callbackQuery, cancellationToken)
        };

        try { await handler; }
        catch(Exception ex) { logger.LogError(ex, "Error handling callback query: {callbackQuery.Data}", callbackQuery.Data); }
    }

    private async Task HandleSelectedLanguageAsync(ITelegramBotClient botClient, CallbackQuery? callbackQuery, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(callbackQuery);
        ArgumentNullException.ThrowIfNull(callbackQuery.Data);
        ArgumentNullException.ThrowIfNull(callbackQuery.Message);

        user.LanguageCode = callbackQuery.Data switch
        {
            "ibtnEn" => "en",
            "ibtnRu" => "ru",
            _ => "uz"
        };

        await botClient.EditMessageTextAsync(
            chatId: callbackQuery.Message.Chat.Id,
            text: localizer["txtSelectedLanguage"],
            messageId: callbackQuery.Message.MessageId,
            cancellationToken: cancellationToken);

        await SendMainMenuAsync(botClient, callbackQuery.Message, cancellationToken);
    }

    private Task HandleUnknownCallbackQueryAsync(ITelegramBotClient botClient, CallbackQuery? callbackQuery, CancellationToken cancellationToken)
    {
        logger.LogInformation("Received unknown callback query: {callbackQuery.Data}", callbackQuery?.Data);
        return Task.CompletedTask;
    }
}

