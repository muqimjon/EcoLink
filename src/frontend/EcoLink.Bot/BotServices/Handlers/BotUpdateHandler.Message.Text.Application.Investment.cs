namespace EcoLink.Bot.BotServices;

public partial class BotUpdateHandler
{
    private async Task HandleSelectedInvestmentMenuAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        var handler = message.Text switch
        {
            { } text when text == localizer["rbtnApply"] => InvestmentApplicationAsync(botClient, message, cancellationToken),
            { } text when text == localizer["rbtnInfo"] => SendProfessionInfoAsync(botClient, message, cancellationToken),
            { } text when text == localizer["rbtnBack"] => SendMenuProfessionsAsync(botClient, message, cancellationToken),
            _ => HandleUnknownMessageAsync(botClient, message, cancellationToken)
        };

        try { await handler; }
        catch (Exception ex) { logger.LogError(ex, "Error handling message from {user.FirstName}", user.FirstName); }

        user.Profession = UserProfession.Investor;
    }

    private async Task InvestmentApplicationAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        var app = await investmentAppService.GetAsync(user.Id, cancellationToken);

        if (app is not null)
            await SendAlreadyExistApplicationAsync(GetApplicationInForm(app), botClient, message, cancellationToken);
        else
            await SendRequestForFirstNameAsync(botClient, message, cancellationToken);
    }

    private async Task HandleInvestmentAmountAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(message);
        ArgumentNullException.ThrowIfNull(message.Text);
        Task handler;

        if (message.Text.Equals(localizer["rbtnCancel"]))
        {
            handler = user.Profession switch
            {
                UserProfession.Investor => SendMenuInvestmentAsync(botClient, message, cancellationToken),
                _ => SendMenuProfessionsAsync(botClient, message, cancellationToken),
            };
        }
        else
        {
            handler = user.Profession switch
            {
                UserProfession.Investor => SendRequestForPhoneNumberAsync(botClient, message, cancellationToken),
                _ => SendMenuProfessionsAsync(botClient, message, cancellationToken),
            };
            user.InvestmentAmount = message.Text; // TODO: need validation
        }

        try { await handler; }
        catch { logger.LogError("Error handling message from {user.FirstName}", user.FirstName); }
    }
}
