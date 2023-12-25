namespace EcoLink.Bot.BotServices;

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

        Task handler;
        var profession = await mediator.Send(new GetProfessionQuery(user.Id), cancellationToken);
        await mediator.Send(new UpdatePhoneCommand() { Id = user.Id, Phone = message.Contact.PhoneNumber }, cancellationToken); // TODO: need validation

        handler = profession switch
        {
            UserProfession.None => SendMenuEditPersonalInfoAsync(botClient, message, cancellationToken),
            _ => SendRequestForEmailAsync(botClient, message, cancellationToken)
        };

        try { await handler; }
        catch (Exception ex) { logger.LogError(ex, "Error handling message from {user.FirstName}", user.FirstName); }
    }
}
