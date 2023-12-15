using OrgBloom.Application.Users.Commands.UpdateUsers;
using OrgBloom.Application.Users.Queries.GetUsers;
using OrgBloom.Domain.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types;
using Telegram.Bot;

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

            string text = $"Assalomu alaykum {user.FirstName} {user.LastName}!\nO'zingiz uchun qulay tilni tanlang:";
            await botClient.SendTextMessageAsync(
                chatId: message.Chat.Id,
                text: text, // localizer[message.Text!, user.FirstName],
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
            new[] { new KeyboardButton("Ariza topshirish") },
            new[] { new KeyboardButton("Contact"), new KeyboardButton("Fikr bildirish") },
            new[] { new KeyboardButton("Sozlamalar"), new KeyboardButton("Information"), }
        })
        { ResizeKeyboard = true };

        await botClient.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: "Menyuni tanlang:",
            replyMarkup: keyboard,
            cancellationToken: cancellationToken
        );

        await mediator.Send(new UpdateStateCommand(user.Id, State.WaitingForSelectMainMenu), cancellationToken);
    }
}
