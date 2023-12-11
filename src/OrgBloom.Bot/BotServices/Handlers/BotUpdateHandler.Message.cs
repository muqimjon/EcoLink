using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace OrgBloom.Bot.BotServices;

public partial class BotUpdateHandler
{

    private async Task HandleMessageAsync(ITelegramBotClient botClient, Message? message, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(message);

        var from = message.From;

        logger.LogInformation("Received message from {from.FirstName}", from?.FirstName);

        var handler = message.Type switch
        {
            MessageType.Text => HandleTextMessageAsync(botClient, message, cancellationToken),
            _ => HandleUnknownMessageAsync(botClient, message, cancellationToken),
        };

        await handler;
    }

    private async Task HandleTextMessageAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        var from = message.From;
        logger.LogInformation("From: {from.FirstName}", from?.FirstName);

        await botClient.SendTextMessageAsync(
            chatId: message.Chat,
            text: localizer["greeting"],
            replyToMessageId: message.MessageId,
            cancellationToken: cancellationToken );
    }

    private Task HandleUnknownMessageAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        logger.LogInformation("Received message type {message.Type} from {from.FirstName}", message.Type, message.From?.FirstName);

        return Task.CompletedTask;
    }
}
