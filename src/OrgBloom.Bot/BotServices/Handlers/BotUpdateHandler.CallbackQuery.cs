using Telegram.Bot;
using Telegram.Bot.Types;
using OrgBloom.Domain.Enums;
using OrgBloom.Application.Users.Queries.GetUsers;
using OrgBloom.Application.Users.Commands.UpdateUsers;

namespace OrgBloom.Bot.BotServices;

public partial class BotUpdateHandler
{
    private async Task HandleCallbackQuery(ITelegramBotClient botClient, CallbackQuery? callbackQuery, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(callbackQuery);

        var state = await mediator.Send(new GetStateQuery(user.Id), cancellationToken);
        var handler = state switch
        {
            State.WaitingForSelectLanguage => HandleSelectedLanguageAsync(botClient, callbackQuery, cancellationToken),
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

        var handle = callbackQuery.Data switch
        {
            "buttonLanguageUz" => mediator.Send(new UpdateLanguageCodeCommand { Id = user.Id, LanguageCode = "uz" }, cancellationToken),
            "buttonLanguageEn" => mediator.Send(new UpdateLanguageCodeCommand { Id = user.Id, LanguageCode = "en" }, cancellationToken),
            "buttonLanguageRu" => mediator.Send(new UpdateLanguageCodeCommand { Id = user.Id, LanguageCode = "ru" }, cancellationToken),
            _ => HandleUnknownCallbackQueryAsync(botClient, callbackQuery, cancellationToken)
        };

        try { await handle; }
        catch (Exception ex) { logger.LogError(ex, "Error handling callback query: {callbackQuery.Data}", callbackQuery.Data); }

        await SendMainMenuAsync(botClient, callbackQuery.Message, cancellationToken);
    }
}

