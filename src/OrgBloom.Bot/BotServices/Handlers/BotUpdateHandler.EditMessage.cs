using Telegram.Bot;
using Telegram.Bot.Types;

namespace OrgBloom.Bot.BotServices;

public partial class BotUpdateHandler
{
    private Task HandleEditedMessageAsync(ITelegramBotClient botClient, Message? editedMessage, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(editedMessage);

        var from = editedMessage.From;

        logger.LogInformation("Received Edit Message from {from.FirstName}: {editedMessage.Text}", from?.FirstName, editedMessage.Text);

        return Task.CompletedTask;
    }
}
