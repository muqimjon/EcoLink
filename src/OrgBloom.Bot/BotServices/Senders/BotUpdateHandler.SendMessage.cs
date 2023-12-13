using Telegram.Bot;
using Telegram.Bot.Types;
using OrgBloom.Domain.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace OrgBloom.Bot.BotServices;

public partial class BotUpdateHandler
{
    private async Task LanguagePreferenceQuery(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(message);
        var keyboard = new InlineKeyboardMarkup(new[] {
            InlineKeyboardButton.WithCallbackData("🇺🇿", "buttonLanguageUz"),
            InlineKeyboardButton.WithCallbackData("🇬🇧", "buttonLanguageEn"),
            InlineKeyboardButton.WithCallbackData("🇷🇺", "buttonLanguageRu")
        });

        string text = $"Assalomu alaykum {user.FirstName} {user.LastName}!\nO'zingiz uchun qulay tilni tanlang:";

        await botClient.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: text,// localizer[message.Text!, user.FirstName],
            replyMarkup: keyboard,
            cancellationToken: cancellationToken
        );

        user.State = UserState.WaitingForSelectLanguage;
    }

    public async Task HandleStartCommand(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        var chatId = update.Message.Chat.Id;

        var keyboard = new InlineKeyboardMarkup(new[]
        {
            new[]
            {
                InlineKeyboardButton.WithCallbackData("Settings", "settings"),
                InlineKeyboardButton.WithCallbackData("Feedback", "feedback")
            },
            new[]
            {
                InlineKeyboardButton.WithCallbackData("Send Application", "apply"),
                InlineKeyboardButton.WithCallbackData("Information", "info")
            },
            new[]
            {
                InlineKeyboardButton.WithCallbackData("Contact", "contact")
            }
        });

        await botClient.SendTextMessageAsync(
            chatId: chatId,
            text: "Welcome to the main menu! Choose an option:",
            replyMarkup: keyboard,
            cancellationToken: cancellationToken
        );

        user.State = UserState.WaitingForSelectMainMenu;
    }
}
