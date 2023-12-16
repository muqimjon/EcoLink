using Telegram.Bot;
using Telegram.Bot.Types;
using OrgBloom.Domain.Enums;
using OrgBloom.Bot.BotServices.Helpers;
using OrgBloom.Application.Users.Commands.UpdateUsers;
using OrgBloom.Application.ProjectManagers.Queries.GetProjectManagers;
using OrgBloom.Application.ProjectManagers.Commands.CreateProjectManagers;

namespace OrgBloom.Bot.BotServices;

public partial class BotUpdateHandler
{
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
}
