using Telegram.Bot;
using Telegram.Bot.Types;
using OrgBloom.Domain.Enums;
using OrgBloom.Bot.BotServices.Helpers;
using OrgBloom.Application.Users.Queries.GetUsers;
using OrgBloom.Application.Users.Commands.UpdateUsers;
using OrgBloom.Application.Investors.Queries.GetInvestors;
using OrgBloom.Application.Investors.Commands.CreateInvestors;
using OrgBloom.Application.Investors.Commands.UpdateInvestors;
using OrgBloom.Application.Entrepreneurs.Queries.GetEntrepreneurs;
using OrgBloom.Application.Entrepreneurs.Commands.CreateEntrepreneurs;
using OrgBloom.Application.ProjectManagers.Queries.GetProjectManagers;
using OrgBloom.Application.Representatives.Queries.GetRepresentatives;
using OrgBloom.Application.Entrepreneurs.Commands.UpdateEntrepreneurs;
using OrgBloom.Application.ProjectManagers.Commands.CreateProjectManagers;
using OrgBloom.Application.Representatives.Commands.CreateRepresentatives;
using OrgBloom.Application.ProjectManagers.Commands.UpdateProjectManagers;
using OrgBloom.Application.Representatives.Commands.UpdateRepresentatives;

namespace OrgBloom.Bot.BotServices;

public partial class BotUpdateHandler
{
    private async Task HandleProfessionAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        var handle = message.Text switch
        {
            "Investorlik qilish" => InvestmentQueryAsync(botClient, message, cancellationToken),
            "Investitsiya jalb qilish" => EntrepreneurshipQueryAsync(botClient, message, cancellationToken),
            "Vakil bo'lish" => RepresentationQueryAsync(botClient, message, cancellationToken),
            "Loyiha boshqarish" => ProjectManagementQueryAsync(botClient, message, cancellationToken),
            _ => HandleUnknownMessageAsync(botClient, message, cancellationToken)
        };

        try { await handle; }
        catch (Exception ex) { logger.LogError(ex, "Error handling message from {from.FirstName}", user.FirstName); }
    }

    private async Task HandleResendApplicationAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        var handle = message.Text switch
        {
            "Qayta yuborish" => ClarifyProfessionQueryAsync(botClient, message, cancellationToken),
            "Ortga" => SendMainMenuAsync(botClient, message, cancellationToken),
            _ => HandleUnknownMessageAsync(botClient, message, cancellationToken)
        };

        try { await handle; }
        catch (Exception ex) { logger.LogError(ex, "Error handling message from {from.FirstName}", user.FirstName); }
    }


    private async Task ClarifyProfessionQueryAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        var profession = await mediator.Send(new GetProfessionQuery(user.Id), cancellationToken);
        switch(profession)
        {
            case UserProfession.Investor:
                await mediator.Send(new UpdateInvestorIsSubmittedCommand() { UserId = user.Id, IsSubmitted = false }, cancellationToken);
                await InvestmentQueryAsync(botClient, message, cancellationToken);
                break;
            case UserProfession.Entrepreneur:
                await mediator.Send(new UpdateEntrepreneurIsSubmittedCommand() { UserId = user.Id, IsSubmitted = false }, cancellationToken);
                await EntrepreneurshipQueryAsync(botClient, message, cancellationToken);
                break;
            case UserProfession.ProjectManager:
                await mediator.Send(new UpdateProjectManagerIsSubmittedCommand() { UserId = user.Id, IsSubmitted = false }, cancellationToken);
                await ProjectManagementQueryAsync(botClient, message, cancellationToken);
                break;
            case UserProfession.Representative:
                await mediator.Send(new UpdateRepresentativeIsSubmittedCommand() { UserId = user.Id, IsSubmitted = false }, cancellationToken);
                await RepresentationQueryAsync(botClient, message, cancellationToken);
                break;
            default: 
                await HandleUnknownMessageAsync(botClient, message, cancellationToken);
                break;
        };
    }


    private async Task RepresentationQueryAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        await mediator.Send(new UpdateProfessionCommand() { Id = user.Id, Profession = UserProfession.Representative }, cancellationToken);
        var application = await mediator.Send(new GetRepresentativeByUserIdQuery(user.Id), cancellationToken)
            ?? await mediator.Send(new CreateRepresentativeWithReturnCommand() { UserId = user.Id }, cancellationToken);

        if (application.IsSubmitted)
            await SendAlreadyExistApplicationAsync(StringHelper.GetRepresentationApplicationInfoForm(application), botClient, message, cancellationToken);
        else
            await SendRequestForFirstNameAsync(botClient, message, cancellationToken);
    }

    private async Task ProjectManagementQueryAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        await mediator.Send(new UpdateProfessionCommand() { Id = user.Id, Profession = UserProfession.ProjectManager }, cancellationToken);
        var application = await mediator.Send(new GetProjectManagerByUserIdQuery(user.Id), cancellationToken)
            ?? await mediator.Send(new CreateProjectManagerWithReturnCommand() { UserId = user.Id }, cancellationToken);

        if (application.IsSubmitted)
            await SendAlreadyExistApplicationAsync(StringHelper.GetProjectManagementApplicationInfoForm(application), botClient, message, cancellationToken);
        else
            await SendRequestForFirstNameAsync(botClient, message, cancellationToken);
    }

    private async Task EntrepreneurshipQueryAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        await mediator.Send(new UpdateProfessionCommand() { Id = user.Id, Profession = UserProfession.Entrepreneur }, cancellationToken);
        var application = await mediator.Send(new GetEntrepreneurByUserIdQuery(user.Id), cancellationToken)
            ?? await mediator.Send(new CreateEntrepreneurWithReturnCommand() { UserId = user.Id }, cancellationToken);

        if (application.IsSubmitted)
            await SendAlreadyExistApplicationAsync(StringHelper.GetEntrepreneurshipApplicationInfoForm(application), botClient, message, cancellationToken);
        else
            await SendRequestForFirstNameAsync(botClient, message, cancellationToken);
    }

    private async Task InvestmentQueryAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        await mediator.Send(new UpdateProfessionCommand() { Id = user.Id, Profession = UserProfession.Investor }, cancellationToken);
        var application = await mediator.Send(new GetInvestorByUserIdQuery(user.Id), cancellationToken)
            ?? await mediator.Send(new CreateInvestorWithReturnCommand() { UserId = user.Id }, cancellationToken);
        
        if (application.IsSubmitted)
            await SendAlreadyExistApplicationAsync(StringHelper.GetInvestmentApplicationInfoForm(application), botClient, message, cancellationToken);
        else
            await SendRequestForFirstNameAsync(botClient, message, cancellationToken);
    }

    private async Task HandleExpectationAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(message);
        ArgumentNullException.ThrowIfNull(message.Text);

        var profession = await mediator.Send(new GetProfessionQuery(user.Id), cancellationToken);
        var handler = profession switch
        {
            UserProfession.Representative => mediator.Send(new UpdateRepresentativeExpectationCommand() { Id = user.Id, Expectation = message.Text }, cancellationToken), // TODO: need validation
            UserProfession.ProjectManager => mediator.Send(new UpdateProjectManagerExpectationCommand() { Id = user.Id, Expectation = message.Text }, cancellationToken),
        };

        try { await handler; }
        catch (Exception ex) { logger.LogError(ex, "Error handling message from {user.FirstName}", user.FirstName); }

        await SendRequestForPurposeAsync(botClient, message, cancellationToken);
    }
}