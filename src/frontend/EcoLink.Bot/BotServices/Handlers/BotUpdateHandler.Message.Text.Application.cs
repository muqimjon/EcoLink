namespace EcoLink.Bot.BotServices;

public partial class BotUpdateHandler
{
    private async Task HandleProfessionAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        var handler = message.Text switch
        {
            { } text when text == localizer["rbtnRepresentation"] => SendMenuRepresentationAsync(botClient, message, cancellationToken),
            { } text when text == localizer["rbtnInvestment"] => SendMenuInvestmentAsync(botClient, message, cancellationToken),
            { } text when text == localizer["rbtnEntrepreneurship"] => SendMenuEntrepreneurshipAsync(botClient, message, cancellationToken),
            { } text when text == localizer["rbtnProjectManagement"] => SendMenuProjectManagementAsync(botClient, message, cancellationToken),
            _ when message.Text == localizer["rbtnBack"] => SendMainMenuAsync(botClient, message, cancellationToken),
            _ => HandleUnknownMessageAsync(botClient, message, cancellationToken)
        };

        try { await handler; }
        catch (Exception ex) { logger.LogError(ex, "Error handling message from {from.FirstName}", user.FirstName); }
    }

    private async Task HandleResendApplicationAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        var handle = message.Text switch
        {
            { } text when text == localizer["rbtnResend"] => ClarifyProfessionQueryAsync(botClient, message, cancellationToken),
            { } text when text == localizer["rbtnBack"] => SendMainMenuAsync(botClient, message, cancellationToken),
            _ => HandleUnknownMessageAsync(botClient, message, cancellationToken)
        };

        try { await handle; }
        catch (Exception ex) { logger.LogError(ex, "Error handling message from {from.FirstName}", user.FirstName); }
    }

    private async Task ClarifyProfessionQueryAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        var handler = user.Profession switch
        {
            UserProfession.Investor => InvestmentApplicationAsync(botClient, message, cancellationToken),
            UserProfession.Entrepreneur => EntrepreneurshipApplicationAsync(botClient, message, cancellationToken),
            UserProfession.ProjectManager => ProjectManagementQueryAsync(botClient, message, cancellationToken),
            UserProfession.Representative => RepresentationApplicationAsync(botClient, message, cancellationToken),
            _ => HandleUnknownMessageAsync(botClient, message, cancellationToken)
        };
        try { await handler; }
        catch { logger.LogError("Error handling message from {from.FirstName}", user.FirstName); }

        user.Application.IsSubmitted = false;
        await service.UpdateAsync(user, cancellationToken);
    }

    private async Task HandleExpectationAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(message);
        ArgumentNullException.ThrowIfNull(message.Text);

        Task handler;

        switch (message.Text)
        {
            case var text when text == localizer["rbtnCancel"]:
                handler = user.Profession switch
                {
                    UserProfession.None => SendMenuSettingsAsync(botClient, message, cancellationToken),
                    _ => SendMenuProfessionsAsync(botClient, message, cancellationToken)
                };
                break;
            default:
                handler = user.Profession switch
                {
                    UserProfession.None => SendMenuSettingsAsync(botClient, message, cancellationToken),
                    _ => SendRequestForPurposeAsync(botClient, message, cancellationToken)
                };
                user.Application.Expectation = message.Text; // TODO: need validation
                break;
        }

        try { await handler; }
        catch (Exception ex) { logger.LogError(ex, "Error handling message from {user.FirstName}", user.FirstName); }

        await service.UpdateAsync(user, cancellationToken);
    }

    private async Task HandlePurposeAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(message);
        ArgumentNullException.ThrowIfNull(message.Text);
        Task handler;

        switch (message.Text)
        {
            case var text when text == localizer["rbtnCancel"]:
                handler = user.Profession switch
                {
                    UserProfession.None => SendMenuSettingsAsync(botClient, message, cancellationToken),
                    _ => SendMenuProfessionsAsync(botClient, message, cancellationToken)
                };
                break;
            default:
                handler = user.Profession switch
                {
                    UserProfession.None => SendMenuSettingsAsync(botClient, message, cancellationToken),
                    _ => SendForSubmitApplicationAsync(botClient, message, cancellationToken)
                };
                user.Application.Purpose = message.Text; // TODO: need validation
                break;
        }

        try { await handler; }
        catch (Exception ex) { logger.LogError(ex, "Error handling message from {user.FirstName}", user.FirstName); }

        await service.UpdateAsync(user, cancellationToken);
    }
}