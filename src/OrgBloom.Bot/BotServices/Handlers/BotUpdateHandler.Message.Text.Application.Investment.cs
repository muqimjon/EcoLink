using Telegram.Bot;
using Telegram.Bot.Types;
using OrgBloom.Application.Investors.Commands.UpdateInvestors;

namespace OrgBloom.Bot.BotServices;

public partial class BotUpdateHandler
{
    private async Task HandleInvestmentAmountAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(message);
        ArgumentNullException.ThrowIfNull(message.Text);

        await mediator.Send(new UpdateInvestorInvestmentAmountCommand() { Id = user.Id, InvestmentAmount = message.Text }, cancellationToken); // TODO: need validation

        await SendRequestForPhoneNumberAsync(botClient, message, cancellationToken);
    }
}
