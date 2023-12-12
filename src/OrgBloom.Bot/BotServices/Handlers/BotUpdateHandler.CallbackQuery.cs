using Telegram.Bot;
using Telegram.Bot.Types;
using OrgBloom.Domain.Enums;
using OrgBloom.Application.Users.Commands.UpdateUsers;

namespace OrgBloom.Bot.BotServices;

public partial class BotUpdateHandler
{
    private async Task HandleCallbackQuery(ITelegramBotClient botClient, CallbackQuery? callbackQuery, CancellationToken cancellationToken)
    {
        var handler = user.State switch
        {
            UserState.WaitingForSelectLanguage => HandleSelectedLanguageAsync(botClient, callbackQuery, cancellationToken)
            
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
                await mediator.Send(new UpdateLanguageCodeCommand { TelegramId = user.Id, LanguageCode = "uz" }, cancellationToken);
                await botClient.SendTextMessageAsync(
                    chatId: callbackQuery.Message.Chat.Id,
                    text: "O'zbek tili tanlandi!",
                    cancellationToken: cancellationToken);
                break;
            case "buttonLanguageEn":
                await mediator.Send(new UpdateLanguageCodeCommand { TelegramId = user.Id, LanguageCode = "en" }, cancellationToken);
                await botClient.SendTextMessageAsync(
                    chatId: callbackQuery.Message.Chat.Id,
                    text: "English language selected!",
                    cancellationToken: cancellationToken);
                break;
            case "buttonLanguageRu":
                await mediator.Send(new UpdateLanguageCodeCommand { TelegramId = user.Id, LanguageCode = "ru" }, cancellationToken);
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
}

