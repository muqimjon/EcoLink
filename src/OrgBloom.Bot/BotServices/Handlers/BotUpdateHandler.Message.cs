using Telegram.Bot;
using Telegram.Bot.Types;
using OrgBloom.Domain.Enums;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace OrgBloom.Bot.BotServices;

public partial class BotUpdateHandler
{

    private async Task HandleMessageAsync(ITelegramBotClient botClient, Message? message, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(message);

        logger.LogInformation("Received message from {from.FirstName}", user.FirstName);

        var handler = message.Type switch
        {
            MessageType.Text => HandleTextMessageAsync(botClient, message, cancellationToken),
            MessageType.Contact => HandleContactMessageAsync(botClient, message, cancellationToken),
            _ => HandleUnknownMessageAsync(botClient, message, cancellationToken),
        };

        await handler;
    }

    private async Task HandleTextMessageAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        var handler = message.Text switch
        {
            "/start" => StartCommandMenuAsync(botClient, message, cancellationToken),
            _ => HandleUnknownMessageAsync(botClient, message, cancellationToken)
        };

        try
        {
            await handler;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error handling message from {from.FirstName}", user.FirstName);
        }
    }

    private async Task StartCommandMenuAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
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

    private Task HandleUnknownMessageAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        logger.LogInformation("Received message type {message.Type} from {from.FirstName}", message.Type, message.From?.FirstName);

        return Task.CompletedTask;
    }

    private Task HandleContactMessageAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
