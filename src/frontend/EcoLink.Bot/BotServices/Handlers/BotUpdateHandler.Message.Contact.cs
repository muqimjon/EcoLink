namespace EcoLink.Bot.BotServices;

public partial class BotUpdateHandler
{
    private async Task HandleContactMessageAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(message);

        var handler = user.State switch
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
        handler = user.Profession switch
        {
            UserProfession.None => SendMenuEditPersonalInfoAsync(botClient, message, cancellationToken),
            _ => SendRequestForEmailAsync(botClient, message, cancellationToken)
        };

        try { await handler; }
        catch (Exception ex) { logger.LogError(ex, "Error handling message from {user.FirstName}", user.FirstName); }

        user.Phone = message.Contact.PhoneNumber; // TODO: need validation
    }
}
