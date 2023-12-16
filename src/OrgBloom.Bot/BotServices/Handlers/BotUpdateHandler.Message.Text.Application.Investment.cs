using Telegram.Bot;
using Telegram.Bot.Types;
using OrgBloom.Domain.Enums;
using OrgBloom.Bot.BotServices.Helpers;
using OrgBloom.Application.Users.Commands.UpdateUsers;
using OrgBloom.Application.Investors.Queries.GetInvestors;
using OrgBloom.Application.Investors.Commands.UpdateInvestors;
using OrgBloom.Application.Investors.Commands.CreateInvestors;

namespace OrgBloom.Bot.BotServices;

public partial class BotUpdateHandler
{
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

    private async Task HandleInvestmentAmountAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(message);
        ArgumentNullException.ThrowIfNull(message.Text);

        await mediator.Send(new UpdateInvestorInvestmentAmountCommand() { Id = user.Id, InvestmentAmount = message.Text }, cancellationToken); // TODO: need validation

        await SendRequestForPhoneNumberAsync(botClient, message, cancellationToken);
    }
}
