using Telegram.Bot;
using Telegram.Bot.Types;
using OrgBloom.Domain.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using OrgBloom.Application.Users.Queries.GetUsers;
using OrgBloom.Application.Users.Commands.UpdateUsers;

namespace OrgBloom.Bot.BotServices;

public partial class BotUpdateHandler
{
    private async Task SendGreetingAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(message);

        var isUserNew = await mediator.Send(new IsUserNewQuery(user.Id), cancellationToken);
        if (isUserNew)
        {
            var keyboard = new InlineKeyboardMarkup(new[] {
                InlineKeyboardButton.WithCallbackData("🇺🇿", "buttonLanguageUz"),
                InlineKeyboardButton.WithCallbackData("🇬🇧", "buttonLanguageEn"),
                InlineKeyboardButton.WithCallbackData("🇷🇺", "buttonLanguageRu")
            });

            await botClient.SendTextMessageAsync(
                chatId: message.Chat.Id,
                text: localizer["greeting", user.FirstName, user.LastName],
                replyMarkup: keyboard,
                cancellationToken: cancellationToken);

            await mediator.Send(new UpdateStateCommand(user.Id, State.WaitingForSelectLanguage), cancellationToken);
        }
        else await SendMainMenuAsync(botClient, message, cancellationToken);
    }

    public async Task SendMainMenuAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        var keyboard = new ReplyKeyboardMarkup(new[]
        {
            new[] { new KeyboardButton(localizer["btnApply"]) },
            new[] { new KeyboardButton(localizer["btnContact"]), new KeyboardButton(localizer["btnFeedback"]) },
            new[] { new KeyboardButton(localizer["btnSettings"]), new KeyboardButton(localizer["btnInfo"]), }
        }) { ResizeKeyboard = true };

        await botClient.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: localizer["txtMainMenu"],
            replyMarkup: keyboard,
            cancellationToken: cancellationToken
        );

        await mediator.Send(new UpdateStateCommand(user.Id, State.WaitingForSelectMainMenu), cancellationToken);
    }

    private async Task SendInfoAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        await botClient.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: localizer["txtInfo"],
            cancellationToken: cancellationToken
        );
    }

    private async Task SendSettingsQueryAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        ReplyKeyboardMarkup keyboard = new(new[] { new[] { new KeyboardButton(localizer["btnEditLanguage"]) }, new[] { new KeyboardButton(localizer["btnEditPersonalInfo"]) } })
        { ResizeKeyboard = true };

        await botClient.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: localizer["txtMainMenu"],
            replyMarkup: keyboard,
            cancellationToken: cancellationToken
        );

        await mediator.Send(new UpdateStateCommand(user.Id, State.WaitingForSelectMainMenu), cancellationToken);
    }
}
