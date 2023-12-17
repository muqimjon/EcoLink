using Telegram.Bot;
using Telegram.Bot.Types;
using OrgBloom.Domain.Enums;
using OrgBloom.Bot.BotServices.Helpers;
using OrgBloom.Application.Users.Commands.UpdateUsers;
using OrgBloom.Application.Representatives.Queries.GetRepresentatives;
using OrgBloom.Application.Representatives.Commands.CreateRepresentatives;
using OrgBloom.Application.Representatives.Commands.UpdateRepresentatives;

namespace OrgBloom.Bot.BotServices;

public partial class BotUpdateHandler
{
    private async Task RepresentationQueryAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        await mediator.Send(new UpdateProfessionCommand() { Id = user.Id, Profession = UserProfession.Representative }, cancellationToken);
        var application = await mediator.Send(new GetRepresentativeByUserIdQuery(user.Id), cancellationToken)
            ?? await mediator.Send(new CreateRepresentativeWithReturnCommand() { UserId = user.Id }, cancellationToken);

        if (application.IsSubmitted)
            await SendAlreadyExistApplicationAsync(StringHelper.GetApplicationInfoForm(application), botClient, message, cancellationToken);
        else
            await SendRequestForFirstNameAsync(botClient, message, cancellationToken);
    }

    private async Task HandleAreaAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(message);
        ArgumentNullException.ThrowIfNull(message.Text);

        await mediator.Send(new UpdateRepresentativeAreaByUserIdCommand() { UserId = user.Id, Area = message.Text }, cancellationToken); // TODO: need validation

        await SendRequestForExpectationAsync(botClient, message, cancellationToken);
    }
}
