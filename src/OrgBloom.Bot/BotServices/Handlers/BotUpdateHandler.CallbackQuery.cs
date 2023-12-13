using Telegram.Bot;
using Telegram.Bot.Types;
using OrgBloom.Domain.Enums;
using OrgBloom.Application.Users.Commands.UpdateUsers;

namespace OrgBloom.Bot.BotServices;

public partial class BotUpdateHandler
{
    private async Task HandleCallbackQuery(ITelegramBotClient botClient, CallbackQuery? callbackQuery, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(callbackQuery);

        var handler = user.State switch
        {
            UserState.WaitingForSelectLanguage => HandleSelectedLanguageAsync(botClient, callbackQuery, cancellationToken),
            UserState.WaitingForSelectMainMenu => HandleCallbackQuerys(botClient, callbackQuery, cancellationToken),
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

    private async Task HandleSelectedLanguageAsync(ITelegramBotClient botClient, CallbackQuery? callbackQuery, CancellationToken cancellationToken)
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
    }
    
    public async Task HandleCallbackQuerys(ITelegramBotClient botClient, CallbackQuery? callbackQuery, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(callbackQuery);
        ArgumentNullException.ThrowIfNull(callbackQuery.Message);

        var chatId = callbackQuery.Message.Chat.Id;
        var data = callbackQuery.Data;

        switch (data)
        {
            case "settings":
                await botClient.SendTextMessageAsync(chatId: chatId, text: "Settings menu", cancellationToken: cancellationToken);
                break;
            case "feedback":
                await botClient.SendTextMessageAsync(chatId: chatId, text: "Send us your feedback!", cancellationToken: cancellationToken);
                break;
            case "apply":
                await botClient.SendTextMessageAsync(chatId: chatId, text: "Send your application via this link", cancellationToken: cancellationToken);
                break;
            case "info":
                await botClient.SendTextMessageAsync(chatId: chatId, text: "Here is some information about our organization", cancellationToken: cancellationToken);
                break;
            case "contact":
                await botClient.SendTextMessageAsync(chatId: chatId, text: "Our contact information: email@example.com", cancellationToken: cancellationToken);
                break;
        }
    }

    private Task HandleUnknownCallbackQueryAsync(ITelegramBotClient botClient, CallbackQuery? callbackQuery, CancellationToken cancellationToken)
    {
        logger.LogInformation("Received unknown callback query: {callbackQuery.Data}", callbackQuery?.Data);
        return Task.CompletedTask;
    }
}

