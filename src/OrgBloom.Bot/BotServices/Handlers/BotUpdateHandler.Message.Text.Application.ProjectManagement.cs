using Telegram.Bot;
using Telegram.Bot.Types;
using OrgBloom.Domain.Enums;
using OrgBloom.Bot.BotServices.Helpers;
using OrgBloom.Application.Users.Queries.GetUsers;
using OrgBloom.Application.Users.Commands.UpdateUsers;
using OrgBloom.Application.ProjectManagers.Queries.GetProjectManagers;
using OrgBloom.Application.ProjectManagers.Commands.CreateProjectManagers;
using OrgBloom.Application.ProjectManagers.Commands.UpdateProjectManagers;

namespace OrgBloom.Bot.BotServices;

public partial class BotUpdateHandler
{
    private async Task HandleSelectedProjectManagementMenuAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        await mediator.Send(new UpdateProfessionCommand() { Id = user.Id, Profession = UserProfession.ProjectManager }, cancellationToken);
        var handler = message.Text switch
        {
            { } text when text == localizer["rbtnApply"] => ProjectManagementQueryAsync(botClient, message, cancellationToken),
            { } text when text == localizer["rbtnInfo"] => SendProfessionInfoAsync(botClient, message, cancellationToken),
            { } text when text == localizer["rbtnBack"] => SendMenuProfessionsAsync(botClient, message, cancellationToken),
            _ => HandleUnknownMessageAsync(botClient, message, cancellationToken)
        };

        try { await handler; }
        catch (Exception ex) { logger.LogError(ex, "Error handling message from {user.FirstName}", user.FirstName); }
    }

    private async Task ProjectManagementQueryAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        var application = await mediator.Send(new GetProjectManagerByUserIdQuery(user.Id), cancellationToken)
            ?? await mediator.Send(new CreateProjectManagerWithReturnCommand() { UserId = user.Id }, cancellationToken);

        if (application.IsSubmitted)
            await SendAlreadyExistApplicationAsync(StringHelper.GetApplicationInfoForm(application), botClient, message, cancellationToken);
        else
            await SendRequestForFirstNameAsync(botClient, message, cancellationToken);
    }

    private async Task HandleProjectDirectionForProjectManagementAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(message);
        ArgumentNullException.ThrowIfNull(message.Text);

        var profession = await mediator.Send(new GetProfessionQuery(user.Id), cancellationToken);
        Task handler;

        if (message.Text.Equals(localizer["rbtnCancel"]))
        {
            handler = profession switch
            {
                UserProfession.None => SendMenuSettingsAsync(botClient, message, cancellationToken),
                _ => SendMenuProfessionsAsync(botClient, message, cancellationToken)
            };
        }
        else
        {
            await mediator.Send(new UpdateProjectManagerProjectDirectionByUserIdCommand() { UserId = user.Id, ProjectDirection = message.Text }, cancellationToken); // TODO: need validation
            handler = profession switch
            {
                UserProfession.None => SendMenuSettingsAsync(botClient, message, cancellationToken),
                _ => SendRequestForExpectationAsync(botClient, message, cancellationToken)
            };
        }

        try { await handler; }
        catch (Exception ex) { logger.LogError(ex, "Error handling message from {user.FirstName}", user.FirstName); }
    }
}
