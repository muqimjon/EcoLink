using OrgBloom.Application.Investors.Queries.GetInvestors;
using OrgBloom.Application.Investors.Commands.UpdateInvestors;
using OrgBloom.Application.Entrepreneurs.Queries.GetEntrepreneurs;
using OrgBloom.Application.Entrepreneurs.Commands.UpdateEntrepreneurs;
using OrgBloom.Application.Entrepreneurs.Commands.CreateEntrepreneurs;
using OrgBloom.Application.ProjectManagers.Queries.GetProjectManagers;
using OrgBloom.Application.Representatives.Queries.GetRepresentatives;
using OrgBloom.Application.InvestmentApps.Commands.CreateInvestmentApps;
using OrgBloom.Application.Representatives.Commands.UpdateRepresentatives;
using OrgBloom.Application.ProjectManagers.Commands.UpdateProjectManagers;
using OrgBloom.Application.RepresentationApps.Commands.CreateRepresentationApps;
using OrgBloom.Application.ProjectManagementApps.Commands.CreateProjectManagementApps;

namespace OrgBloom.Bot.BotServices;

public partial class BotUpdateHandler
{
    private async Task HandleSubmitApplicationAsync(ITelegramBotClient botClient, CallbackQuery callbackQuery, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(callbackQuery.Message);

        var profession = await mediator.Send(new GetProfessionQuery(user.Id), cancellationToken);
        var handle = profession switch
        {
            UserProfession.Entrepreneur => HandleSubmitEntrepreneurApplicationAsync(botClient, callbackQuery, cancellationToken),
            UserProfession.Investor => HandleSubmitInvestmentApplicationAsync(botClient, callbackQuery, cancellationToken),
            UserProfession.ProjectManager => HandleSubmitProjectManagerApplicationAsync(botClient, callbackQuery, cancellationToken),
            UserProfession.Representative => HandleSubmitRepresentativeApplicationAsync(botClient, callbackQuery, cancellationToken),
            _ => HandleUnknownSubmissionAsync(botClient, callbackQuery, cancellationToken)
        };

        try { await handle; }
        catch (Exception ex) { logger.LogError(ex, "Error handling callback query application submit: {callbackQuery.Data}", callbackQuery.Data); }
        
        await SendMainMenuAsync(botClient, callbackQuery.Message, cancellationToken);
    }

    private async Task HandleSubmitRepresentativeApplicationAsync(ITelegramBotClient botClient, CallbackQuery callbackQuery, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(callbackQuery);
        ArgumentNullException.ThrowIfNull(callbackQuery.Message);

        await botClient.SendTextMessageAsync(
            chatId: callbackQuery.Message.Chat.Id,
            text: localizer["txtResponseSubmittedApp"],
            cancellationToken: cancellationToken);

        var application = await mediator.Send(new GetRepresentativeByUserIdQuery(user.Id), cancellationToken);

        await mediator.Send(new CreateRepresentationAppWithReturnCommand()
        {
            UserId = user.Id,
            Area = application.Area,
            Purpose = application.Purpose,
            Degree = application.User.Degree,
            Address = application.User.Address,
            LastName = application.User.LastName,
            Expectation = application.Expectation,
            FirstName = application.User.FirstName,
            Languages = application.User.Languages,
            Experience = application.User.Experience,
            DateOfBirth = application.User.DateOfBirth,
        }, cancellationToken);

        await mediator.Send(new UpdateRepresentativeIsSubmittedByUserCommand() { UserId = user.Id, IsSubmitted = true }, cancellationToken);
    }

    private async Task HandleSubmitProjectManagerApplicationAsync(ITelegramBotClient botClient, CallbackQuery callbackQuery, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(callbackQuery);
        ArgumentNullException.ThrowIfNull(callbackQuery.Message);

        await botClient.SendTextMessageAsync(
            chatId: callbackQuery.Message.Chat.Id,
            text: localizer["txtResponseSubmittedApp"],
            cancellationToken: cancellationToken);

        var application = await mediator.Send(new GetProjectManagerByUserIdQuery(user.Id), cancellationToken);

        await mediator.Send(new CreateProjectManagementAppWithReturnCommand()
        {
            UserId = user.Id,
            Purpose = application.Purpose,
            Degree = application.User.Degree,
            Address = application.User.Address,
            LastName = application.User.LastName,
            Expectation = application.Expectation,
            FirstName = application.User.FirstName,
            Languages = application.User.Languages,
            Experience = application.User.Experience,
            DateOfBirth = application.User.DateOfBirth,
            ProjectDirection = application.ProjectDirection,
        }, cancellationToken);

        await mediator.Send(new UpdateProjectManagerIsSubmittedByUserIdCommand() { UserId = user.Id, IsSubmitted = true }, cancellationToken);
    }

    private async Task HandleSubmitEntrepreneurApplicationAsync(ITelegramBotClient botClient, CallbackQuery callbackQuery, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(callbackQuery);
        ArgumentNullException.ThrowIfNull(callbackQuery.Message);

        await botClient.SendTextMessageAsync(
            chatId: callbackQuery.Message.Chat.Id,
            text: localizer["txtResponseSubmittedApp"],
            cancellationToken: cancellationToken);

        var application = await mediator.Send(new GetEntrepreneurByUserIdQuery(user.Id), cancellationToken);

        await mediator.Send(new CreateEntrepreneurshipAppWithReturnCommand()
            {
                UserId = user.Id,
                Project = application.Project,
                Phone = application.User.Phone,
                Email = application.User.Email,
                HelpType = application.HelpType,
                Degree = application.User.Degree,
                LastName = application.User.LastName,
                FirstName = application.User.FirstName,
                Experience = application.User.Experience,
                DateOfBirth = application.User.DateOfBirth,
                AssetsInvested = application.AssetsInvested,
                RequiredFunding = application.RequiredFunding,
            }, cancellationToken);

        await mediator.Send(new UpdateEntrepreneurIsSubmittedByUserIdCommand() { UserId = user.Id, IsSubmitted = true }, cancellationToken);
    }

    private async Task HandleSubmitInvestmentApplicationAsync(ITelegramBotClient botClient, CallbackQuery callbackQuery, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(callbackQuery);
        ArgumentNullException.ThrowIfNull(callbackQuery.Message);

        await botClient.SendTextMessageAsync(
            chatId: callbackQuery.Message.Chat.Id,
            text: localizer["txtResponseSubmittedApp"],
            cancellationToken: cancellationToken);

        var application = await mediator.Send(new GetInvestorByUserIdQuery(user.Id), cancellationToken);
        
        await mediator.Send(new CreateInvestmentAppWithReturnCommand()
        {
            UserId = user.Id,
            Sector = application.Sector,
            Phone = application.User.Phone,
            Email = application.User.Email,
            Degree = application.User.Degree,
            LastName = application.User.LastName,
            FirstName = application.User.FirstName,
            DateOfBirth = application.User.DateOfBirth,
            InvestmentAmount = application.InvestmentAmount
        }, cancellationToken);

        await mediator.Send(new UpdateInvestorIsSubmittedByUserIdCommand() { UserId = user.Id, IsSubmitted = true }, cancellationToken);
    }
}
