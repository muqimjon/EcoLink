using Telegram.Bot;
using Telegram.Bot.Types;
using OrgBloom.Domain.Enums;
using OrgBloom.Application.Users.Queries.GetUsers;
using OrgBloom.Application.Investors.Commands.UpdateInvestors;
using OrgBloom.Application.Entrepreneurs.Commands.UpdateEntrepreneurs;
using OrgBloom.Application.ProjectManagers.Commands.UpdateProjectManagers;
using OrgBloom.Application.Representatives.Commands.UpdateRepresentatives;

namespace OrgBloom.Bot.BotServices;

public partial class BotUpdateHandler
{
    private async Task HandleProfessionAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        var handler = message.Text switch
        {
            { } text when text == localizer["rbtnEntrepreneurship"] => SendEntrepreneurshipMenuAsync(botClient, message, cancellationToken),
            { } text when text == localizer["rbtnInvestment"] => SendInvestmentMenuAsync(botClient, message, cancellationToken),
            { } text when text == localizer["rbtnRepresentation"] => SendRepresentationMenuAsync(botClient, message, cancellationToken),
            { } text when text == localizer["rbtnProjectManagement"] => SendProjectManagementMenuAsync(botClient, message, cancellationToken),
            _ when message.Text == localizer["rbtnBack"] => SendMainMenuAsync(botClient, message, cancellationToken),
            _ => HandleUnknownMessageAsync(botClient, message, cancellationToken)
        };


        /*        var handle = message.Text switch
                {
                    { } text when text == localizer["rbtnEntrepreneurship"] => EntrepreneurshipQueryAsync(botClient, message, cancellationToken),
                    { } text when text == localizer["rbtnInvestment"] => InvestmentQueryAsync(botClient, message, cancellationToken),
                    { } text when text == localizer["rbtnRepresentation"] => RepresentationQueryAsync(botClient, message, cancellationToken),
                    { } text when text == localizer["rbtnProjectManagement"] => ProjectManagementQueryAsync(botClient, message, cancellationToken),
                    _ when message.Text == localizer["rbtnBack"] => SendMainMenuAsync(botClient, message, cancellationToken),
                    _ => HandleUnknownMessageAsync(botClient, message, cancellationToken)
                };*/

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
        var profession = await mediator.Send(new GetProfessionQuery(user.Id), cancellationToken);
        switch (profession)
        {
            case UserProfession.Investor:
                await mediator.Send(new UpdateInvestorIsSubmittedByUserIdCommand() { UserId = user.Id, IsSubmitted = false }, cancellationToken);
                await InvestmentQueryAsync(botClient, message, cancellationToken);
                break;
            case UserProfession.Entrepreneur:
                await mediator.Send(new UpdateEntrepreneurIsSubmittedByUserIdCommand() { UserId = user.Id, IsSubmitted = false }, cancellationToken);
                await EntrepreneurshipQueryAsync(botClient, message, cancellationToken);
                break;
            case UserProfession.ProjectManager:
                await mediator.Send(new UpdateProjectManagerIsSubmittedByUserIdCommand() { UserId = user.Id, IsSubmitted = false }, cancellationToken);
                await ProjectManagementQueryAsync(botClient, message, cancellationToken);
                break;
            case UserProfession.Representative:
                await mediator.Send(new UpdateRepresentativeIsSubmittedByUserCommand() { UserId = user.Id, IsSubmitted = false }, cancellationToken);
                await RepresentationQueryAsync(botClient, message, cancellationToken);
                break;
            default:
                await HandleUnknownMessageAsync(botClient, message, cancellationToken);
                break;
        };
    }

    private async Task HandleExpectationAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(message);
        ArgumentNullException.ThrowIfNull(message.Text);

        var profession = await mediator.Send(new GetProfessionQuery(user.Id), cancellationToken);
        Task handler;

        switch (message.Text)
        {
            case var text when text == localizer["rbtnCancel"]:
                handler = profession switch
                {
                    UserProfession.None => SendMenuSettingsAsync(botClient, message, cancellationToken),
                    _ => SendApplyQueryAsync(botClient, message, cancellationToken)
                };
                break;
            default:
                handler = profession switch
                {
                    UserProfession.Representative => mediator.Send(new UpdateRepresentativeExpectationByUserIdCommand() { UserId = user.Id, Expectation = message.Text }, cancellationToken), // TODO: need validation
                    UserProfession.ProjectManager => mediator.Send(new UpdateProjectManagerExpectationByUserIdCommand() { UserId = user.Id, Expectation = message.Text }, cancellationToken), // TODO: need validation
                    _ => HandleUnknownMessageAsync(botClient, message, cancellationToken)
                };

                try { await handler; }
                catch (Exception ex) { logger.LogError(ex, "Error handling message from {user.FirstName}", user.FirstName); }

                handler = profession switch
                {
                    UserProfession.None => SendMenuSettingsAsync(botClient, message, cancellationToken),
                    _ => SendRequestForPurposeAsync(botClient, message, cancellationToken)
                };
                break;
        }

        try { await handler; }
        catch (Exception ex) { logger.LogError(ex, "Error handling message from {user.FirstName}", user.FirstName); }
    }

    private async Task HandlePurposeAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(message);
        ArgumentNullException.ThrowIfNull(message.Text);

        var profession = await mediator.Send(new GetProfessionQuery(user.Id), cancellationToken);
        Task handler;

        switch (message.Text)
        {
            case var text when text == localizer["rbtnCancel"]:
                handler = profession switch
                {
                    UserProfession.None => SendMenuSettingsAsync(botClient, message, cancellationToken),
                    _ => SendApplyQueryAsync(botClient, message, cancellationToken)
                };
                break;
            default:
                handler = profession switch
                {
                    UserProfession.Representative => mediator.Send(new UpdateRepresentativePurposeByUserIdCommand() { UserId = user.Id, Purpose = message.Text }, cancellationToken), // TODO: need validation
                    UserProfession.ProjectManager => mediator.Send(new UpdateProjectManagerPurposeVyUserIdCommand() { UserId = user.Id, Purpose = message.Text }, cancellationToken), // TODO: need validation
                    _ => throw new NotImplementedException()
                };

                try { await handler; }
                catch (Exception ex) { logger.LogError(ex, "Error handling message from {user.FirstName}", user.FirstName); }

                handler = profession switch
                {
                    UserProfession.None => SendMenuSettingsAsync(botClient, message, cancellationToken),
                    _ => SendForSubmitApplicationAsync(botClient, message, cancellationToken)
                };
                break;
        }

        try { await handler; }
        catch (Exception ex) { logger.LogError(ex, "Error handling message from {user.FirstName}", user.FirstName); }
    }
}