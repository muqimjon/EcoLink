using System.Globalization;

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

        var culture = user.LanguageCode switch
        {
            "uz" => new CultureInfo("uz-Uz"),
            "en" => new CultureInfo("en-US"),
            "ru" => new CultureInfo("ru-RU"),
            _ => CultureInfo.CurrentCulture
        };

        CultureInfo.CurrentCulture = new CultureInfo("uz-Uz");
        CultureInfo.CurrentUICulture = new CultureInfo("uz-Uz");

        await botClient.EditMessageTextAsync(
            chatId: callbackQuery.Message.Chat.Id,
            text: localizer["txtLanguageSelected"],
            messageId: callbackQuery.Message.MessageId,
            cancellationToken: cancellationToken);

        await SendMainMenuAsync(botClient, callbackQuery.Message, cancellationToken);
        await service.UpdateAsync(user, cancellationToken);
    }

    private Task HandleUnknownCallbackQueryAsync(ITelegramBotClient botClient, CallbackQuery? callbackQuery, CancellationToken cancellationToken)
    {
        logger.LogInformation("Received unknown callback query: {callbackQuery.Data}", callbackQuery?.Data);
        return Task.CompletedTask;
    }
}

