using Telegram.Bot;
using Telegram.Bot.Types;
using OrgBloom.Domain.Enums;
using OrgBloom.Bot.BotServices.Helpers;
using OrgBloom.Application.Users.Queries.GetUsers;
using OrgBloom.Application.Users.Commands.UpdateUsers;
using OrgBloom.Application.Investors.Queries.GetInvestors;
using OrgBloom.Application.Investors.Commands.CreateInvestors;
using OrgBloom.Application.Entrepreneurs.Queries.GetEntrepreneurs;
using OrgBloom.Application.Entrepreneurs.Commands.CreateEntrepreneurs;
using OrgBloom.Application.ProjectManagers.Queries.GetProjectManagers;
using OrgBloom.Application.ProjectManagers.Commands.CreateProjectManagers;
using OrgBloom.Application.Representatives.Commands.CreateRepresentatives;
using OrgBloom.Application.Representatives.Queries.GetRepresentatives;

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
        var handle = profession switch
        {
            UserProfession.Investor => InvestmentQueryAsync(botClient, message, cancellationToken),
            UserProfession.Entrepreneur => EntrepreneurshipQueryAsync(botClient, message, cancellationToken),
            UserProfession.ProjectManager => ProjectManagementQueryAsync(botClient, message, cancellationToken),
            UserProfession.Representative => RepresentationQueryAsync(botClient, message, cancellationToken),
            _ => HandleUnknownMessageAsync(botClient, message, cancellationToken)
        };

        try { await handle; }
        catch (Exception ex) { logger.LogError(ex, "Error handling message from {from.FirstName}", user.FirstName); }
    }


    private async Task RepresentationQueryAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        var application = await mediator.Send(new GetRepresentativeByUserIdQuery() { UserId = user.Id }, cancellationToken)
            ?? await mediator.Send(new CreateRepresentativeWithReturnCommand() { UserId = user.Id }, cancellationToken);
        if (application.IsSubmitted)
            await SendAlreadyExistApplicationAsync(StringHelper.GetRepresentationApplicationInfoForm(application), botClient, message, cancellationToken);
        else
        {
            await mediator.Send(new UpdateProfessionCommand() { Id = user.Id, Profession = UserProfession.ProjectManager }, cancellationToken);
            await SendRequestForFirstNameAsync(botClient, message, cancellationToken);
        }
    }

    private async Task ProjectManagementQueryAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        var application = await mediator.Send(new GetProjectManagerByUserIdQuery() { UserId = user.Id }, cancellationToken)
            ?? await mediator.Send(new CreateProjectManagerWithReturnCommand() { UserId = user.Id }, cancellationToken);
        if (application.IsSubmitted)
            await SendAlreadyExistApplicationAsync(StringHelper.GetProjectManagementApplicationInfoForm(application), botClient, message, cancellationToken);
        else
        {
            await mediator.Send(new UpdateProfessionCommand() { Id = user.Id, Profession = UserProfession.ProjectManager }, cancellationToken);
            await SendRequestForFirstNameAsync(botClient, message, cancellationToken);
        }
    }

    private async Task EntrepreneurshipQueryAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        var application = await mediator.Send(new GetEntrepreneurByUserIdQuery() { UserId = user.Id }, cancellationToken)
            ?? await mediator.Send(new CreateEntrepreneurWithReturnCommand() { UserId = user.Id }, cancellationToken);
        if (application.IsSubmitted)
            await SendAlreadyExistApplicationAsync(StringHelper.GetEntrepreneurshipApplicationInfoForm(application), botClient, message, cancellationToken);
        else
        {
            await mediator.Send(new UpdateProfessionCommand() { Id = user.Id, Profession = UserProfession.Entrepreneur }, cancellationToken);
            await SendRequestForFirstNameAsync(botClient, message, cancellationToken);
        }
    }

    private async Task InvestmentQueryAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        var application = await mediator.Send(new GetInvestorByUserIdQuery() { UserId = user.Id }, cancellationToken)
            ?? await mediator.Send(new CreateInvestorWithReturnCommand() { UserId = user.Id }, cancellationToken);
        if (application.IsSubmitted)
            await SendAlreadyExistApplicationAsync(StringHelper.GetInvestmentApplicationInfoForm(application), botClient, message, cancellationToken);
        else
        {
            await mediator.Send(new UpdateProfessionCommand() { Id = user.Id, Profession = UserProfession.Investor }, cancellationToken);
            await SendRequestForFirstNameAsync(botClient, message, cancellationToken);
        }
    }
}