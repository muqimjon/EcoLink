using Telegram.Bot;
using Telegram.Bot.Types;
using OrgBloom.Domain.Enums;
using OrgBloom.Application.Users.Queries.GetUsers;
using OrgBloom.Application.Users.Commands.UpdateUsers;

namespace OrgBloom.Bot.BotServices;

public partial class BotUpdateHandler
{

    private async Task HandleContactMessageAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(message);

        var state = await mediator.Send(new GetStateQuery(user.Id), cancellationToken);
        var handler = state switch
        {
            State.WaitingForEnterPhoneNumber => HandlePhoneNumbeFromContactAsync(botClient, message, cancellationToken),
            _ => HandleUnknownMessageAsync(botClient, message, cancellationToken)

        };

        try { await handler; }
        catch (Exception ex) { logger.LogError(ex, "Error handling contact message: {callbackQuery.Data}", message.Text); }
    }

    private async Task HandlePhoneNumbeFromContactAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(message);
        ArgumentNullException.ThrowIfNull(message.Contact);

        await mediator.Send(new UpdatePhoneCommand() { Id = user.Id, Phone = message.Contact.PhoneNumber }, cancellationToken); // TODO: need validation

        await SendRequestForEmailAsync(botClient, message, cancellationToken);
    }
}
