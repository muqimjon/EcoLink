using EcoLink.Application.Investors.Queries.GetInvestors;
using EcoLink.Application.Investors.Commands.UpdateInvestors;
using EcoLink.Application.Entrepreneurs.Queries.GetEntrepreneurs;
using EcoLink.Application.Entrepreneurs.Commands.UpdateEntrepreneurs;
using EcoLink.Application.Entrepreneurs.Commands.CreateEntrepreneurs;
using EcoLink.Application.ProjectManagers.Queries.GetProjectManagers;
using EcoLink.Application.Representatives.Queries.GetRepresentatives;
using EcoLink.Application.InvestmentApps.Commands.CreateInvestmentApps;
using EcoLink.Application.Representatives.Commands.UpdateRepresentatives;
using EcoLink.Application.ProjectManagers.Commands.UpdateProjectManagers;
using EcoLink.Application.RepresentationApps.Commands.CreateRepresentationApps;
using EcoLink.Application.ProjectManagementApps.Commands.CreateProjectManagementApps;

namespace EcoLink.Bot.BotServices;

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

        await botClient.EditMessageTextAsync(
            chatId: callbackQuery.Message.Chat.Id,
            messageId: callbackQuery.Message.MessageId,
            text: $"{callbackQuery.Message.Text}\n\n{localizer["txtResponseSubmittedApp"]}",
            cancellationToken: cancellationToken);

        var application = await mediator.Send(new GetRepresentativeByUserIdQuery(user.Id), cancellationToken);

        await mediator.Send(new CreateRepresentationAppWithReturnCommand()
        {
            UserId = user.Id,
            Area = application.Area,
            Age = application.User.Age,
            Purpose = application.Purpose,
            Degree = application.User.Degree,
            Address = application.User.Address,
            LastName = application.User.LastName,
            Expectation = application.Expectation,
            FirstName = application.User.FirstName,
            Languages = application.User.Languages,
            Experience = application.User.Experience,
        }, cancellationToken);

        await mediator.Send(new UpdateRepresentativeIsSubmittedByUserCommand() { UserId = user.Id, IsSubmitted = true }, cancellationToken);
    }

    private async Task HandleSubmitProjectManagerApplicationAsync(ITelegramBotClient botClient, CallbackQuery callbackQuery, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(callbackQuery);
        ArgumentNullException.ThrowIfNull(callbackQuery.Message);

        await botClient.EditMessageTextAsync(
            chatId: callbackQuery.Message.Chat.Id,
            messageId: callbackQuery.Message.MessageId,
            text: $"{callbackQuery.Message.Text}\n\n{localizer["txtResponseSubmittedApp"]}",
            cancellationToken: cancellationToken);

        var application = await mediator.Send(new GetProjectManagerByUserIdQuery(user.Id), cancellationToken);

        await mediator.Send(new CreateProjectManagementAppWithReturnCommand()
        {
            UserId = user.Id,
            Age = application.User.Age,
            Purpose = application.Purpose,
            Degree = application.User.Degree,
            Address = application.User.Address,
            LastName = application.User.LastName,
            Expectation = application.Expectation,
            FirstName = application.User.FirstName,
            Languages = application.User.Languages,
            Experience = application.User.Experience,
            ProjectDirection = application.ProjectDirection,
        }, cancellationToken);

        await mediator.Send(new UpdateProjectManagerIsSubmittedByUserIdCommand() { UserId = user.Id, IsSubmitted = true }, cancellationToken);
    }

    private async Task HandleSubmitEntrepreneurApplicationAsync(ITelegramBotClient botClient, CallbackQuery callbackQuery, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(callbackQuery);
        ArgumentNullException.ThrowIfNull(callbackQuery.Message);

        await botClient.EditMessageTextAsync(
            chatId: callbackQuery.Message.Chat.Id,
            messageId: callbackQuery.Message.MessageId,
            text: $"{callbackQuery.Message.Text}\n\n{localizer["txtResponseSubmittedApp"]}",
            cancellationToken: cancellationToken);

        var application = await mediator.Send(new GetEntrepreneurByUserIdQuery(user.Id), cancellationToken);

        await mediator.Send(new CreateEntrepreneurshipAppWithReturnCommand()
            {
                UserId = user.Id,
                Age = application.User.Age,
                Project = application.Project,
                Phone = application.User.Phone,
                Email = application.User.Email,
                HelpType = application.HelpType,
                Degree = application.User.Degree,
                LastName = application.User.LastName,
                FirstName = application.User.FirstName,
                Experience = application.User.Experience,
                AssetsInvested = application.AssetsInvested,
                RequiredFunding = application.RequiredFunding,
            }, cancellationToken);

        await mediator.Send(new UpdateEntrepreneurIsSubmittedByUserIdCommand() { UserId = user.Id, IsSubmitted = true }, cancellationToken);
    }

    private async Task HandleSubmitInvestmentApplicationAsync(ITelegramBotClient botClient, CallbackQuery callbackQuery, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(callbackQuery);
        ArgumentNullException.ThrowIfNull(callbackQuery.Message);

        await botClient.EditMessageTextAsync(
            chatId: callbackQuery.Message.Chat.Id,
            messageId: callbackQuery.Message.MessageId,
            text: $"{callbackQuery.Message.Text}\n\n{localizer["txtResponseSubmittedApp"]}",
            cancellationToken: cancellationToken);

        var application = await mediator.Send(new GetInvestorByUserIdQuery(user.Id), cancellationToken);
        
        await mediator.Send(new CreateInvestmentAppWithReturnCommand()
        {
            UserId = user.Id,
            Age = application.User.Age,
            Sector = application.Sector,
            Phone = application.User.Phone,
            Email = application.User.Email,
            Degree = application.User.Degree,
            LastName = application.User.LastName,
            FirstName = application.User.FirstName,
            InvestmentAmount = application.InvestmentAmount
        }, cancellationToken);

        await mediator.Send(new UpdateInvestorIsSubmittedByUserIdCommand() { UserId = user.Id, IsSubmitted = true }, cancellationToken);
    }
}
