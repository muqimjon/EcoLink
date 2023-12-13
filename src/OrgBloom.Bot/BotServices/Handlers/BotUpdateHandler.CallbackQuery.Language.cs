using OrgBloom.Application.Users.Commands.UpdateUsers;
using Telegram.Bot.Types;
using Telegram.Bot;

namespace OrgBloom.Bot.BotServices;

public partial class BotUpdateHandler
{
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

        await SendMainMenuAsync(botClient, callbackQuery.Message, cancellationToken);
    }
}
