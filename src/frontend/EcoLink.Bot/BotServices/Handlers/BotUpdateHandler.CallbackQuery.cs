namespace EcoLink.Bot.BotServices;

public partial class BotUpdateHandler
{
    private async Task HandleCallbackQuery(ITelegramBotClient botClient, CallbackQuery? callbackQuery, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(callbackQuery);

        var state = await mediator.Send(new GetStateQuery(user.Id), cancellationToken);
        var handler = state switch
        {
            State.WaitingForSelectLanguage => HandleSelectedLanguageAsync(botClient, callbackQuery, cancellationToken),
            State.WaitingForEnterSector => HandleSectorAsync(botClient, callbackQuery, cancellationToken),
            State.WaitingForSubmitApplication => HandleSubmittionApplicationAsync(botClient, callbackQuery, cancellationToken),
            _ => HandleUnknownCallbackQueryAsync(botClient, callbackQuery, cancellationToken)
        };

        try { await handler; }
        catch(Exception ex) { logger.LogError(ex, "Error handling callback query: {callbackQuery.Data}", callbackQuery.Data); }
    }

    private Task HandleUnknownCallbackQueryAsync(ITelegramBotClient botClient, CallbackQuery? callbackQuery, CancellationToken cancellationToken)
    {
        logger.LogInformation("Received unknown callback query: {callbackQuery.Data}", callbackQuery?.Data);
        return Task.CompletedTask;
    }

    private async Task HandleSelectedLanguageAsync(ITelegramBotClient botClient, CallbackQuery? callbackQuery, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(callbackQuery);
        ArgumentNullException.ThrowIfNull(callbackQuery.Data);
        ArgumentNullException.ThrowIfNull(callbackQuery.Message);

        string text;
        switch (callbackQuery.Data)
        {
            case "ibtnEn":
                await mediator.Send(new UpdateLanguageCodeCommand { Id = user.Id, LanguageCode = "en" }, cancellationToken);
                text = "Great, we will continue with you in English!";
                break;
            case "ibtnRu":
                await mediator.Send(new UpdateLanguageCodeCommand { Id = user.Id, LanguageCode = "ru" }, cancellationToken);
                text = "Отлично, мы продолжим с вами на русском языке!";
                break;
            default:
                await mediator.Send(new UpdateLanguageCodeCommand { Id = user.Id, LanguageCode = "uz" }, cancellationToken);
                text = "Ajoyib, siz bilan o'zbek tilida davom ettiramiz!";
                break;
        }

        await botClient.EditMessageTextAsync(
            chatId: callbackQuery.Message.Chat.Id,
            text: text,
            messageId: callbackQuery.Message.MessageId,
            cancellationToken: cancellationToken);

        await SendMainMenuAsync(botClient, callbackQuery.Message, cancellationToken);
    }
}

