namespace EcoLink.Bot.BotServices;

public partial class BotUpdateHandler
{
    private async Task HandleSelectedRepresentationMenuAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        var handler = message.Text switch
        {
            { } text when text == localizer["rbtnApply"] => RepresentationApplicationAsync(botClient, message, cancellationToken),
            { } text when text == localizer["rbtnInfo"] => SendProfessionInfoAsync(botClient, message, cancellationToken),
            { } text when text == localizer["rbtnBack"] => SendMenuProfessionsAsync(botClient, message, cancellationToken),
            _ => HandleUnknownMessageAsync(botClient, message, cancellationToken)
        };

        try { await handler; }
        catch (Exception ex) { logger.LogError(ex, "Error handling message from {user.FirstName}", user.FirstName); }

        user.Profession = UserProfession.Representative;
    }

    private async Task RepresentationApplicationAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        if (user.Representation.IsSubmitted)
            await SendAlreadyExistApplicationAsync(botClient, message, cancellationToken);
        else
            await SendRequestForFirstNameAsync(botClient, message, cancellationToken);
    }

    private async Task HandleAreaAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(message);
        ArgumentNullException.ThrowIfNull(message.Text);

        Task handler;
        if (message.Text.Equals(localizer["rbtnCancel"]))
        {
            handler = user.Profession switch
            {
                UserProfession.Representative => SendMenuRepresentationAsync(botClient, message, cancellationToken),
                _ => SendMenuProfessionsAsync(botClient, message, cancellationToken)
            };;
        }
        else
        {
            handler = user.Profession switch
            {
                UserProfession.Representative => SendRequestForExpectationAsync(botClient, message, cancellationToken),
                _ => SendMenuProfessionsAsync(botClient, message, cancellationToken)
            };
            user.Representation.Area = message.Text; // TODO: need validation
        }

        try { await handler; }
        catch (Exception ex) { logger.LogError(ex, "Error handling message from {user.FirstName}", user.FirstName); }
    }
}
