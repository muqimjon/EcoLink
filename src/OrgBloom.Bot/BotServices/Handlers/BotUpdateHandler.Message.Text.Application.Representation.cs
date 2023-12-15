using OrgBloom.Application.Representatives.Commands.UpdateRepresentatives;
using Telegram.Bot.Types;
using Telegram.Bot;

namespace OrgBloom.Bot.BotServices;

public partial class BotUpdateHandler
{
    private async Task HandleAreaAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(message);
        ArgumentNullException.ThrowIfNull(message.Text);

        await mediator.Send(new UpdateRepresentativeAreaCommand() { Id = user.Id, Area = message.Text }, cancellationToken); // TODO: need validation

        await SendRequestForExpectationForRepresentationAsync(botClient, message, cancellationToken);
    }

    private async Task HandleExpectationAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(message);
        ArgumentNullException.ThrowIfNull(message.Text);

        await mediator.Send(new UpdateRepresentativeExpectationCommand() { Id = user.Id, Expectation = message.Text }, cancellationToken); // TODO: need validation

        await SendRequestForPurposeAsync(botClient, message, cancellationToken);
    }
}
