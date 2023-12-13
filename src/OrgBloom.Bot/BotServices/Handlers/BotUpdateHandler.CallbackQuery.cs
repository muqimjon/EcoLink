using Telegram.Bot;
using Telegram.Bot.Types;
using OrgBloom.Domain.Enums;
using OrgBloom.Application.Users.Commands.UpdateUsers;
using OrgBloom.Application.Users.Queries.GetUsers;

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
            State.WaitingForEnterFirstName => HandleSelectedFieldApplicationAsync(botClient, callbackQuery, cancellationToken),
            _ => HandleUnknownCallbackQueryAsync(botClient, callbackQuery, cancellationToken)
        };

        try
        {
            await handler;
        }
        catch(Exception ex)
        {
            logger.LogError(ex, "Error handling callback query: {callbackQuery.Data}", callbackQuery.Data);
        }
    }

    private async Task HandleSelectedFieldApplicationAsync(ITelegramBotClient botClient, CallbackQuery? callbackQuery, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(callbackQuery);
        ArgumentNullException.ThrowIfNull(callbackQuery.Data);
        ArgumentNullException.ThrowIfNull(callbackQuery.Message);

        string languageCode = callbackQuery.Data;

        switch (languageCode)
        {
            case "buttonLanguageUz":
                await mediator.Send(new UpdateLanguageCodeCommand { Id = user.Id, LanguageCode = "uz" }, cancellationToken);
                await botClient.SendTextMessageAsync(
                    chatId: callbackQuery.Message.Chat.Id,
                    text: "O'zbek tili tanlandi!",
                    cancellationToken: cancellationToken);
                break;
            case "buttonLanguageEn":
                await mediator.Send(new UpdateLanguageCodeCommand { Id = user.Id, LanguageCode = "en" }, cancellationToken);
                await botClient.SendTextMessageAsync(
                    chatId: callbackQuery.Message.Chat.Id,
                    text: "English language selected!",
                    cancellationToken: cancellationToken);
                break;
            case "buttonLanguageRu":
                await mediator.Send(new UpdateLanguageCodeCommand { Id = user.Id, LanguageCode = "ru" }, cancellationToken);
                await botClient.SendTextMessageAsync(
                    chatId: callbackQuery.Message.Chat.Id,
                    text: "Русский язык выбран!",
                    cancellationToken: cancellationToken);
                break;
            default:
                await botClient.SendTextMessageAsync(
                    chatId: callbackQuery.Message.Chat.Id,
                    text: "Unknown language selected!",
                    cancellationToken: cancellationToken);
                break;
        }

        await SendMainMenuAsync(botClient, callbackQuery.Message, cancellationToken);
    }

    private Task HandleUnknownCallbackQueryAsync(ITelegramBotClient botClient, CallbackQuery? callbackQuery, CancellationToken cancellationToken)
    {
        logger.LogInformation("Received unknown callback query: {callbackQuery.Data}", callbackQuery?.Data);
        return Task.CompletedTask;
    }
}

